import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { College } from 'src/app/models/college';


@Injectable({ providedIn: 'root' })
export class CollegeService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<College[]>(`http://locahost:4001/college`);
    }

    add(college:College){
        return this.http.post(`http://locahost:4001/college`,college);
    }
    
    update(college:College, id:number){

        return this.http.put(`http://locahost:4001/college/${id}`,college);
    }

    delete(id:number){

        return this.http.delete(`http://locahost:4001/college/${id}`);
    }
}