import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Candidate } from 'src/app/shared/models/candidate';


@Injectable({ providedIn: 'root' })
export class CandidateService {
    constructor(private http: HttpClient) { }

    getAll(idElectoralRoom:number) {    
        return this.http.get<Candidate[]>(`http://localhost:4001/candidates/${idElectoralRoom}`);
    }

    getPossibleCandidates(candidates:Candidate[]){
        return this.http.post<Candidate[]>(`http://localhost:4001/candidates/getPossibleCandidates`,candidates);
    }

    add(candidate:Candidate){
        return this.http.post(`http://localhost:4001/candidates`,candidate);
    }
    
    update(candidate:Candidate, id:number){

        return this.http.put(`http://localhost:4001/candidates/${id}`,candidate);
    }

    delete(id:number){

        return this.http.delete(`http://localhost:4001/candidates/${id}`);
    }
}