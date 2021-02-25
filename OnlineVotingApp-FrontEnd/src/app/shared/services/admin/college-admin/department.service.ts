import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Department } from 'src/app/shared/models/department';


@Injectable({ providedIn: 'root' })
export class DepartmentService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Department[]>(`http://locahost:4001/department`);
    }

    add(department:Department){
        return this.http.post(`http://locahost:4001/department`,department);
    }
    
    update(department:Department, id:number){

        return this.http.put(`http://locahost:4001/department/${id}`,department);
    }

    delete(id:number){

        return this.http.delete(`http://locahost:4001/department/${id}`);
    }
}