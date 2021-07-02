import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ElectoralRoom } from 'src/app/shared/models/electoral-room';
import { ElectionView } from 'src/app/shared/models/election-view';


@Injectable({ providedIn: 'root' })
export class ElectoralRoomService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<ElectionView[]>(`http://localhost:4001/electoralRoom`);
    }

    add(electoralRoom:ElectoralRoom){
        return this.http.post(`http://localhost:4001/electoralRoom`,electoralRoom);
    }
    
    update(electoralRoom:ElectoralRoom, id:number){

        return this.http.put(`http://localhost:4001/electoralRoom/${id}`,electoralRoom);
    }

    delete(id:number){

        return this.http.delete(`http://localhost:4001/electoralRoom/${id}`);
    }
}