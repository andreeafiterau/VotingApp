import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/shared/models/user';
import { UserActivationView } from 'src/app/shared/models/user-activation-view';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { Role } from '../../models/role';
import { UserAdminView } from '../../models/user-admin-view';
import { UserAuthenticationView } from '../../models/user-auth-view';


@Injectable({ providedIn: 'root' })
export class UserService {

    private currentUserSubject: BehaviorSubject<UserAuthenticationView>;
    public currentUser: Observable<UserAuthenticationView>;

    constructor(private http: HttpClient,private router: Router){ 

        this.currentUserSubject = new BehaviorSubject<UserAuthenticationView>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): UserAuthenticationView {
        return this.currentUserSubject.value;
    }

    sendActivationCode(email:string){

        return this.http.post(`http://localhost:4001/user/sendActivationCode`,{email});
    }

    activateUser(email:string,code:string,password:string){

        return this.http.put(`http://localhost:4001/user/activateUser`,{email,code,password});
    }

    changePassword(username:string,password:string){

        return this.http.put(`http://localhost:4001/user/changePassword`,{username,password});
    }

    forgotPassword(email:string,password:string,code:string){

        return this.http.put(`http://localhost:4001/user/forgotPassword`,{email,code,password});
    }

    sendPasswordToken(email:string){

        return this.http.post(`http://localhost:4001/user/sendPasswordToken`,{email});

    }

    authenticate(username: string, password: string) {
        return this.http.post<any>(`http://localhost:4001/user/authenticate`, { username, password })
            .pipe(map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    this.currentUserSubject.next(user);
                }
                return user;
            }));
    }

    getRole(idUser:number){

        return this.http.post<Role>(`http://localhost:4001/user/getRole`,{idUser});
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }

}