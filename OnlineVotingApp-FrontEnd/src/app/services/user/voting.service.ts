import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Vote } from 'src/app/models/vote';


@Injectable({ providedIn: 'root' })
export class VotingService {
    constructor(private http: HttpClient) { }

    vote(vote:Vote){

        return this.http.post(`http://localhost:4001/voting/vote`,vote);
    }

    getResults(id:number){

        return this.http.post(`http://localhost:4001/voting/${id}`,null);
    }
}