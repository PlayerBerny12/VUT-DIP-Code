import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-feedback-dialog',
  templateUrl: './feedback-dialog.component.html',
  styleUrls: ['./feedback-dialog.component.scss']
})
export class FeedbackDialogComponent {
  feedbackFormControl = new FormControl<string>('', [Validators.required]);

  constructor(private dialogRef: MatDialogRef<FeedbackDialogComponent>) { }

  closeClick(): void {
    this.dialogRef.close();
  }
}
