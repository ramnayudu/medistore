import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';


import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { MedicineData } from './home.component';
import { HttpErrorHandler, HandleError } from '../http-error-handler.service';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    Authorization: 'my-auth-token'
  })
};

@Injectable()
export class MediStoreService {
  mediStoreUrl = 'api/MediStore';  // URL to web api
  private handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('MediStoreService');
  }

  /** GET medicines  from the server */
  getMedicines(): Observable<MedicineData[]> {
    return this.http.get<MedicineData[]>(this.mediStoreUrl)
      .pipe(
        catchError(this.handleError('getMedicines', []))
      );
  }

  //////// Save methods //////////

  /** POST: add a new Medicine to the database */
  addMedicine(medicine: MedicineData): Observable<MedicineData> {
    return this.http.post<MedicineData>(this.mediStoreUrl, medicine, httpOptions)
      .pipe(
        catchError(this.handleError('addMedicine', medicine))
      );
  }

  /** DELETE: delete the medicine from the server */
  deleteMedicine(id: number): Observable<{}> {
    const url = `${this.mediStoreUrl}/${id}`; // DELETE api/heroes/42
    return this.http.delete(url, httpOptions)
      .pipe(
        catchError(this.handleError('deleteMedicine'))
      );
  }

  /** PUT: update the Medicine on the server. Returns the updated Medicine upon success. */
  updateMedicine(medicine: MedicineData): Observable<MedicineData> {
    httpOptions.headers =
      httpOptions.headers.set('Authorization', 'my-new-auth-token');

    return this.http.put<MedicineData>(this.mediStoreUrl, medicine, httpOptions)
      .pipe(
        catchError(this.handleError('updateMedicine', medicine))
      );
  }
}
