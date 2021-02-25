import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { first } from "rxjs/operators";
import { UserService } from "src/app/shared/services/user/user.service";

@Component({templateUrl: 'forgot-password.component.html'})

export class ForgotPasswordComponent implements OnInit{

    forgotPasswordForm: FormGroup;
    loading = false;
    submitted = false;
    differentPasswords = false;
    showInput=false;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private userService: UserService
    ) {}
    

    ngOnInit(){
        this.forgotPasswordForm = this.formBuilder.group({
            email: ['', Validators.required],
            token:['',Validators.required],
            newPassword: ['', Validators.required],
            confirmedPassword:['',Validators.required]
        });}

    sendToken(){

        this.userService.sendPasswordToken(this.forgotPasswordForm.controls.email.value).pipe(first()).
                         subscribe(data=>this.showInput=true, error=>console.log(error));
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
                                        },
                                                  error => {
                                            console.log(error);            
                                            this.loading = false;
                                            });
        }
}