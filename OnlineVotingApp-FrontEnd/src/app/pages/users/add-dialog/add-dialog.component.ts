import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef } from "@angular/material/dialog";
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

@Component({templateUrl:'./add-dialog.component.html'})

export class AddUser implements OnInit{

    roles:Role[]=[];
    departments:Department[]=[];
    colleges:College[]=[];

    selectedRole:number;
    selectedCollege:number;
    selectedDepartment:number;

    addUserForm:FormGroup;

    constructor(private userService:UsersService,
        private roleService:UserService,
        private collegeService:CollegeService,
        private departmentService:DepartmentService,
        private alertService:AlertService,
        private formBuilder:FormBuilder,
        public dialogRef:MatDialogRef<AddUser>){

    }

    ngOnInit(){

        this.addUserForm=this.formBuilder.group({
            username:['',Validators.required],
            firstName:['',Validators.required],
            lastName:['',Validators.required],
            email:['',Validators.required]
            // nrMatricol:['',Validators.required]
        });

        this.getRoles();
        this.getColleges();
    }

    selectCollege($event){
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

    addUser(){

        if(this.addUserForm.invalid){
            return;
        }

        let userAdminView=new UserAdminView();

        userAdminView.user.username=this.addUserForm.controls.username.value;
        userAdminView.user.firstName=this.addUserForm.controls.firstName.value;
        userAdminView.user.lastName=this.addUserForm.controls.lastName.value;
        userAdminView.user.email=this.addUserForm.controls.email.value;
        //userAdminView.user.nrMatricol=this.addUserForm.controls.nrMatricol.value;

        userAdminView.role.idRole=this.selectedRole;

        let dept=new Department();
        dept.idDepartment=this.selectedDepartment;
        userAdminView.departments.push(dept);

        this.userService.add(userAdminView).toPromise()
        .then(res=>{
            this.dialogRef.close(res);
        })
        .catch(err=>{
            this.dialogRef.close(err);
        });
    }

    
}