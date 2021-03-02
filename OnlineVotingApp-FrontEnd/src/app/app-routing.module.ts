import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ActivateAccountComponent } from './pages/activate-account/activate-account.component';
import { ChangePasswordComponent } from './pages/change-password/change-password.component';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { SendActivationKeyComponent } from './pages/send-activation-key/send-activation-key.component';


const appRoutes: Routes = [
  { path: '', component: LoginComponent},
  { path: 'home', component: HomeComponent },
  { path: 'forgotPassword',component:ForgotPasswordComponent },
  { path: 'sendActivationKey',component:SendActivationKeyComponent},
  { path: 'activateAccount', component:ActivateAccountComponent},
  { path: 'changePassword', component:ChangePasswordComponent},
  { path: 'login', component:LoginComponent},

  // otherwise redirect to home
  { path: '*', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);


