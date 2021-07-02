import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { College } from "src/app/shared/models/college";
import { Department } from "src/app/shared/models/department";
import { CollegeService } from "src/app/shared/services/admin/college-admin/college.service";
import { DepartmentService } from "src/app/shared/services/admin/college-admin/department.service";
@Component({templateUrl:"./confirm-delete-college.component.html"})

export class ConfirmDeleteCollege{

    constructor(@Inject(MAT_DIALOG_DATA)public data:College,
                private collegeService:CollegeService,
                public dialogRef: MatDialogRef<ConfirmDeleteCollege>){}

    onYes(){       

        this.collegeService.delete(this.data.idCollege).toPromise()
        .then(result=>{
          this.dialogRef.close(result);
        })
        .catch(error=>{
          this.dialogRef.close(error);
        });
    }

    onNo(){

        this.dialogRef.close();
    }
    
}