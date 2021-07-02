import { animate, state, style, transition, trigger } from "@angular/animations";
import { SelectionModel } from "@angular/cdk/collections";
import { Component, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { MatDialog } from "@angular/material/dialog";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { College } from "src/app/shared/models/college";
import { Department } from "src/app/shared/models/department";
import { CollegeService } from "src/app/shared/services/admin/college-admin/college.service";
import { DepartmentService } from "src/app/shared/services/admin/college-admin/department.service";
import { AlertService } from "src/app/shared/services/others/alert.service";
import { AddCollegeDialog } from "./add-college/add-college.component";
import { AddDepartmentDialog } from "./add-department/add-department.component";
import { ConfirmDeleteCollege } from "./confirm-delete-college/confirm-delete-college.component";
import { ConfirmDeleteDepartment } from "./confirm-delete-department/confirm-delete-department.component";
import { UpdateCollegeDialog } from "./update-college/update-college.component";
import { UpdateDepartmentDialog } from "./update-department/update-department.component";

@Component({selector:'colleges',templateUrl:'colleges.component.html',styleUrls:['./colleges.component.css'],
animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})

export class CollegeComponent implements OnInit{

    //for colleges
    displayedColumnsCollege: string[] = ['collegeName','edit','delete'];
    dataSourceCollege = new MatTableDataSource<College>();
    selectionCollege = new SelectionModel<College>(true, []);
    isReadyCollege=false;
    @ViewChild(MatPaginator) paginator: MatPaginator;

    //for departments
    displayedColumnsDepartment: string[] = ['departmentName','edit','delete'];
    dataSourceDepartment = new MatTableDataSource<Department>();
    selectionDepartment = new SelectionModel<Department>(true, []);
    isReadyDepartment=false;
    
    expandedElement: Department|null=null;

    colleges:College[]=[];
    departments:Department[]=[];

    selectedCollege:number;

    constructor(private alertService:AlertService,
                private collegeService:CollegeService,
                private departmentService:DepartmentService,
                private dialog:MatDialog) {}

    ngOnInit(){
      this.getColleges();
    }

    ngAfterViewInit() {
      this.dataSourceCollege.paginator = this.paginator;

    }

    getColleges()
    {
      this.dataSourceCollege = new MatTableDataSource<College>();
      this.selectionCollege = new SelectionModel<College>(true, []);

        this.collegeService.getAll().toPromise()
        .then(result=>{          
         
          this.dataSourceCollege.data=result;
          
          for(let i=0;i<result.length;i++){

            this.departmentService.getAll(result[i]).toPromise()
            .then(result=>{
              this.dataSourceCollege.data[i].departments=result;
            })
            .catch(error=>{
              this.alertService.error(error);
            });

          }

          this.isReadyDepartment=true;
           
        })
        .catch(error=>{
          this.alertService.error(error);
        })

        this.isReadyCollege=true;
    }

    getRecord(row){

        this.dataSourceDepartment=new MatTableDataSource<Department>();
        this.selectionDepartment = new SelectionModel<Department>(true, []);
        this.isReadyDepartment=false;

        this.dataSourceDepartment.data=this.dataSourceCollege.data.find(c=>c.idCollege==row.idCollege).departments;

        this.isReadyDepartment=true;
    }

      openAddCollegeDialog(){
        
        let dialogRef=this.dialog.open(AddCollegeDialog);
        dialogRef.afterClosed().subscribe((result)=>{
         
          if(result){

            this.alertService.success("added succesfully");
          this.getColleges();
          }

          
          
        },error=>{
          this.alertService.error(error.error.message);
        });
      }

      openUpdateCollegeDialog(element:College){

          let dialogRef=this.dialog.open(UpdateCollegeDialog,{
              data: element
          });
        
        dialogRef.afterClosed().subscribe((result)=>{

          if(result){
         
          this.alertService.success("updated succesfully!");
          this.getColleges();
          }
        
        },error=>{
          this.alertService.error(error.error.message);
        });
      }

      deleteCollege(element:College){

        let dialogRef=this.dialog.open(ConfirmDeleteCollege,{
          data:element
        });

        dialogRef.afterClosed().subscribe(result=>{

          if(result){
        
          this.getColleges();
          this.alertService.success("deleted succesfully");
          
         }
        },error=>{
          this.alertService.error(error.error.message);
        });

      }

      deleteDepartment(row:Department){

         let dialogRef=this.dialog.open(ConfirmDeleteDepartment,{
           data:row
         });

         dialogRef.afterClosed().subscribe(result=>{

          debugger
           if(result){
         
           this.getColleges();
           this.alertService.success("deleted succesfully");
           
          }
         
         },error=>{
           this.alertService.error(error.error.message);
         });
      }

      editDepartment(element:Department){

        let dialogRef=this.dialog.open(UpdateDepartmentDialog,{
          data:element
        });

        dialogRef.afterClosed().subscribe((result)=>{

          if(result){

            this.alertService.success("edited succesfully");
            this.getColleges();
          }
                   
        },
        error=>{
          this.alertService.error(error.error.message);
        });

      }

      openAddDepartmentDialog(row:College){

        let dialogRef=this.dialog.open(AddDepartmentDialog,{
          data:row
        });

        dialogRef.afterClosed().subscribe((result)=>{
          if(result){

            this.getColleges();
            this.alertService.success("department added succesfully");   
          }
             
        },error=>{
          this.alertService.error(error.error.message);
        });
      }
}