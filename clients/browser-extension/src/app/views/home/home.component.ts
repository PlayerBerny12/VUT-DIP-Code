import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { LinkDialogComponent } from 'src/app/components/link-dialog/link-dialog.component';
import { RequestService } from 'src/app/services/request.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  constructor(
    private router: Router,
    private dialog: MatDialog,
    private requestService: RequestService
  ) { }

  fileInputChange(fileInputEvent: any) {
    this.requestService.detectFile(fileInputEvent.target.files[0])
      .subscribe(() => this.redirectToResults());
  }

  openLinkDialog(): void {
    const dialogRef = this.dialog.open(LinkDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.requestService.detectLink(result)
          .subscribe(() => this.redirectToResults());
      }
    });
  }

  private redirectToResults() {
    this.router.navigate(["results"]);
  }
}
