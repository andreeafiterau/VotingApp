import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { connectableObservableDescriptor } from "rxjs/internal/observable/ConnectableObservable";
import { Candidate } from "src/app/shared/models/candidate";
import { ElectionView } from "src/app/shared/models/election-view";
import { ElectoralRoom } from "src/app/shared/models/electoral-room";
import { User } from "src/app/shared/models/user";
import { UserAdminView } from "src/app/shared/models/user-admin-view";
import { CandidateService } from "src/app/shared/services/admin/elections-admin/candidate.service";
import { UsersService } from "src/app/shared/services/admin/users-admin/users.service";
import { AlertService } from "src/app/shared/services/others/alert.service";
import { UserService } from "src/app/shared/services/user/user.service";

@Component({templateUrl:'./add-candidate.component.html'})

export class AddCandidateDialog implements OnInit{

    addCandidateForm:FormGroup;

    selectedUser:UserAdminView=new UserAdminView();
    users:UserAdminView[]=[];

    noCandidates=false;

    constructor(private formBuilder:FormBuilder,
                private candidateService:CandidateService,
                @Inject(MAT_DIALOG_DATA) public data: ElectionView,
                private userService:UsersService,
                private alertService:AlertService,
                private dialogRef:MatDialogRef<AddCandidateDialog>){}

    ngOnInit(){

        this.addCandidateForm=this.formBuilder.group({
            selectName:['',Validators.required]
        });

        this.getUsers();
    }

    getUsers(){

        this.userService.getAll().toPromise()
        .then(result=>{

            console.log(result);
            result.forEach(item=>{

                if(this.data.electionName=='alegere rector' || this.data.electionName=='alegere director senat'){
                    this.users.push(item);
                    return;
                }

                if(this.data.electionName=='alegere decan'){

                    if(this.data.college==item.colleges[0].collegeName){
                        this.users.push(item);
                        return;
                    }
                }

                if(this.data.electionName=='alegere director departament'){

                    if(this.data.college==item.colleges[0].collegeName && this.data.department==item.departments[0].departmentName){
                        this.users.push(item);
                        return;
                    }
                }

                // if(item.colleges[0].idCollege==0 && item.departments[0].idDepartment==0){
                //     this.users.push(item);
                // }
                // else if(item.colleges[0].idCollege!=0 && item.colleges[0].collegeName==this.data.college){

                //     if(item.departments[0].idDepartment==0){
                //         this.users.push(item);
                //     }
                //     else if(item.departments[0].idDepartment!=0 && item.departments[0].departmentName==this.data.department){
                //         this.users.push(item);
                //     }
                // }
              
            });

            if(this.users.length==0){
                this.noCandidates=true;
            }
        })
        .catch(error=>{
            this.alertService.error(error);
        });

        
    }

    addCandidate(){

        if(this.addCandidateForm.invalid){
            return;
        }

        let candidate=new Candidate();
        candidate.idUser=this.selectedUser.user.idUser;
        candidate.firstName=this.selectedUser.user.firstName;
        candidate.lastName=this.selectedUser.user.lastName;
        candidate.idElectoralRoom=this.data.idElectoralRoom;

        this.candidateService.add(candidate).toPromise()
        .then((res)=>{
            this.dialogRef.close(res);
        })
        .catch(error=>{
            this.dialogRef.close(error);
        });
    }

}