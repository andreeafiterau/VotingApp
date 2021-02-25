import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/shared/models/user';
import {UserFilter} from 'src/app/shared/models/users-filter';
import { UserAdminView } from 'src/app/shared/models/user-admin-view';


@Injectable({ providedIn: 'root' })
export class UsersService {
    constructor(private http: HttpClient) { }

    getAll(userFilter:UserFilter) {
        return this.http.post<User[]>(`http://locahost:4001/userComponent/filter`,userFilter);
    }

    add(userAdminView:UserAdminView){
        return this.http.post(`http://locahost:4001/userComponent`,userAdminView);
    }
    
    update(userAdminView:UserAdminView, id:number){

        return this.http.put(`http://locahost:4001/userComponent/${id}`,userAdminView);
    }

    delete(id:number){

        return this.http.delete(`http://locahost:4001/userComponent/${id}`);
    }
}