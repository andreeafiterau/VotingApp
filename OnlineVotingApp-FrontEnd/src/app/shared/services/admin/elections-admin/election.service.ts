import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/shared/models/user';
import { UserFilter } from 'src/app/shared/models/users-filter';


@Injectable({ providedIn: 'root' })
export class ElectionService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<User[]>(`http://locahost:4001/election`);
    }

    add(userFilter:UserFilter,idElectoralRoom:number){
        return this.http.post(`http://locahost:4001/election/${idElectoralRoom}`,userFilter);
    }
    
}