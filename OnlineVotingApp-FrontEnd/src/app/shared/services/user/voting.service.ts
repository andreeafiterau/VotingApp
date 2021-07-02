import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Vote } from 'src/app/shared/models/vote';
import { ElectionViewForUser } from '../../models/election-view-for-user';
import { ElectoralRoom } from '../../models/electoral-room';
import { Result } from '../../models/result';


@Injectable({ providedIn: 'root' })
export class VotingService {
    constructor(private http: HttpClient) { }

    vote(idUser:number,idCandidate){

        return this.http.post(`http://localhost:4001/voting/vote`,{idUser,idCandidate});
    }

    getResults(id:number){

        return this.http.post<Result[]>(`http://localhost:4001/voting/${id}`,null);
    }

    getPresentElectoralRooms(idUser:number){

        return this.http.post<ElectionViewForUser[]>(`http://localhost:4001/election/getPresent`,{idUser});
    }

    getPastElectoralRooms(idUser:number){

        return this.http.post<ElectionViewForUser[]>(`http://localhost:4001/election/getPast`,{idUser});
    }

    getFutureElectoralRooms(idUser:number){

        return this.http.post<ElectionViewForUser[]>(`http://localhost:4001/election/getFuture`,{idUser});
    }
}