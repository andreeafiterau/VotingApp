import { Component, OnInit } from "@angular/core";
import { Subscription } from "rxjs";
import { UserAdminView } from "../../models/user-admin-view";
import { UserAuthenticationView } from "../../models/user-auth-view";
import { UserService } from "../../services/user/user.service";

@Component({selector:'nav-bar',templateUrl: 'nav-bar.component.html'})
export class NavBarComponent implements OnInit {

    currentUser: UserAuthenticationView=new UserAuthenticationView();
    currentUserSubscription: Subscription;

    isAdmin=false;

    constructor(private userService:UserService){
       
        this.currentUserSubscription = this.userService.currentUser.subscribe(user => {
            this.currentUser = user;});

        if(this.currentUser.role=='admin'){
            this.isAdmin=true;
        }
    }

    ngOnInit(){}

    logout(){
       this.userService.logout();
    }
}