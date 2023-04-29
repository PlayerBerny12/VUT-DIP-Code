import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { LinkDialogComponent } from 'src/app/components/link-dialog/link-dialog.component';
import { RequestService } from 'src/app/services/request.service';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  private loadingSource = new BehaviorSubject(false);
  loading$ = this.loadingSource.asObservable();

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private requestService: RequestService
  ) { }

  fileInputChange(fileInputEvent: any) {
    this.loadingSource.next(true);
    this.requestService.detectFile(fileInputEvent.target.files[0])
      .subscribe({
        next: () => this.finishRequest(),
        error: () => this.loadingSource.next(false)
      });
  }

  openLinkDialog(): void {
    const dialogRef = this.dialog.open(LinkDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadingSource.next(true);
        this.requestService.detectLink(result)
          .subscribe({
            next: () => this.finishRequest(),
            error: () => this.loadingSource.next(false)
          });
      }
    });
  }

  private finishRequest() {
    this.router.navigate(["results"]);
    this.loadingSource.next(false);
  }
}
