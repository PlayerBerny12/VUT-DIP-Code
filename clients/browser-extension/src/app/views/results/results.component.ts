import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { NoopScrollStrategy } from '@angular/cdk/overlay';
import { Subject } from 'rxjs';
import { OverallScoreDialogComponent } from 'src/app/components/overall-score-dialog/overall-score-dialog.component';
import { ResponsesVM } from 'src/app/models/response.model';
import { RequestService } from 'src/app/services/request.service';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.scss']
})
export class ResultsComponent implements OnInit {
  private responsesSource = new Subject<ResponsesVM>();
  responses$ = this.responsesSource.asObservable();

  constructor(
    public requestService: RequestService,
    private dialog: MatDialog,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.requestService.getResults()
      .subscribe(data => this.responsesSource.next(data));
  }

  openOverallScoreDialog() {
    this.dialog.open(OverallScoreDialogComponent, {
      scrollStrategy: new NoopScrollStrategy()
    });
  }

  getOverallScoreIcon(value: number) {
    if (value < 0.35) {
      return "mood_bad"
    } else if (value < 0.6) {
      return "sentiment_neutral"
    } else if (value < 0.75) {
      return "sentiment_satisfied"
    } else if (value < 0.9) {
      return "mood"
    } else {
      return "sentiment_very_satisfied"
    }
  }

  goBack() {
    this.router.navigate(["/"]);
  }
}
