import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './views/home/home.component';
import { LinkDialogComponent } from './components/link-dialog/link-dialog.component';
import { FileUploadDialogComponent } from './components/file-upload-dialog/file-upload-dialog.component';
import { FeedbackDialogComponent } from './components/feedback-dialog/feedback-dialog.component';
import { ResultsComponent } from './views/results/results.component';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LinkDialogComponent,
    FileUploadDialogComponent,
    FeedbackDialogComponent,
    ResultsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule, 
    ReactiveFormsModule, 
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatInputModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
