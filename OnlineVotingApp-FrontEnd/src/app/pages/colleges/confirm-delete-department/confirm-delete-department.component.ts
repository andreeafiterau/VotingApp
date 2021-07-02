import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Department } from "src/app/shared/models/department";
import { DepartmentService } from "src/app/shared/services/admin/college-admin/department.service";
@Component({templateUrl:"./confirm-delete-department.component.html"})

export class ConfirmDeleteDepartment{

    constructor(@Inject(MAT_DIALOG_DATA)public data:Department,
                private depService:DepartmentService,
                public dialogRef: MatDialogRef<ConfirmDeleteDepartment>){}

    onYes(){

        this.depService.delete(this.data.idDepartment).toPromise()
        .then(result=>{
            this.dialogRef.close(result);
        })
        .catch(error=>{
            this.dialogRef.close(error);
        });
    }

    onNo(){
        this.dialogRef.close(false);
    }
    
}