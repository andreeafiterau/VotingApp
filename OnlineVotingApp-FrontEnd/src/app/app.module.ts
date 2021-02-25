import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './components/login/login.component';
import { routing } from './app-routing.module';
import { HomeComponent } from './components/home/home.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { SendActivationKeyComponent } from './components/send-activation-key/send-activation-key.component';
import { ActivateAccountComponent } from './components/activate-account/activate-account.component';

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
    ActivateAccountComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
