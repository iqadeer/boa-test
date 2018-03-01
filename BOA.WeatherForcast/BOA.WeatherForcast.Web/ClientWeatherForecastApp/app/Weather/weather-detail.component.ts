import { Component, OnInit, Input } from "@angular/core";
import { WeatherService } from "../Shared/weather.service";
import { CityWeatherDetail } from "./CityWeatherDetail";


@Component({
    selector: "weather-detail",
    templateUrl: "weather-detail.component.html"
})
export class WeatherDetailComponent implements OnInit{
    error: any;

    public cityWeatherData: Array<CityWeatherDetail>;
    public showWeather: boolean = false;

    @Input() selectedId: string;


    constructor(private _weatherService: WeatherService) {

    }

    ngOnInit(): void {
        this.getWeatherForCity("2638976");
    }

    newCitySelected(): void {
        this.getWeatherForCity(this.selectedId);
    }

    getWeatherForCity(id:string) :void {
        this._weatherService.getWeatherOfCityById(id).subscribe(
            data => {
                this.cityWeatherData = data;
                console.log(data);
            },
            error => this.error = error);
       }

    millisecondsToDate(seconds: number) : string {
        let d = new Date(seconds * 1000);
        this.showWeather = true;
        return d.toLocaleString();
    }
}


