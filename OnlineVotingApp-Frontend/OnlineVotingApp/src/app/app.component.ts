import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { User } from './models/user';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'OnlineVotingApp';
 
  currentUser: User;
  userActivity;
  userInactive: Subject<any> = new Subject();

  constructor(
      private router: Router,
      private authenticationService: AuthenticationService
  ) {
      this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
      //console.log(this.currentUser.isAdmin);
  }

  logout() {
      this.authenticationService.logout();
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
        if (this.authenticationService.currentUser) {
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
