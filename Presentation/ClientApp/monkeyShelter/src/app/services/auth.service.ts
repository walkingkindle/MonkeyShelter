import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private apiUrl = 'https://localhost:7008/api/auth/login';

    constructor(private http: HttpClient) {}

    login(username: string): Observable<any> {
        return this.http.post<any>(this.apiUrl, { username });
    }
}
