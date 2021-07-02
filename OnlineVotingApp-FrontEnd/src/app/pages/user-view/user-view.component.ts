import { templateJitUrl } from "@angular/compiler";
import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Router } from "@angular/router";
import { ModalDismissReasons, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { Candidate } from "src/app/shared/models/candidate";
import { ElectionViewForUser } from "src/app/shared/models/election-view-for-user";
import { ElectoralRoom } from "src/app/shared/models/electoral-room";
import { Result } from "src/app/shared/models/result";
import { User } from "src/app/shared/models/user";
import { UserAdminView } from "src/app/shared/models/user-admin-view";
import { UserAuthenticationView } from "src/app/shared/models/user-auth-view";
import { CandidateService } from "src/app/shared/services/admin/elections-admin/candidate.service";
import { ElectoralRoomService } from "src/app/shared/services/admin/elections-admin/electoral-room.service";
import { AlertService } from "src/app/shared/services/others/alert.service";
import { UserService } from "src/app/shared/services/user/user.service";
import { VotingService } from "src/app/shared/services/user/voting.service";
import { ResultsDialog } from "./results-dialog/results-dialog";
import { UserViewVote } from "./user-view-vote-dialog/user-view-vote-dialog";


@Component({selector:'user-view', templateUrl: 'user-view.component.html' })

export class UserView implements OnInit{

    results:Result[]=[];

    currentUser: UserAuthenticationView=new UserAuthenticationView();
    currentUserSubscription: Subscription;   

    pastElectoralRooms:ElectionViewForUser[]=[];
    presentElectoralRooms:ElectionViewForUser[]=[];
    futureElectoralRooms:ElectionViewForUser[]=[];
    candidates:Candidate[]=[];

    closeResult: string;

    votingError:boolean;
    votingSuccess:boolean;
    votingErrorMessage:string;
  
    hasVoted=false;

    constructor(
        private authenticationService: UserService,
        private router: Router,
        private electoralRoomService:ElectoralRoomService,
        private candidatesService:CandidateService,
        private votingService:VotingService,
        private alertService:AlertService,
        private modalService: NgbModal,
        private dialog:MatDialog
        
    ) {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;

        });

    }

    getPast(){

        this.votingService.getPastElectoralRooms(this.currentUser.idUser).pipe(first())
                          .subscribe(data=>{

                            console.log(this.currentUser.college,this.currentUser.department,data);
                            data.forEach(item=>{
                              if(item.college==0 && item.department==0){

                                this.pastElectoralRooms.push(item);
                              }
                              else

                                if(item.college!=0 && item.college==this.currentUser.college)
                                {
                                  if(item.department==0){

                                    this.pastElectoralRooms.push(item);
                                  }
                                  else if(item.department!=0 && item.department==this.currentUser.department)
                                  {
                                    this.pastElectoralRooms.push(item);
                                  }
                                }
                              
                            });
                          },
                          error=>this.alertService.error(error.error.message));
    }

    getPresent(){

      this.votingService.getPresentElectoralRooms(this.currentUser.idUser).pipe(first())
                        .subscribe(data=>{
                          console.log(data);
                          data.forEach(item=>{
                            if(item.college==0 && item.department==0){

                              this.presentElectoralRooms.push(item);
                            }
                            else

                              if(item.college!=0 && item.college==this.currentUser.college)
                              {
                                if(item.college==0){

                                  this.presentElectoralRooms.push(item);
                                }
                                else if(item.department!=0 && item.department==this.currentUser.department)
                                {
                                  this.presentElectoralRooms.push(item);
                                }
                              }
                            
                          });
                        },
                        error=>this.alertService.error(error.error.message));
    }

    getFuture(){

      this.votingService.getFutureElectoralRooms(this.currentUser.idUser).pipe(first())
                        .subscribe(data=>{

                          console.log(data);
                          data.forEach(item=>{
                            if(item.college==0 && item.department==0){

                              this.futureElectoralRooms.push(item);
                            }
                            else

                              if(item.college!=0 && item.college==this.currentUser.college)
                              {
                                if(item.department==0){

                                  this.futureElectoralRooms.push(item);
                                }
                                else if(item.department!=0 && item.department==this.currentUser.department)
                                {
                                  this.futureElectoralRooms.push(item);
                                }
                              }
                          });
                        },
                        error=>this.alertService.error(error.error.message));
  }

    getCandidates(idElectoralRoom:number){
        this.candidatesService.getAll(idElectoralRoom).pipe(first()).
                               subscribe(candidates=>{this.candidates=candidates;},
                                          error=>{this.alertService.error(error.error.message);});
    }

    openVoteDialog(idElectoralRoom:number){

      let d:number[]=[];
      d[0]=idElectoralRoom;
      d[1]=this.currentUser.idUser;

       let dialogRef=this.dialog.open(UserViewVote,{
         data:d
       });

       dialogRef.afterClosed().subscribe(()=>{

           this.hasVoted=true;
           this.alertService.success("Voted succesfully!");
       },
       error=>{
         this.alertService.error(error);
       });
    }
   
    ngOnInit(){

      this.getPast();
      this.getPresent();
      this.getFuture();

    }

    seeResults(er:number){

      this.dialog.open(ResultsDialog,{
        data:er
      })
      .afterClosed().subscribe(()=>{
        console.log("closed")
      },
      err=>{
        this.alertService.error(err);
      });

    }

}