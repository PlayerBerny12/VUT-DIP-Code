import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NoopScrollStrategy } from '@angular/cdk/overlay';
import { FeedbackService } from './services/feedback.service';
import { FeedbackDialogComponent } from './components/feedback-dialog/feedback-dialog.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'browser-extension';

  constructor(
    private dialog: MatDialog,
    private feedbackService: FeedbackService,
  ) { }

  openFeedbackDialog(): void {
    const dialogRef = this.dialog.open(FeedbackDialogComponent, {
      scrollStrategy: new NoopScrollStrategy()
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.feedbackService.sendFeedback(result)
          .subscribe();
      }
    });
  }
}
