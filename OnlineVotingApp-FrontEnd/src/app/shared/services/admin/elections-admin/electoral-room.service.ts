import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ElectoralRoom } from 'src/app/shared/models/electoral-room';


@Injectable({ providedIn: 'root' })
export class DepartmentService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<ElectoralRoom[]>(`http://locahost:4001/electoralRoom`);
    }

    add(electoralRoom:ElectoralRoom){
        return this.http.post(`http://locahost:4001/electoralRoom`,electoralRoom);
    }
    
    update(electoralRoom:ElectoralRoom, id:number){

        return this.http.put(`http://locahost:4001/electoralRoom/${id}`,electoralRoom);
    }

    delete(id:number){

        return this.http.delete(`http://locahost:4001/electoralRoom/${id}`);
    }
}