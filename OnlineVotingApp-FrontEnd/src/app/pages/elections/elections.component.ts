import { animate, state, style, transition, trigger } from "@angular/animations";
import { SelectionModel } from "@angular/cdk/collections";
import { AfterViewInit, Component, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialog } from "@angular/material/dialog";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { ModalDismissReasons, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { Candidate } from "src/app/shared/models/candidate";
import { ElectionType } from "src/app/shared/models/election-type";
import { ElectionView } from "src/app/shared/models/election-view";
import { ElectoralRoom } from "src/app/shared/models/electoral-room";
import { UserAuthenticationView } from "src/app/shared/models/user-auth-view";
import { CandidateService } from "src/app/shared/services/admin/elections-admin/candidate.service";
import { ElectionService } from "src/app/shared/services/admin/elections-admin/election.service";
import { ElectoralRoomService } from "src/app/shared/services/admin/elections-admin/electoral-room.service";
import { AlertService } from "src/app/shared/services/others/alert.service";
import { AddCandidateDialog } from "./add-candidate/add-candidate.component";
import { AddElectoralRoomDialog } from "./add-electoral-room/add-electoral-room.component";
import { ConfirmDeleteCandidate } from "./confirm-delete-candidate/confirm-delete-candidate.component";
import { ConfirmDeleteElectoralRoom } from "./confirm-delete-electoral-room/confirm-delete-electoral-room.component";
import { UpdateElectoralRoomDialog } from "./update-electoral-room/update-electoral-room.component";

@Component({selector:'elections',templateUrl:'elections.component.html',styleUrls:['./elections.component.css'],
animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})

export class ElectoralRoomComponent implements OnInit,AfterViewInit{

    //for colleges
    displayedColumnsElectoralRoom: string[] = ['electoralRoomName','electoralRoomDate','college','department','edit','delete'];
    dataSourceElectoralRoom = new MatTableDataSource<ElectionView>();
    selectionElectoralRoom = new SelectionModel<ElectionView>(true, []);
    isReadyElectoralRoom=false;
    @ViewChild(MatPaginator) paginator: MatPaginator;

    //for departments
    displayedColumnsCandidate: string[] = ['candidateName','delete'];
    dataSourceCandidate = new MatTableDataSource<Candidate>();
    selectionCandidate = new SelectionModel<Candidate>(true, []);
    isReadyCandidate=false;
    selectedElectoralRoom:number;
    
    expandedElement: Candidate|null=null;

    selectedElection:number;

    constructor(
                private alertService:AlertService,
                private modalService: NgbModal,
                private electoralRoomService:ElectoralRoomService,
                private candidateService:CandidateService,
                private formBuilder:FormBuilder,
                private dialog:MatDialog
                
            ) {
              this.getElections();
            }

    ngOnInit(){}

    ngAfterViewInit() {
      this.dataSourceElectoralRoom.paginator = this.paginator;

    }

    getElections()
    {
        this.electoralRoomService.getAll().pipe(first()).toPromise()
        .then(result=>{

          console.log(result);
          
          this.dataSourceElectoralRoom.data=result;
                    
          for(let i=0;i<result.length;i++){

            this.candidateService.getAll(result[i].idElectoralRoom).toPromise()
            .then(result=>{
              this.dataSourceElectoralRoom.data[i].candidates=result;
            })
            .catch(error=>{
              this.alertService.error(error);
            });

          }
           
        })
        .catch(error=>{
          this.alertService.error(error);
        })

        this.isReadyElectoralRoom=true;
    }

    getRecord(row){

        this.dataSourceCandidate=new MatTableDataSource<Candidate>();
        this.selectionCandidate = new SelectionModel<Candidate>(true, []);
        this.isReadyCandidate=false;
        this.selectedElectoralRoom=row.idElectoralRoom;

        this.dataSourceCandidate.data=this.dataSourceElectoralRoom.data.find(c=>c.idElectoralRoom==row.idElectoralRoom).candidates;

        this.isReadyCandidate=true;
    }

    openAddElectoralRoomDialog(){

        let dialogRef=this.dialog.open(AddElectoralRoomDialog);
        dialogRef.afterClosed().subscribe(result=>{

          if(result){
            this.alertService.success("added succesfully");
          this.getElections();

          }
                   
          
        },error=>{
          this.alertService.error(error.error.message);
        });
      }

      openUpdateElectoralRoomDialog(element:ElectionView){

          let dialogRef=this.dialog.open(UpdateElectoralRoomDialog,{
            data:element
          });

          dialogRef.afterClosed().subscribe((result)=>{ 
            
            if(result){

              this.getElections();
            this.alertService.success("updated succesfully");
            }
            
          },error=>{
            this.alertService.error(error.error.message);
          });
          
      }

      deleteElectoralRoom(element:ElectionView){

        let dialogRef=this.dialog.open(ConfirmDeleteElectoralRoom,{data:element});

        dialogRef.afterClosed().subscribe((result)=>{
          if(result){
            this.alertService.success("deleted succesfully");
            this.getElections();
           
          }
         
          
        },error=>{
          this.alertService.error(error.error.message);
        });

      }

      deleteCandidate(row:Candidate){

        let dialogRef=this.dialog.open(ConfirmDeleteCandidate,{
          data:row
        });

        dialogRef.afterClosed().subscribe((result)=>{

          if(result){

            this.alertService.success("deleted succesfully");
            this.getElections();
          }
        
          
        },error=>{
          this.alertService.error(error.error.message);
        });
      }

      openAddCandiadateDialog(row:ElectionView){

        row.idElectoralRoom
        let dialogRef=this.dialog.open(AddCandidateDialog,{
          data:row
        });

        dialogRef.afterClosed().subscribe((result)=>{

          if(result){

            this.alertService.success("added succesfully");
          this.getElections();
          }
        
        },error=>{
          this.alertService.error(error.error.message);
        });
      }
}