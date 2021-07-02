import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { first } from "rxjs/operators";
import { College } from "src/app/shared/models/college";
import { Department } from "src/app/shared/models/department";
import { Role } from "src/app/shared/models/role";
import { UserAdminView } from "src/app/shared/models/user-admin-view";
import { CollegeService } from "src/app/shared/services/admin/college-admin/college.service";
import { DepartmentService } from "src/app/shared/services/admin/college-admin/department.service";
import { UsersService } from "src/app/shared/services/admin/users-admin/users.service";
import { AlertService } from "src/app/shared/services/others/alert.service";
import { UserService } from "src/app/shared/services/user/user.service";
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({templateUrl:'./update-dialog.component.html'})

export class UpdateUser implements OnInit{

    roles:Role[]=[];
    departments:Department[]=[];
    colleges:College[]=[];

    selectedRole:number=this.data.role.idRole;
    selectedCollege:number=this.data.colleges[0].idCollege;
    selectedDepartment:number=this.data.departments[0].idDepartment;

    updateUserForm:FormGroup;

    constructor(private userService:UsersService,
        private collegeService:CollegeService,
        private departmentService:DepartmentService,
        private alertService:AlertService,
        private formBuilder:FormBuilder,
        @Inject(MAT_DIALOG_DATA) public data: UserAdminView,
        public dialogRef:MatDialogRef<UpdateUser>){

            console.log(this.data);

    }

    ngOnInit(){

       
        this.updateUserForm=this.formBuilder.group({
            username:[this.data.user.username,Validators.required],
            firstName:[this.data.user.firstName,Validators.required],
            lastName:[this.data.user.lastName,Validators.required],
            email:[this.data.user.email,Validators.required]
            // nrMatricol:[this.data.user.nrMatricol]
        });

        this.getRoles();
        this.getColleges();
        this.getDepartments(this.selectedCollege);
    }

    getDepartments(college:number){

        let coll=new College();
        coll.idCollege=college;
        this.departmentService.getAll(coll).pipe(first()).subscribe(
            data=>this.departments=data,
            error=>this.alertService.error(error.error.message));
    }

    getColleges(){
        this.collegeService.getAll().toPromise().then(res=>{
            this.colleges=res;
        })
        .catch(err=>{
            this.alertService.error(err);
        });
            //console.log(this.colleges);
    }

    getRoles(){
        this.userService.getRoles().pipe(first()).subscribe(
            data=>this.roles=data.filter(d=>d.idRole!=1),
            error=>console.log(error));
    }

    updateUser(){

        if(this.updateUserForm.invalid){

            console.log("invalid");
            return;
        }

        let userAdminView=new UserAdminView();
        userAdminView.user.username=this.updateUserForm.controls.username.value;
        userAdminView.user.firstName=this.updateUserForm.controls.firstName.value;
        userAdminView.user.lastName=this.updateUserForm.controls.lastName.value;
        userAdminView.user.email=this.updateUserForm.controls.email.value;
        //userAdminView.user.nrMatricol=this.updateUserForm.controls.nrMatricol.value;

        userAdminView.role.idRole=this.selectedRole;
        userAdminView.user.isAccountActive=this.data.user.isAccountActive;

        let dept=new Department();
        dept.idDepartment=this.selectedDepartment;
        userAdminView.departments.push(dept);

        console.log(userAdminView,this.data.user.idUser);

        return this.userService.update(userAdminView,this.data.user.idUser).toPromise()
        .then(res=>{
            this.dialogRef.close("updated");
        })
        .catch(err=>{
            this.dialogRef.close(err);
        });
    }

    updateUserBtn(){
        if(this.updateUserForm.invalid){
            return;
        }
        this.updateUser();
    }

}

