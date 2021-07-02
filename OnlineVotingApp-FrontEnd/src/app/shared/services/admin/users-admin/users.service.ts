import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/shared/models/user';
import {UserFilter} from 'src/app/shared/models/users-filter';
import { UserAdminView } from 'src/app/shared/models/user-admin-view';
import { Role } from 'src/app/shared/models/role';
import { UserView } from 'src/app/pages/user-view/user-view.component';


@Injectable({ providedIn: 'root' })
export class UsersService {
    constructor(private http: HttpClient) { }

    getFiltered(userFilter:UserFilter) {
        return this.http.post<UserAdminView[]>(`http://localhost:4001/usersComponent/filter`,userFilter);
    }

    getAll() {
        return this.http.get<UserAdminView[]>(`http://localhost:4001/usersComponent/getAllUsers`);
    }

    getRoles(){
        return this.http.get<Role[]>(`http://localhost:4001/UsersComponent`);
    }

    add(userAdminView:UserAdminView){
        return this.http.post(`http://localhost:4001/usersComponent`,userAdminView);
    }
    
    update(userAdminView:UserAdminView, id:number){

        return this.http.put(`http://localhost:4001/usersComponent/${id}`,userAdminView);
    }

    delete(id:number){

        return this.http.delete(`http://localhost:4001/usersComponent/${id}`);
    }
}