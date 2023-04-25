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
    this.requestService.getResults()
      .subscribe(data => this.responsesSource.next(data));
  }
}
