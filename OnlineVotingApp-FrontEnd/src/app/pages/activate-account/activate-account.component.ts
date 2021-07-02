import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { first } from "rxjs/operators";
import { AlertService } from "src/app/shared/services/others/alert.service";
import { UserService } from "src/app/shared/services/user/user.service";

@Component({templateUrl: 'activate-account.component.html',styleUrls:['./activate-account.component.css']})
export class AccountActivationComponent implements OnInit {

    activationForm: FormGroup;
    submitted = false;
    sendActivationCodeButton=true;
    showInputs=false;
    disableEmail=false;
    differentPasswords=false;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private userService: UserService,
        private alertService:AlertService
    ) {}

    ngOnInit(){

        this.activationForm = this.formBuilder.group({
            email: ['', Validators.required],
            activationKey: ['', Validators.required],
            password:['',Validators.required],
            confirmPassword:['',Validators.required]
        });
    }

    sendActivationCode() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.activationForm.controls.email.invalid) {
            return;
        }



        this.userService.sendActivationCode(this.activationForm.controls.email.value)
            .pipe(first())
            .subscribe(
                data => {
                    //this.alertService.success("Email was sent succesfully");
                    this.sendActivationCodeButton=false;
                    this.showInputs=true;
                    this.disableEmail=true;
                    
                },
                error => {
                    this.alertService.error(error.error.message);
                    console.log(error);        
                });

        this.submitted=false;
    }

    activateAccount(){

        this.submitted=true;
        console.log("activ");

        if (this.activationForm.controls.activationKey.invalid || 
            this.activationForm.controls.password.invalid || 
            this.activationForm.controls.confirmPassword.invalid) {
            console.log("invalid");
            return;
        }

           
        if(this.activationForm.controls.password.value!=
            this.activationForm.controls.confirmPassword.value){
               
             this.differentPasswords=true;
             console.log("diff pass");
             return;

            }


            this.userService.activateUser(this.activationForm.controls.email.value,
                this.activationForm.controls.activationKey.value,
                this.activationForm.controls.password.value).pipe(first()).subscribe(
                                    data => {this.router.navigate(['login']);this.alertService.success("User has been activated succesfully");},
                                    error =>{this.alertService.error(error.error.message);});
    }
    
}