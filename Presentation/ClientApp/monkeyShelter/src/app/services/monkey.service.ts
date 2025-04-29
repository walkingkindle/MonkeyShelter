import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MonkeyEntryRequest } from '../models/MonkeyEntryRequest';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MonkeyDepartureRequest } from '../models/MonkeyDepartureRequest';
import { MonkeyDateRequest } from '../models/MonkeyDateRequest';

@Injectable({
  providedIn: 'root'
})
export class MonkeyService {
  getMonkeysBySpecies():Observable<any> {
    return this.http.get(this.reportsApiUrl + '/monkeys-per-species')
  }

  getMonkeysByDate(request:MonkeyDateRequest):Observable<any> {


    const params = new HttpParams()
    .set('dateFrom', request.dateFrom.toISOString())
    .set('dateTo', request.dateTo.toISOString());

    return this.http.get(this.reportsApiUrl + '/arrivals-per-species', {params})
  }

  private monkeysApiUrl = 'http://localhost:7008/monkeys'; // Your API endpoint

  private reportsApiUrl = 'http://localhost:7008/reports'

  constructor(private http: HttpClient) {}

  departMonkeyFromShelter(request: MonkeyDepartureRequest) : Observable<any>{
    const params = {
    monkeyId: request.monkeyId,
    };
    return this.http.delete(this.monkeysApiUrl, {params});
  }

  admitMonkeyToShelter(request: MonkeyEntryRequest): Observable<any> {
    return this.http.post(this.monkeysApiUrl, request);
  }
}
