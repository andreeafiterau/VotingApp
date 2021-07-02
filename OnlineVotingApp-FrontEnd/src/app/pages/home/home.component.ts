import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ModalDismissReasons, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { Candidate } from "src/app/shared/models/candidate";
import { ElectoralRoom } from "src/app/shared/models/electoral-room";
import { Role } from "src/app/shared/models/role";
import { User } from "src/app/shared/models/user";
import { UserAdminView } from "src/app/shared/models/user-admin-view";
import { UserAuthenticationView } from "src/app/shared/models/user-auth-view";
import { CandidateService } from "src/app/shared/services/admin/elections-admin/candidate.service";
import { ElectoralRoomService } from "src/app/shared/services/admin/elections-admin/electoral-room.service";
import { AlertService } from "src/app/shared/services/others/alert.service";
import { UserService } from "src/app/shared/services/user/user.service";
import { VotingService } from "src/app/shared/services/user/voting.service";

@Component({ templateUrl: 'home.component.html' })

export class HomeComponent implements OnInit {

     currentUser:UserAuthenticationView=new UserAuthenticationView();
     currentUserSubscription: Subscription;
     isAdmin=false;

     role:Role=new Role();
    // //user:User=new User();
    

    // electoralRooms:ElectoralRoom[]=[];
    // candidates:Candidate[]=[];

    // closeResult: string;
  

    constructor(
        private authenticationService: UserService,
        private router: Router,
        private electoralRoomService:ElectoralRoomService,
        private candidatesService:CandidateService,
        private votingService:VotingService,
        private alertService:AlertService,
        private modalService: NgbModal
        
    ) {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user; console.log(this.currentUser);

        });

       

    }


    ngOnInit(){

      //this.getRole(this.currentUser.idUser);

      //console.log(this.role[0]);

      if(this.currentUser.role=='admin')
      {
        this.isAdmin=true;
      }

      //console.log(this.currentUser.role,this.isAdmin);
    }

    // getRole(idUser:number){

    //   this.authenticationService.getRole(idUser).pipe(first()).subscribe(
    //     role=>{this.role=role;
    //            console.log(this.role);},
    //     error=>this.alertService.error(error.error.message));
    // }

    // getElectoralRooms(){
    //     this.electoralRoomService.
    //         getAll().pipe(first()).subscribe(
    //             electoralRooms=>{this.electoralRooms=electoralRooms; 
    //                              console.log(this.electoralRooms);},
    //             error=>{this.alertService.error(error.error.message);
    //                     console.log(error);});
    // }

    // getCandidates(idElectoralRoom:number){
    //     this.candidatesService.getAll(idElectoralRoom).pipe(first()).
    //                            subscribe(candidates=>{this.candidates=candidates;},
    //                                       error=>{this.alertService.error(error.error.message);});
    // }

    // vote(index){

    //     let candidateId;

    //      for(let i=0;i<=this.candidates.length;i++)
    //      {
    //         if(i==index)
    //         {
    //             candidateId=this.candidates[i].idCandidate;
    //         }
    //      }

    //      console.log(this.currentUser.idUser,candidateId);

    //      this.votingService.vote(this.currentUser.idUser,candidateId).pipe(first()).subscribe(data=>{this.alertService.success("Voted succesfully")},
    //                                                                          error=>{this.alertService.error(error.error.message);});
    // }
   
    

    // open(content,idElectoralRoom) {

    //     this.getCandidates(idElectoralRoom);

    //     console.log(this.candidates);

    //     this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
    //       this.closeResult = `Closed with: ${result}`;
    //     }, (reason) => {
    //       this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    //     });

        
    //   }
      
    //   private getDismissReason(reason: any): string {
    //     if (reason === ModalDismissReasons.ESC) {
    //       return 'by pressing ESC';
    //     } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
    //       return 'by clicking on a backdrop';
    //     } else {
    //       return  `with: ${reason}`;
    //     }
    //   }


}