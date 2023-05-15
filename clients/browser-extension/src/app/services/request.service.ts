import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BehaviorSubject, catchError, filter, of, repeat, take, tap, throwError } from 'rxjs';
import { ResponsesVM } from '../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  requestID: number | null = null;

  constructor(
    private http: HttpClient,
    private snackBar: MatSnackBar
  ) { }

  detectFile(uploadFile: any) {
    let formData: FormData = new FormData();
    formData.append('file', uploadFile);

    return this.http.post<number>('http://20.31.219.16/api/detect/file', formData)
      .pipe(
        tap(result => this.requestID = result),
        catchError(error => this.handleError(error))
      );
  }

  detectLink(link: string) {
    let params = new HttpParams()
      .append("link", encodeURIComponent(link));

    return this.http.post<number>('http://20.31.219.16/api/detect/link', null, { params: params })
      .pipe(
        tap(result => this.requestID = result),
        catchError(error => this.handleError(error))
      );
  }

  getResults() {
    let params = new HttpParams()
      .append("requestID", this.requestID!.toString());

    let count = 600;

    return this.http.get<ResponsesVM>('http://20.31.219.16/api/request/results', { params: params })
      .pipe(
        repeat({ delay: 2000 }),
        tap(() => count -= 1),
        filter(data => data != null || (data == null && count == 0)),
        take(1),
      );
  }

  roundResults(value: number) {
    return Math.round(value * 100) / 100;
  }

  private handleError(error: HttpErrorResponse) {
    this.snackBar.open("Start detection failed.", undefined, { duration: 3500 });
    return throwError(() => error)
  }
}
