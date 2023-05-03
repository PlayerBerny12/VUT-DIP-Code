import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  constructor(
    private http: HttpClient,
    private snackBar: MatSnackBar
  ) { }

  sendFeedback(feedback: string) {
    let formData: FormData = new FormData();
    formData.append('feedback', feedback);

    return this.http.post('http://localhost/api/feedback', formData)
      .pipe(
        tap({
          next: () => this.snackBar.open("Feedback successfully sent.", undefined, { duration: 3500 }),
          error: () => this.snackBar.open("Error occurred while sending feedback.", undefined, { duration: 3500 })
        })
      );
  }
}
