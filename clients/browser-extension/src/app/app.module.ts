import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './views/home/home.component';
import { LinkDialogComponent } from './components/link-dialog/link-dialog.component';
import { FileUploadDialogComponent } from './components/file-upload-dialog/file-upload-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LinkDialogComponent,
    FileUploadDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
