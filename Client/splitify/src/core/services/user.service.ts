import { EventEmitter, Injectable } from "@angular/core";
import { UserPost } from "../models/user.post.model";
import { Observable, Observer } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { UserGet } from "../models/user.get.model";

@Injectable({ providedIn: 'root' })
export class UserService{
    userAuthorized: EventEmitter<void> = new EventEmitter;
    userUnauthorized: EventEmitter<void> = new EventEmitter;
    userNotVerified: EventEmitter<void> = new EventEmitter;

    constructor(private httpClient: HttpClient){}
    
    fetchUser(): Observable<UserGet>{
        return this.httpClient.get<UserGet>(environment.userServiceApiUrl + 'api/v1/user');
    }

    createUser(userEmail: string, userPassword: string): Observable<UserPost>{
        const body = {
            email: userEmail,
            password: userPassword
        };
        return this.httpClient.post<UserPost>(
            environment.userServiceApiUrl + 'api/v1/auth/register',
            body);
    }

    resendVerificationCode(): Observable<any>{
        const body = {};
        return this.httpClient.patch<any>(
            environment.userServiceApiUrl + 'api/v1/user/verificationCode',
            body);
    }

    verifyUser(code: string){
        const body = { verificationCode: code };
        
        return this.httpClient.post<UserPost>(
            environment.userServiceApiUrl + 'api/v1/auth/verify',
            body);
    }

    loginUser(login: string, password: string): Observable<UserPost>{
        const body = { email: login, password: password };

        return this.httpClient.post<UserPost>(
            environment.userServiceApiUrl + 'api/v1/auth/login',
            body);
    }

    sendResetPasswordCode(email: string){
        const body = { email: email }

        return this.httpClient.post<UserPost>(
            environment.userServiceApiUrl + 'api/v1/password/token/reset',
            body);
    }

    setNewPassword(token: string, password: string){
        const body = {
            token: token,
            password: password
        }

        return this.httpClient.post(
            environment.userServiceApiUrl + 'api/v1/password/',
            body);
    }

    verifyResetPasswordToken(token: string){
        return this.httpClient.get<any>(
            environment.userServiceApiUrl
            + 'api/v1/password/token/'
            + token);
    }
}