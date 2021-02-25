import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { first } from "rxjs/operators";
import { UserService } from "src/app/shared/services/user/user.service";

@Component({templateUrl: 'send-activation-key.component.html'})
export class SendActivationKeyComponent implements OnInit {

    sendActivationForm: FormGroup;
    loading = false;
    submitted = false;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private userService: UserService
    ) {}

    ngOnInit(){

        this.sendActivationForm = this.formBuilder.group({
            email: ['', Validators.required]
        });
    }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.sendActivationForm.invalid) {
            return;
        }

        this.loading = true;
        this.userService.sendActivationCode(this.sendActivationForm.controls.email.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.router.navigate(['activateAccount']);
                },
                error => {
                    console.log(error);            
                    this.loading = false;
                });
    }
    
}