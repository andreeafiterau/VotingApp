import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/models/user';
import { UserActivationView } from 'src/app/models/user-activation-view';


@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

    sendActivationCode(email:string,idUser:number){

        return this.http.post(`http://localhost:4001/user/sendActivationCode/${idUser}`,email);
    }

    activateUser(userActivationView:UserActivationView){

        return this.http.put(`http://localhost:4001/user/activateUser`,userActivationView);
    }

    authenticate(user:User){
        
        return this.http.post(`http://localhost:4001/user/authenticate`,user);     
    }

    changePassword(user:User){

        return this.http.put(`http://localhost:4001/user/changePassword`,user);
    }
}