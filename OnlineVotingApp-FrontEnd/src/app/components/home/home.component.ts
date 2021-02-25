import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { User } from "src/app/shared/models/user";
import { UserService } from "src/app/shared/services/user/user.service";

@Component({ templateUrl: 'home.component.html' })

export class HomeComponent implements OnInit {

    currentUser: User;
    currentUserSubscription: Subscription;

    constructor(
        private authenticationService: UserService,
        private router: Router,
        
    ) {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;
        });

        //console.log(document.getElementById("home-dropdown"));
    }
   
    ngOnInit(){}
}