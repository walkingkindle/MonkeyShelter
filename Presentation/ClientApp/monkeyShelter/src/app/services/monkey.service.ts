import { Injectable } from '@angular/core';
import { catchError, Observable, of, throwError } from 'rxjs';
import { MonkeyEntryRequest } from '../models/MonkeyEntryRequest';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { MonkeyDepartureRequest } from '../models/MonkeyDepartureRequest';
import { MonkeyDateRequest } from '../models/MonkeyDateRequest';
import {MonkeyWeightRequest} from '../models/MonkeyWeightRequest'
import { MonkeyVetCheckResponse } from '../models/MonkeyVetCheckResponse';
import { MonkeySpecies } from '../enums/species';
import { MonkeyReportResponse } from '../models/MonkeyReportResponse';

@Injectable({
  providedIn: 'root'
})
export class MonkeyService {
  getMonkeysCheckup(): Observable<MonkeyReportResponse> {
    return this.http.get<MonkeyReportResponse>(this.monkeysApiUrl);
  }

updateMonkeyWeight(request: MonkeyWeightRequest): Observable<any> {
  const token = localStorage.getItem('token');
  const headers = new HttpHeaders({
    Authorization: `Bearer ${token}`
  });

  const url = `${this.monkeysApiUrl}/${request.monkeyId}`;

  return this.http.patch(url, request, {headers: headers , responseType:'text'});
}

  getMonkeysBySpecies(species: string): Observable<MonkeyReportResponse[]> {
    const url = `${this.reportsApiUrl}/monkeys-per-species?species=${species}`;
    return this.http.get<MonkeyReportResponse[]>(url);
  }

  getMonkeysByDate(request: MonkeyDateRequest): Observable<any> {
    const params = new HttpParams()
      .set('dateFrom', request.dateFrom.toISOString())
      .set('dateTo', request.dateTo.toISOString());

    return this.http.get(this.reportsApiUrl + '/arrivals-per-date', {params});
  }

  private monkeysApiUrl = 'https://localhost:7008/api/monkeys'; // Your API endpoint

  private reportsApiUrl = 'https://localhost:7008/api/reports';

  constructor(private http: HttpClient) {}

departMonkeyFromShelter(monkeyId: number): Observable<any> {
  const token = localStorage.getItem('token');
  const headers = new HttpHeaders({
    Authorization: `Bearer ${token}`
  });

  return this.http.delete(`${this.monkeysApiUrl}/${monkeyId}`, {
    headers: headers,
    responseType: 'text' as const
  });
}
admitMonkeyToShelter(request: MonkeyEntryRequest): Observable<MonkeyReportResponse | string> {
  const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
    Authorization: `Bearer ${token}`
  });
    return this.http.post<MonkeyReportResponse | string>(this.monkeysApiUrl, request, { headers }).pipe(
    catchError((error) => {
      return of(this.handleError(error)); 
    })
  );
}
private handleError(error: HttpErrorResponse): string {
  let errorMessage = 'An unexpected error occurred.';

  if (error.status === 401) {
    errorMessage = 'You are not authorized to perform this action.';
  } else if (error.status === 403) {
    errorMessage = 'You do not have permission to admit a monkey.';
  } else if (error.status === 400) {
    errorMessage = error.error || 'Invalid request data.';
  }

  return errorMessage; 
}


}
