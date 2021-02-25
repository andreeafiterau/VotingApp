import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ActivateAccountComponent } from './components/activate-account/activate-account.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { SendActivationKeyComponent } from './components/send-activation-key/send-activation-key.component';


const appRoutes: Routes = [
  { path: '', component: LoginComponent},
  { path: 'home', component: HomeComponent },
  { path:'forgotPassword',component:ForgotPasswordComponent },
  { path:'sendActivationKey',component:SendActivationKeyComponent},
  { path: 'activateAccount', component:ActivateAccountComponent},

  // otherwise redirect to home
  { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);


