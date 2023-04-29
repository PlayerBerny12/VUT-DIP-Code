import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { ResponsesVM } from 'src/app/models/response.model';
import { RequestService } from 'src/app/services/request.service';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.scss']
})
export class ResultsComponent implements OnInit {
  responsesSource = new Subject<ResponsesVM>();
  responses$ = this.responsesSource.asObservable();

  constructor(private requestService: RequestService) { }

  ngOnInit(): void {
    // this.requestService.getResults()
    //   .subscribe(data => this.responsesSource.next(data));
    setTimeout(() => {
      this.responsesSource.next({
        value: 0.5,
        responses: [
          { detectionMethod: { description: "very long long long desc\n asdasd ada kjlajfklasfsamf af saf.", name: "name 1", trainingDataset: "dataset 1" }, value: 0.6 },
          { detectionMethod: { description: "desc", name: "name 2", trainingDataset: "dataset 1" }, value: null },
          { detectionMethod: { description: "very long long long desc\n asdasd ada kjlajfklasfsamf af saf.", name: "name 1", trainingDataset: "dataset 1" }, value: 0.6 },
          { detectionMethod: { description: "desc", name: "name 2", trainingDataset: "dataset 1" }, value: null },
          { detectionMethod: { description: "very long long long desc\n asdasd ada kjlajfklasfsamf af saf.", name: "name 1", trainingDataset: "dataset 1" }, value: 0.6 },
          { detectionMethod: { description: "desc", name: "name 2", trainingDataset: "dataset 1" }, value: null }
        ]
      });
      // this.responsesSource.next({
      //   value: null,
      //   responses: []
      // });
    }, 500);
  }

  openOverallScoreDialog() {

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
}
