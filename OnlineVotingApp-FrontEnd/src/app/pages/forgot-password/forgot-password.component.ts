import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { first } from "rxjs/operators";
import { AlertComponent } from "src/app/shared/components/alert/alert.component";
import { AlertService } from "src/app/shared/services/others/alert.service";
import { UserService } from "src/app/shared/services/user/user.service";

@Component({templateUrl: 'forgot-password.component.html',styleUrls:['./forgot-password.component.css']})

export class ForgotPasswordComponent implements OnInit{

    forgotPasswordForm: FormGroup;
    loading = false;
    submitted = false;
    differentPasswords = false;
    showInput=false;
    disableEmail=false;
    hideSendTokenButton=true;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private userService: UserService,
        private alertService:AlertService
    ) {}
    

    ngOnInit(){
        this.forgotPasswordForm = this.formBuilder.group({
            email: ['', Validators.required],
            token:['',Validators.required],
            newPassword: ['', Validators.required],
            confirmedPassword:['',Validators.required]
        });}

    sendToken(){

        this.submitted=true;

        if(this.forgotPasswordForm.controls.email.invalid)
        {
            return;
        }

        this.userService.sendPasswordToken(this.forgotPasswordForm.controls.email.value).pipe(first()).
                         subscribe(data=>{this.showInput=true;this.hideSendTokenButton=false;}, error=>this.alertService.error(error.error.message));

        this.disableEmail=true;
        this.submitted=false;   
    }

    onSubmit() {
        this.submitted = true;
    
        // stop here if form is invalid
        if (this.forgotPasswordForm.invalid) {
                return;
        }

        if(this.forgotPasswordForm.controls.newPassword.value!=
               this.forgotPasswordForm.controls.confirmedPassword.value){
                  
                this.differentPasswords=true;
                return;

        }
    
        this.loading = true;
            
        this.userService.forgotPassword(this.forgotPasswordForm.controls.email.value,
                                        this.forgotPasswordForm.controls.newPassword.value,
                                        this.forgotPasswordForm.controls.token.value).
                                        pipe(first()).
                                        subscribe(data => {
                                            this.router.navigate(['login']);
                                            this.alertService.success("The password has been succesfully changed");
                                        },
                                                  error => {
                                            this.alertService.error(error.error.message);
                                                      
                                            this.loading = false;
                                            });
        }
}