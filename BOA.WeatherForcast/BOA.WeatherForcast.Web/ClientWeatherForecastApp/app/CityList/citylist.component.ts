import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { WeatherService } from '../Shared/weather.service';
import { ICityViewModel } from './CityViewModel';

@Component({
    selector: 'city-list',
    templateUrl: "./cityList.component.html"
})
export class CityListComponent implements OnInit {
    error: any;

    constructor(private service: WeatherService) {
        //this.cities = service.cities;
    }

    @Output() selectionChanged = new EventEmitter<string>(); 

    ngOnInit(): Array<ICityViewModel> {
        this.service.getCities()
            .subscribe(data => {
                this.cities = data;
                    this.selectedCity = this.cities[0].id;
                    console.log(this.selectedCity);
                },
                error => this.error = error);
        return this.getCities();
    }

    public selectedCity: string;

    onSelect(cityId: string) {
       // console.log("city is changed to " + cityId);
        this.selectedCity = null;
        for (let i = 0; i < this.cities.length; i++) {
            if (this.cities[i].id === cityId) {
                this.selectedCity = this.cities[i].id;
                break;
            }
        }
        this.selectionChanged.emit(cityId);
    }

    onCityChanged(id: string) {
        console.log("city is changed to " + id);
        this.selectionChanged.emit(id);
    }
    title = "Five day weather forecast";

    public cities: Array<ICityViewModel>;

    public getCities(): Array<ICityViewModel> {
        return this.cities;
    }
}

