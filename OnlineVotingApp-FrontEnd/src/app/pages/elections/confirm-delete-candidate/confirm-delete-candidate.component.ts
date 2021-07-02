import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Candidate } from "src/app/shared/models/candidate";
import { Department } from "src/app/shared/models/department";
import { DepartmentService } from "src/app/shared/services/admin/college-admin/department.service";
import { CandidateService } from "src/app/shared/services/admin/elections-admin/candidate.service";
@Component({templateUrl:"./confirm-delete-candidate.component.html"})

export class ConfirmDeleteCandidate{

    constructor(@Inject(MAT_DIALOG_DATA)public data:Candidate,
                private candidateService:CandidateService,
                public dialogRef: MatDialogRef<ConfirmDeleteCandidate>){}

    onYes(){

        this.candidateService.delete(this.data.idCandidate).toPromise()
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