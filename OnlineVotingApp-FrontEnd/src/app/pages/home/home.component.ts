import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { ElectoralRoom } from "src/app/shared/models/electoral-room";
import { User } from "src/app/shared/models/user";
import { ElectoralRoomService } from "src/app/shared/services/admin/elections-admin/electoral-room.service";
import { AlertService } from "src/app/shared/services/others/alert.service";
import { UserService } from "src/app/shared/services/user/user.service";

@Component({ templateUrl: 'home.component.html' })

export class HomeComponent implements OnInit {

    currentUser: User;
    currentUserSubscription: Subscription;

    electoralRooms:ElectoralRoom[]=[];

    constructor(
        private authenticationService: UserService,
        private router: Router,
        private electoralRoomService:ElectoralRoomService,
        private alertService:AlertService
        
    ) {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;
        });

        //console.log(document.getElementById("home-dropdown"));
    }

    getElectoralRooms(){
        this.electoralRoomService.
            getAll().pipe(first()).subscribe(
                electoralRooms=>{this.electoralRooms=electoralRooms; 
                                 console.log(this.electoralRooms);},
                error=>{this.alertService.error(error.error.message);
                        console.log(error);});
    }
   
    ngOnInit(){
        console.log(this.authenticationService.user);

        this.getElectoralRooms();

    }


}