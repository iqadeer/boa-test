import { Component, OnInit } from '@angular/core';
import { City } from "./City";
import { WeatherService } from '../Shared/weatherService';

@Component({
    selector: 'city-list',
    templateUrl: "./cityList.component.html"
})
export class CityListComponent implements OnInit {
    error: any;

    constructor(private service: WeatherService) {
        //this.cities = service.cities;
    }

    ngOnInit(): Array<City> {
        this.service.getCities()
            .subscribe(data => {
                    this.cities = data;
                    console.log(data);
                },
                error => this.error = error);
        return this.getCities();
    }
    title = "Five day weather forecast";

    public cities: Array<City>;

    public getCities(): Array<City> {
        return this.cities;
    }
}

