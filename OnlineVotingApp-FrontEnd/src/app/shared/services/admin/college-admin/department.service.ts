import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Department } from 'src/app/shared/models/department';
import { College } from 'src/app/shared/models/college';


@Injectable({ providedIn: 'root' })
export class DepartmentService {
    constructor(private http: HttpClient) { }

    getAll(college:College) {
        return this.http.post<Department[]>(`http://localhost:4001/department/getDepartmentsByCollegeId`,college);
    }

    add(department:Department){
        return this.http.post(`http://localhost:4001/department`,department);
    }
    
    update(department:Department, id:number){

        return this.http.put(`http://localhost:4001/department/${id}`,department);
    }

    delete(id:number){

        return this.http.delete(`http://localhost:4001/department/${id}`);
    }
}