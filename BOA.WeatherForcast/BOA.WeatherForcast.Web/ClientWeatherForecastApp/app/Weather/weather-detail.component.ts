import { Component, OnInit, Input } from "@angular/core";
import { WeatherService } from "../Shared/weather.service";


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





    export interface Main {
        temp: number;
        temp_min: number;
        temp_max: number;
        pressure: number;
        sea_level: number;
        grnd_level: number;
        humidity: number;
        temp_kf: number;
    }

    export interface Weather {
        id: number;
        main: string;
        description: string;
        icon: string;
    }

    export interface Clouds {
        all: number;
    }

    export interface Wind {
        speed: number;
        deg: number;
    }

    export interface Rain {
        "3h": number;
    }

    export interface Sys {
        pod: string;
    }

    export interface Snow {
        "3h": number;
    }

    export interface List {
        dt: number;
        main: Main;
        weather: Weather[];
        clouds: Clouds;
        wind: Wind;
        rain: Rain;
        sys: Sys;
        dt_txt: string;
        snow: Snow;
    }

    export interface Coord {
        lat: number;
        lon: number;
    }

    export interface City {
        id: number;
        name: string;
        coord: Coord;
        country: string;
    }

    export interface CityWeatherDetail {
        cod: string;
        message: number;
        cnt: number;
        list: List[];
        city: City;
    }

