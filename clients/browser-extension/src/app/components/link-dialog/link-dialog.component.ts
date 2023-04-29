import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-link-dialog',
  templateUrl: './link-dialog.component.html',
  styleUrls: ['./link-dialog.component.scss']
})
export class LinkDialogComponent {
  linkFormControl = new FormControl<string>('', [Validators.required]);

  constructor(private dialogRef: MatDialogRef<LinkDialogComponent>) { }

  closeClick(): void {
    this.dialogRef.close();
  }
}
