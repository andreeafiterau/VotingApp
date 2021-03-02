import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './pages/login/login.component';
import { routing } from './app-routing.module';
import { HomeComponent } from './pages/home/home.component';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { SendActivationKeyComponent } from './pages/send-activation-key/send-activation-key.component';
import { ActivateAccountComponent } from './pages/activate-account/activate-account.component';
import { NavBarComponent } from './shared/components/nav-bar/nav-bar.component';
import { ChangePasswordComponent } from './pages/change-password/change-password.component';
import { AlertComponent } from './shared/components/alert/alert.component';

@NgModule({
  
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    routing
  ],
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    ForgotPasswordComponent,
    SendActivationKeyComponent,
    ActivateAccountComponent,
    NavBarComponent,
    ChangePasswordComponent,
    AlertComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
