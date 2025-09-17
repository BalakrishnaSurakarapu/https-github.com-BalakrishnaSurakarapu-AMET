import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NewDataComponent } from './Components/new-data/new-data.component';
import { NewPopupComponent } from './Components/new-popup/new-popup.component';

@NgModule({
  declarations: [
    AppComponent,
    NewDataComponent,
    NewPopupComponent,    
    
     
      ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,  
     FormsModule,     
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
