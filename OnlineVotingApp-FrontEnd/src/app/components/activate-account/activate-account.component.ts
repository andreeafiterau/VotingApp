import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { first } from "rxjs/operators";
import { UserService } from "src/app/shared/services/user/user.service";

@Component({templateUrl: 'activate-account.component.html'})
export class ActivateAccountComponent implements OnInit {

    activateAccount: FormGroup;
    loading = false;
    submitted = false;
    differentPasswords=false;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private userService: UserService
    ) {}

    ngOnInit(){

        this.activateAccount = this.formBuilder.group({
            email: ['', Validators.required],
            activationKey: ['', Validators.required],
            password:['',Validators.required],
            confirmPassword:['',Validators.required]
        });
    }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.activateAccount.invalid) {
            return;
        }

        if(this.activateAccount.controls.password.value!=
            this.activateAccount.controls.confirmPassword.value){
               
             this.differentPasswords=true;
             return;

         }

        this.loading = true;

        console.log(this.activateAccount.controls.email.value);
        console.log(this.activateAccount.controls.activationKey.value);
        console.log(this.activateAccount.controls.password.value);
        
        this.userService.activateUser(this.activateAccount.controls.email.value,
                                      this.activateAccount.controls.activationKey.value,
                                      this.activateAccount.controls.password.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.router.navigate(['login']);
                },
                error => {
                    console.log(error);            
                    this.loading = false;
                });
    }
    
}