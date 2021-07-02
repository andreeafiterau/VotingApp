import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef } from "@angular/material/dialog";
import { College } from "src/app/shared/models/college";
import { CollegeService } from "src/app/shared/services/admin/college-admin/college.service";
import { AlertService } from "src/app/shared/services/others/alert.service";

@Component({templateUrl:'./add-college.component.html'})

export class AddCollegeDialog implements OnInit{

    addCollegeForm:FormGroup;

    constructor(private formBuilder:FormBuilder,
                private collegeService:CollegeService,
                private dialogRef:MatDialogRef<College>,
                private alertService:AlertService){}

    ngOnInit(){

        this.addCollegeForm=this.formBuilder.group({
            collegeName:['',Validators.required]
        });
    }

    addCollege(){

        if(this.addCollegeForm.invalid){
            return;
        }

        let college=new College();
        college.collegeName=this.addCollegeForm.controls.collegeName.value;

        this.collegeService.add(college).toPromise()
        .then((res)=>{
            this.dialogRef.close(res);
        })
        .catch(error=>{
            this.dialogRef.close(error.error.message);
        });
       
    }

}