import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NoopScrollStrategy } from '@angular/cdk/overlay';
import { BehaviorSubject } from 'rxjs';
import { DetectionMethodVM } from 'src/app/models/detection_method.model';
import { ResponseVM } from 'src/app/models/response.model';
import { DetectionMethodDialogComponent } from '../detection-method-dialog/detection-method-dialog.component';
import { RequestService } from 'src/app/services/request.service';

@Component({
  selector: 'app-result-method',
  templateUrl: './result-method.component.html',
  styleUrls: ['./result-method.component.scss']
})
export class ResultMethodComponent {
  private responseSource = new BehaviorSubject<ResponseVM | null>(null);
  response$ = this.responseSource.asObservable();

  @Input() set response(value: ResponseVM) {
    this.responseSource.next(value);
  }

  constructor(
    private dialog: MatDialog,
    public requestService: RequestService
  ) { }

  openDetectionMethodDialog(detectionMethod: DetectionMethodVM): void {
    this.dialog.open(DetectionMethodDialogComponent, {
      data: detectionMethod,
      scrollStrategy: new NoopScrollStrategy()
    });
  }

  getWidth(value: number | null) {
    if (value) {
      let roundedValue = this.requestService.roundResults(value);
      return `width: ${roundedValue * 100}%`
    } else {
      return "";
    }
  }
}
