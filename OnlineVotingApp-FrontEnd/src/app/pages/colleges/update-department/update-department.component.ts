import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { College } from "src/app/shared/models/college";
import { Department } from "src/app/shared/models/department";
import { DepartmentService } from "src/app/shared/services/admin/college-admin/department.service";
import { AlertService } from "src/app/shared/services/others/alert.service";

@Component({templateUrl:'./update-department.component.html'})

export class UpdateDepartmentDialog implements OnInit{

    updateDepartmentForm:FormGroup;

    constructor(private formBuilder:FormBuilder,
                private departmentService:DepartmentService,
                @Inject (MAT_DIALOG_DATA)public data:Department,
                private alertService:AlertService,
                private dialogRef:MatDialogRef<UpdateDepartmentDialog>){}

    ngOnInit(){

        this.updateDepartmentForm=this.formBuilder.group({
            departmentName:[this.data.departmentName,Validators.required]
        });
    }

    updateDepartment(){

        let dep=new Department();
        dep.departmentName=this.updateDepartmentForm.controls.departmentName.value;
        dep.idCollege=this.data.idCollege;

        this.departmentService.update(dep,this.data.idDepartment).toPromise()
        .then(result=>{
            this.dialogRef.close(result);
        })
        .catch(error=>{
            this.alertService.error(error.error.message);
        });
    }
}