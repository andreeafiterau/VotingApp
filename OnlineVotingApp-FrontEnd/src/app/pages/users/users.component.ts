import { SelectionModel } from "@angular/cdk/collections";
import { AfterViewInit, Component, OnInit, ViewChild, ɵSWITCH_CHANGE_DETECTOR_REF_FACTORY__POST_R3__ } from "@angular/core";
import { FormControl} from "@angular/forms";
import { MatDialog } from "@angular/material/dialog";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Subscription } from "rxjs";
import { filter, first } from "rxjs/operators";
import { College } from "src/app/shared/models/college";
import { Department } from "src/app/shared/models/department";
import { Role } from "src/app/shared/models/role";
import { User } from "src/app/shared/models/user";
import { UserAdminView } from "src/app/shared/models/user-admin-view";
import { UserAuthenticationView } from "src/app/shared/models/user-auth-view";
import { CollegeService } from "src/app/shared/services/admin/college-admin/college.service";
import { DepartmentService } from "src/app/shared/services/admin/college-admin/department.service";
import { UsersService } from "src/app/shared/services/admin/users-admin/users.service";
import { AlertService } from "src/app/shared/services/others/alert.service";
import { UserService } from "src/app/shared/services/user/user.service";
import { AddUser } from "./add-dialog/add-dialog.component";
import { ConfirmDeleteUsers } from "./confirm-delete-users/confirm-delete-user.component";
import { UpdateUser } from "./update-dialog/update-dialog.component";

@Component ({selector:'users',templateUrl:'./users.component.html',styleUrls:['./users.component.css']})

export class UsersComponent implements OnInit,AfterViewInit{

    currentUser: UserAuthenticationView=new UserAuthenticationView();
    currentUserSubscription: Subscription;

    roles:Role[]=[];
    departments:Department[]=[];
    colleges:College[]=[];

    role = new FormControl(-1);
    college = new FormControl(-1);
    department=new FormControl(-1);

    isReady=false;

    selectedOption:number;
    selectedRole:number;
    selectedDepartment:number;

    displayedColumns: string[] = ['username', 'firstName', 'lastName', 'email','edit','delete'];
    dataSource:MatTableDataSource<UserAdminView> = new MatTableDataSource<UserAdminView>();
    selection = new SelectionModel<UserAdminView>(true, []);
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    

    constructor(private userService:UsersService,
                private authenticationService: UserService,
                private alertService:AlertService,
                private departmentService:DepartmentService,
                private collegeService:CollegeService,
                public dialog: MatDialog){

        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;});
    }

    getRoles(){
        this.userService.getRoles().pipe(first()).subscribe(
            data=>this.roles=data,
            error=>console.log(error));
    }

    getDepartments(college:number){

        let coll=new College();
        coll.idCollege=college;
        console.log(this.selectedOption);
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
    }

    ngOnInit(){

       this.getRoles();
       this.getColleges();
       this.getAllUsers();
    }

    ngAfterViewInit() {
        this.dataSource.paginator = this.paginator;

    }
    

    getAllUsers(){      

         this.userService.getAll().pipe(first())
            .subscribe(res=>{

                this.dataSource= new MatTableDataSource<UserAdminView>();
                res.forEach(item=>{

                    this.dataSource.data.push(item);
                });
                console.log(this.dataSource.data);

                this.dataSource.paginator = this.paginator;
                this.dataSource.filterPredicate = (data:UserAdminView, filter: string) => {
         
                 const username=data.user.username.toLowerCase();
                 const firstName=data.user.firstName.toLowerCase();
                 const lastName=data.user.lastName.toLowerCase();
                 const email=data.user.email.toLowerCase();

                 let string=username+firstName+lastName+email;

                 if(string.includes(filter))

                  return true;
                 else
   
                 return false;
                }
               
            },
            error=>{
                this.alertService.error(error.error.message);
            });
            
   
        this.isReady=true;
        
    }


    openDialogAddUser(){
        let dialogRef=this.dialog.open(AddUser);

        dialogRef.afterClosed().subscribe((res)=>{
            
            this.getAllUsers();

            if(res){

                this.alertService.success("added succesfully");
            }
            

        },(error)=>{

            this.alertService.error(error.error.message);
        });
    }


    deleteUser(element:UserAdminView){

        let dialogRef=this.dialog.open(ConfirmDeleteUsers,{
            data:element
        });
        
       dialogRef.afterClosed().subscribe((res)=>{
           if(res){

            this.getAllUsers();
            this.alertService.success("Deleted succesfully");
           }
           
       },error=>{
           this.alertService.error(error.error.message);
       });
           
    }

    editUser(element:UserAdminView){

        let dialogRef=this.dialog.open(UpdateUser,{
                data: element
        });

        dialogRef.afterClosed().subscribe((result)=>{
            if(result){

                debugger
                console.log(result);
                this.getAllUsers();
                this.alertService.success("edited succesfully");
            }
               
        },(error)=>{
            this.alertService.error(error.error.message);
        });

    }

    applyFilter(filterValue:string) {

        console.log(this.dataSource.filter);
        
        this.dataSource.filter = filterValue.trim().toLowerCase();

        if (this.dataSource.paginator) {
          this.dataSource.paginator.firstPage();
        }
    }

}