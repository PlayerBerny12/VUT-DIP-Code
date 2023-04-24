import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LinkDialogComponent } from 'src/app/components/link-dialog/link-dialog.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  constructor(
    private dialog: MatDialog,
    private http: HttpClient
  ) { }

  file = null;

  fileInputChange(fileInputEvent: any) {
    this.detectFile(fileInputEvent.target.files[0]);
  }

  openLinkDialog(): void {
    const dialogRef = this.dialog.open(LinkDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      this.detectFile(result);
    });
  }

  private detectFile(uploadFile: any) {
    let formData: FormData = new FormData();
    formData.append('file', uploadFile);

    this.http.post<any>('http://localhost:XXXX/detect', formData).subscribe(
      data => {
        console.log(data);
      }
    );
  }

  private detectLink(link: string) {
    let formData: FormData = new FormData();
    formData.append('link', link);

    this.http.post<any>('http://localhost:XXXX/detect', formData).subscribe(
      data => {
        console.log(data);
      }
    );
  }
}
