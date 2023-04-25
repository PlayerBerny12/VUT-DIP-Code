import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DetectionMethodVM } from 'src/app/models/detection_method.model';

@Component({
  selector: 'app-detection-method-dialog',
  templateUrl: './detection-method-dialog.component.html',
  styleUrls: ['./detection-method-dialog.component.scss']
})
export class DetectionMethodDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public detectionMethod: DetectionMethodVM) { }
}
