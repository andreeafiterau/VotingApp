import { Component, OnInit, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from './shared/services/user/user.service';
import { User } from './shared/models/user';
import { Subject } from 'rxjs';

@Component({ selector: 'app-root', templateUrl: 'app.component.html'})

export class AppComponent implements OnInit{

    currentUser: User;
    userActivity;
    userInactive: Subject<any> = new Subject();

    constructor(
        private router: Router,
        private userService: UserService
    ) {
        this.userService.currentUser.subscribe(x => this.currentUser = x);
        //console.log(this.currentUser.isAdmin);
    }

    logout() {
        this.userService.logout();
        this.router.navigate(['/login']);
    }

   //if user is inactive will be logged out,we set a timeout on the moment of initialization
   ngOnInit(){

     this.setTimeout();
     this.userInactive.subscribe(() => {this.logout();}); 

   }

   //set timeout function
    setTimeout() {

        this.userActivity = setTimeout(() => {
          if (this.userService.currentUser) {
            this.userInactive.next(undefined);
            console.log('logged out');
          }
        },900000);
      }
    
    //if the mouse is active we refresh the state of the timeout
    @HostListener('window:mousemove') refreshUserState() {
        clearTimeout(this.userActivity);
        this.setTimeout();
    }
}