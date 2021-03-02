import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { User } from "src/app/shared/models/user";
import { UserService } from "src/app/shared/services/user/user.service";

@Component({templateUrl: 'change-password.component.html'})

export class ChangePasswordComponent implements OnInit{

    changePasswordForm: FormGroup;
    loading = false;
    submitted = false;
    differentPasswords = false;
    currentUser: User;
    currentUserSubscription: Subscription;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private userService: UserService
    ) 
    {this.currentUserSubscription = this.userService.currentUser.subscribe(user => {
        this.currentUser = user;});}
    

    ngOnInit(){
        this.changePasswordForm = this.formBuilder.group({
            newPassword: ['', Validators.required],
            confirmedPassword:['',Validators.required]
        });
    
        console.log(this.currentUser.username);
    }

  
    onSubmit() {
        this.submitted = true;
    
        // stop here if form is invalid
        if (this.changePasswordForm.invalid) {
                return;
        }

        if(this.changePasswordForm.controls.newPassword.value!=
               this.changePasswordForm.controls.confirmedPassword.value){
                  
                this.differentPasswords=true;
                return;

        }
    
        this.loading = true;

        console.log(this.currentUser.username);
            
        this.userService.changePassword(this.currentUser.username,this.changePasswordForm.controls.newPassword.value).
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