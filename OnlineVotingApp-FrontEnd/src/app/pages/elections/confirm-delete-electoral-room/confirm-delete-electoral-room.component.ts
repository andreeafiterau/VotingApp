import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { College } from "src/app/shared/models/college";
import { Department } from "src/app/shared/models/department";
import { ElectionView } from "src/app/shared/models/election-view";
import { CollegeService } from "src/app/shared/services/admin/college-admin/college.service";
import { DepartmentService } from "src/app/shared/services/admin/college-admin/department.service";
import { ElectionService } from "src/app/shared/services/admin/elections-admin/election.service";
import { ElectoralRoomService } from "src/app/shared/services/admin/elections-admin/electoral-room.service";
@Component({templateUrl:"./confirm-delete-electoral-room.component.html"})

export class ConfirmDeleteElectoralRoom{

    constructor(@Inject(MAT_DIALOG_DATA)public data:ElectionView,
                private electionService:ElectoralRoomService,
                public dialogRef: MatDialogRef<ConfirmDeleteElectoralRoom>){}

    onYes(){

        this.electionService.delete(this.data.idElectoralRoom).toPromise()
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