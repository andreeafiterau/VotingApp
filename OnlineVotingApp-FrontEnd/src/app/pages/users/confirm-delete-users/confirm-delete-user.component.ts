import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { UserAdminView } from "src/app/shared/models/user-admin-view";
import { UsersService } from "src/app/shared/services/admin/users-admin/users.service";
import { UserService } from "src/app/shared/services/user/user.service";

@Component({templateUrl:"./confirm-delete-users.component.html"})

export class ConfirmDeleteUsers{

    constructor(@Inject(MAT_DIALOG_DATA)public data:UserAdminView,
                private userService:UsersService,
                public dialogRef: MatDialogRef<ConfirmDeleteUsers>){}

    onYes(){
        this.userService.delete(this.data.user.idUser).toPromise()
        .then(res=>
            {this.dialogRef.close(res);})
        .catch(err=>this.dialogRef.close(err));

    }

    onNo(){
        this.dialogRef.close();
    }
    
}