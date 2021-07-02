import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChangePasswordComponent } from './pages/change-password/change-password.component';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { AccountActivationComponent} from './pages/activate-account/activate-account.component';
import { CollegeComponent } from './pages/colleges/colleges.component';
import { ElectoralRoomComponent } from './pages/elections/elections.component';
import { UsersComponent } from './pages/users/users.component';
import { UserView } from './pages/user-view/user-view.component';


const appRoutes: Routes = [
  { path: '', component: LoginComponent},
  { path: 'home', component: HomeComponent },
  { path: 'forgotPassword',component:ForgotPasswordComponent },
  { path: 'sendActivationKey',component:AccountActivationComponent},
  { path: 'changePassword', component:ChangePasswordComponent},
  { path: 'login', component:LoginComponent},
  { path: 'colleges', component:CollegeComponent},
  { path: 'elections', component:ElectoralRoomComponent},
  { path: 'users', component:UsersComponent},
  { path:'user-view',component:UserView},

  // otherwise redirect to home
  { path: '*', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes, { relativeLinkResolution: 'legacy' });


