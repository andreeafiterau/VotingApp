import { Component, NgZone, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { College } from "src/app/shared/models/college";
import { Department } from "src/app/shared/models/department";
import { ElectionType } from "src/app/shared/models/election-type";
import { ElectionView } from "src/app/shared/models/election-view";
import { ElectoralRoom } from "src/app/shared/models/electoral-room";
import { CollegeService } from "src/app/shared/services/admin/college-admin/college.service";
import { DepartmentService } from "src/app/shared/services/admin/college-admin/department.service";
import { ElectionService } from "src/app/shared/services/admin/elections-admin/election.service";
import { ElectoralRoomService } from "src/app/shared/services/admin/elections-admin/electoral-room.service";
import { AlertService } from "src/app/shared/services/others/alert.service";

@Component({templateUrl:'./add-electoral-room.component.html'})

export class AddElectoralRoomDialog implements OnInit{

    showCollege=false;
    showDep=false;

    addElectoralRoomForm:FormGroup;

    selectedElectionType:number;
    selectedCollege:number;
    selectedDepartment:number;

    colleges:College[]=[];
    departments:Department[]=[];
    elections:ElectionType[]=[];

    constructor(private formBuilder:FormBuilder,
                private electionService:ElectionService,
                private alertService:AlertService,
                private electoralRoomService:ElectoralRoomService,
                private dialogRef:MatDialogRef<ElectionView>,
                private collegeService:CollegeService,
                private departmentService:DepartmentService){}

    getElectionTypes(){

        this.electionService.getElectionTypes().toPromise()
        .then(result=>{
            this.elections=result;
        })
        .catch(error=>{
            this.alertService.error(error);           
        });

    }

    getColleges(){

        this.collegeService.getAll().toPromise()
        .then(result=>{
            this.colleges=result;
        })
        .catch(error=>{
             console.log(error);
        });
    }

    onSelectionChange(event:any){

        this.getDepartments();
    }

    onSelectionChangeElection(event){

        this.showCollege=false;
        this.showDep=false;

        if(this.selectedElectionType==2 || this.selectedElectionType==5){
              this.showCollege=true;
        }

        if(this.selectedElectionType==5){
            this.showDep=true;
        }
    }

    getDepartments(){

        let c=new College();
        c.idCollege=this.selectedCollege;

        console.log(c);
        this.departmentService.getAll(c).toPromise()
        .then(result=>{
            this.departments=result;
        })
        .catch(error=>{
             console.log(error);
        });
    }


    ngOnInit(){

        this.addElectoralRoomForm=this.formBuilder.group({
            date:['',Validators.required]
        });

        this.getElectionTypes();
        this.getColleges();
        this.getDepartments();
    }

    addElectoralRoom(){

        let er=new ElectoralRoom();
        er.idElectionType=this.selectedElectionType;
        er.date=this.addElectoralRoomForm.controls.date.value;
        er.idCollege=this.selectedCollege;
        er.idDepartment=this.selectedDepartment;

        er.date=new Date(new Date(er.date).getTime()-new Date(er.date).getTimezoneOffset()*60*1000).toISOString();

        console.log(er);
        this.electoralRoomService.add(er).toPromise()
        .then((res)=>{            
            this.dialogRef.close(res);
        })
        .catch(error=>{
            this.dialogRef.close(error);
        });
    }
}