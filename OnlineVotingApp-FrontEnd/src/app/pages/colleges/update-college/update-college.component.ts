import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { College } from "src/app/shared/models/college";
import { CollegeService } from "src/app/shared/services/admin/college-admin/college.service";
import { AlertService } from "src/app/shared/services/others/alert.service";

@Component({templateUrl:'./update-college.component.html'})

export class UpdateCollegeDialog implements OnInit{

    updateCollegeForm:FormGroup;

    constructor(private formBuilder:FormBuilder,
                private collegeService:CollegeService,
                @Inject(MAT_DIALOG_DATA) public data: College,
                public dialogRef:MatDialogRef<College>){

    }

    ngOnInit(){

        this.updateCollegeForm=this.formBuilder.group({
           collegeName:[this.data.collegeName,Validators.required] 
        });
    }

    updateCollege(){

        if(this.updateCollegeForm.invalid){
            return;
        }

        let college=new College();
        college.collegeName=this.updateCollegeForm.controls.collegeName.value;

        return this.collegeService.update(college,this.data.idCollege).toPromise()
        .then(result=>{
            this.dialogRef.close(result);
        })
        .catch(error=>{
            this.dialogRef.close(error);
        });
    }
}