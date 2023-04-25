import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BehaviorSubject, catchError, filter, of, repeat, take, tap, throwError } from 'rxjs';
import { ResponsesVM } from '../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  requestID: number = 0;

  constructor(
    private http: HttpClient,
    private snackBar: MatSnackBar
  ) { }

  detectFile(uploadFile: any) {
    console.log(uploadFile);
    let formData: FormData = new FormData();
    console.log(formData);
    formData.append('file', uploadFile);
    console.log(formData);
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "multipart/form-data"
      })
    };
    return this.http.post<number>('https://localhost:7278/api/detect/file', formData)
      .pipe(
        tap(result => this.requestID = result),
        catchError(error => this.handleError(error))
      );
  }

  detectLink(link: string) {
    let params = new HttpParams()
      .append("link", encodeURIComponent(link));

    return this.http.post<number>('https://localhost:7278/api/detect/link', null, { params: params })
      .pipe(
        tap(result => this.requestID = result),
        catchError(error => this.handleError(error))
      );
  }

  getResults() {
    let params = new HttpParams()
      .append("requestID", this.requestID.toString());

    return this.http.get<ResponsesVM>('http://localhost/api/request/results', { params: params })
      .pipe(
        repeat({ delay: 2500 }),
        filter(data => data != null),
        take(1)
      );
  }

  private handleError(error: HttpErrorResponse) {
    this.snackBar.open("Start detection failed.", undefined, { duration: 3500 });
    return throwError(() => error)
  }
}
