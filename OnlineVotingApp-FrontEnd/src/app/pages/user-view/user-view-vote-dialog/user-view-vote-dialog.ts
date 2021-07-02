import { Component, Inject, OnInit } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { first } from "rxjs/operators";
import { Candidate } from "src/app/shared/models/candidate";
import { CandidateService } from "src/app/shared/services/admin/elections-admin/candidate.service";
import { VotingService } from "src/app/shared/services/user/voting.service";

@Component({templateUrl:'./user-view-vote-dialog.html'})

export class UserViewVote implements OnInit{

    constructor(private candidateService:CandidateService,
        @Inject(MAT_DIALOG_DATA) public data: number[],
        private votingService:VotingService,
        private dialogRef:MatDialogRef<UserViewVote>){}

    selectedCandidate:Candidate=new Candidate();

    candidates:Candidate[]=[];

    getCandidates(idElectoralRoom:number){
        this.candidateService.getAll(idElectoralRoom).pipe(first()).
                               subscribe(candidates=>{this.candidates=candidates;});
    }

    ngOnInit(){
        this.getCandidates(this.data[0]);
    }

    vote(){

        this.votingService.vote(this.data[1],this.selectedCandidate.idCandidate)
        .pipe(first())
        .subscribe(data=>{
            this.dialogRef.close(data);
        },
         error=>{
             this.dialogRef.close(error);
        });
    }

   
}