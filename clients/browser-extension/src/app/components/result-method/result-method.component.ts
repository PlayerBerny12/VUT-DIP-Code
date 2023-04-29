import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BehaviorSubject, Subject } from 'rxjs';
import { DetectionMethodVM } from 'src/app/models/detection_method.model';
import { ResponseVM } from 'src/app/models/response.model';
import { DetectionMethodDialogComponent } from '../detection-method-dialog/detection-method-dialog.component';

@Component({
  selector: 'app-result-method',
  templateUrl: './result-method.component.html',
  styleUrls: ['./result-method.component.scss']
})
export class ResultMethodComponent {
  responseSource = new BehaviorSubject<ResponseVM | null>(null);
  response$ = this.responseSource.asObservable();

  @Input() set response(value: ResponseVM) {
    this.responseSource.next(value);
  }

  constructor(public dialog: MatDialog) { }

  openDetectionMethodDialog(detectionMethod: DetectionMethodVM): void {
    this.dialog.open(DetectionMethodDialogComponent, {
      data: detectionMethod
    });
  }

  getWidth(value: number | null) {
    if (value) {
      return `width: ${value * 100}%`
    } else {
      return "";
    }
  }
}
