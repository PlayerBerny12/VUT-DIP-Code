import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { tap } from 'rxjs';
import { RequestService } from './request.service';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  constructor(
    private http: HttpClient,
    private snackBar: MatSnackBar,
    private requestService: RequestService
  ) { }

  sendFeedback(feedback: string) {
    return this.http.post('http://20.4.98.50/api/feedback', { "request": this.requestService.requestID, "text": feedback })
      .pipe(
        tap({
          next: () => this.snackBar.open("Feedback successfully sent.", undefined, { duration: 3500 }),
          error: () => this.snackBar.open("Error occurred while sending feedback.", undefined, { duration: 3500 })
        })
      );
  }
}
