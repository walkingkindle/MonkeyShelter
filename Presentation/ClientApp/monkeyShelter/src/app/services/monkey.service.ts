import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MonkeyEntryRequest } from '../models/MonkeyEntryRequest';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MonkeyService {
  private apiUrl = 'http://localhost:7008/monkeys'; // Your API endpoint

  constructor(private http: HttpClient) {}

  admitMonkeyToShelter(request: MonkeyEntryRequest): Observable<any> {
    return this.http.post(this.apiUrl, request);
  }
}
