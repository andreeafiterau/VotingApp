import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Candidate } from 'src/app/models/candidate';


@Injectable({ providedIn: 'root' })
export class CandidateService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Candidate[]>(`http://locahost:4001/candidates`);
    }

    add(candidate:Candidate){
        return this.http.post(`http://locahost:4001/candidates`,candidate);
    }
    
    update(candidate:Candidate, id:number){

        return this.http.put(`http://locahost:4001/candidates/${id}`,candidate);
    }

    delete(id:number){

        return this.http.delete(`http://locahost:4001/candidates/${id}`);
    }
}