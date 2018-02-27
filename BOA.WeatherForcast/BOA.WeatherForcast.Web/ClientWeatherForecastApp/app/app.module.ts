import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import {HttpClientModule } from "@angular/common/http";
import { AppComponent } from './app.component';
import { CityListComponent } from './CityList/cityList.component';
import { WeatherService } from './Shared/weather.service';
import { WeatherDetailComponent } from './Weather/weather-detail.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
      AppComponent,
      CityListComponent,
      WeatherDetailComponent
    ],

  imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule
  ],
  providers: [
      WeatherService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
