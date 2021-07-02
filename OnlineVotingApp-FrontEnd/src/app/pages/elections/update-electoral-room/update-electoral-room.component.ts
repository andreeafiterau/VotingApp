import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { College } from "src/app/shared/models/college";
import { Department } from "src/app/shared/models/department";
import { ElectionType } from "src/app/shared/models/election-type";
import { ElectionView } from "src/app/shared/models/election-view";
import { ElectoralRoom } from "src/app/shared/models/electoral-room";
import { CollegeService } from "src/app/shared/services/admin/college-admin/college.service";
import { DepartmentService } from "src/app/shared/services/admin/college-admin/department.service";
import { ElectionService } from "src/app/shared/services/admin/elections-admin/election.service";
import { ElectoralRoomService } from "src/app/shared/services/admin/elections-admin/electoral-room.service";
import { AlertService } from "src/app/shared/services/others/alert.service";

@Component({templateUrl:'./update-electoral-room.component.html'})

export class UpdateElectoralRoomDialog implements OnInit{

    showCollege=false;
    showDep=false;

    updateElectoralRoomForm:FormGroup;

    selectedElectionType:number;
    selectedCollege:number;
    selectedDepartment:number;

    elections:ElectionType[]=[];
    colleges:College[]=[];
    departments:Department[]=[];

    constructor(private formBuilder:FormBuilder,
                private electionService:ElectionService,
                private alertService:AlertService,
                private electoralRoomService:ElectoralRoomService,
                @Inject(MAT_DIALOG_DATA)private data:ElectionView,
                private dialogRef:MatDialogRef<ElectionView>,
                private collegeService:CollegeService,
                private departmentService:DepartmentService){}

    async getElectionTypes(){

        await this.electionService.getElectionTypes().toPromise()
        .then(result=>{
            this.elections=result;
            this.elections.forEach(item=>{
                if(item.electionName==this.data.electionName)
                {
                    this.selectedElectionType=item.idElectionType;
                }
            });
        })
        .catch(error=>{
            this.alertService.error(error);           
        });

    }

    getColleges(){

        this.collegeService.getAll().toPromise()
        .then(result=>{
            this.colleges=result;

            if(this.data.college){

                this.showCollege=true;
                this.colleges.forEach(item=>{
                    if(item.collegeName==this.data.college)
                    {
                        this.selectedCollege=item.idCollege;
                    }
                });
            }

            this.getDepartments();
            
        })
        .catch(error=>{
             console.log(error);
        });
    }

    onSelectionChange(event:any){

        this.getDepartments();
    }

    onSelectionChangeElection(event){

        this.showCollege=false;
        this.showDep=false;
        
        this.selectedCollege=0;
        this.selectedDepartment=0;

        if(this.selectedElectionType==2 || this.selectedElectionType==5){
              this.showCollege=true;
        }

        if(this.selectedElectionType==5){
            this.showDep=true;
        }

    }

    getDepartments(){

        let c=new College();
        c.idCollege=this.selectedCollege;

        console.log(c);
        this.departmentService.getAll(c).toPromise()
        .then(result=>{
            this.departments=result;

            if(this.data.department){

                this.showDep=true;
                this.departments.forEach(item=>{

                    if(item.departmentName==this.data.department)
                    {
                        this.selectedDepartment=item.idDepartment;
                    }
                });
            }
           
        })
        .catch(error=>{
             console.log(error);
        });
    }

    ngOnInit(){

        this.getElectionTypes();
        this.getColleges();
        this.getDepartments();

        this.updateElectoralRoomForm=this.formBuilder.group({
            date:[new Date(this.data.electionDate),Validators.required]
        });
        
    }

    updateElectoralRoom(){

        if(this.updateElectoralRoomForm.invalid){
            return;
        }

        let er=new ElectoralRoom();
        er.idElectionType=this.selectedElectionType;
        er.date=this.updateElectoralRoomForm.controls.date.value;
        er.idElectoralRoom=this.data.idElectoralRoom;
        er.idCollege=this.selectedCollege;
        er.idDepartment=this.selectedDepartment;

        er.date=new Date(new Date(er.date).getTime()-new Date(er.date).getTimezoneOffset()*60*1000).toISOString();

        console.log(er);
        this.electoralRoomService.update(er,this.data.idElectoralRoom).toPromise()
        .then(result=>{
           this.dialogRef.close(result);
        })
        .catch(error=>{
            this.dialogRef.close(error);
        });
    }
}