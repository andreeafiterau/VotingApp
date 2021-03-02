import { Component, OnInit } from "@angular/core";
import { UserService } from "../../services/user/user.service";

@Component({selector:'nav-bar',templateUrl: 'nav-bar.component.html'})
export class NavBarComponent implements OnInit {

    constructor(private userService:UserService){}

    ngOnInit(){}

    logout(){
       this.userService.logout();
    }
}