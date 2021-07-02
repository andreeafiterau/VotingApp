import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { College } from "src/app/shared/models/college";
import { Department } from "src/app/shared/models/department";
import { DepartmentService } from "src/app/shared/services/admin/college-admin/department.service";
import { AlertService } from "src/app/shared/services/others/alert.service";

@Component({templateUrl:'./add-department.component.html'})

export class AddDepartmentDialog implements OnInit{

    addDepartmentForm:FormGroup;

    constructor(private formBuilder:FormBuilder,
                private departmentService:DepartmentService,
                @Inject(MAT_DIALOG_DATA) private data:College,
                private alertService:AlertService,
                private dialogRef:MatDialogRef<AddDepartmentDialog>){}

    ngOnInit(){

        this.addDepartmentForm=this.formBuilder.group({
            departmentName:['',Validators.required]
        });
    }

    addDepartment(){

        if(this.addDepartmentForm.invalid){
            return;
        }

        let dep=new Department();
        dep.departmentName=this.addDepartmentForm.controls.departmentName.value;
        dep.idCollege=this.data.idCollege;

        this.departmentService.add(dep).toPromise()
        .then((res)=>{
            this.dialogRef.close(res);
        })
        .catch(error=>{
            this.dialogRef.close(error.error.message);
        });
    }
}