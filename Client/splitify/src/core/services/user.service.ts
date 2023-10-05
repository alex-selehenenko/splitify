import { EventEmitter, Injectable } from "@angular/core";
import { UserPost } from "../models/user.post.model";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";

@Injectable({ providedIn: 'root' })
export class UserService{
    userAuthorized: EventEmitter<void> = new EventEmitter;
    userUnauthorized: EventEmitter<void> = new EventEmitter;
    userNotVerified: EventEmitter<void> = new EventEmitter;

    constructor(private httpClient: HttpClient){}
    
    createUser(userEmail: string, userPassword: string): Observable<UserPost>{
        const body = {
            email: userEmail,
            password: userPassword
        };
        return this.httpClient.post<UserPost>(
            environment.userServiceApiUrl + 'api/v1/auth/register',
            body);
    }
}