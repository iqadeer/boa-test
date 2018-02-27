import { TestBed, inject, async, getTestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Observable } from "rxjs/Rx";
import { City, List, CityWeatherDetail, WeatherDetailComponent } from '../Weather/weather-detail.component';
import { WeatherService } from '../Shared/weather.service';
import { defer } from 'rxjs/observable/defer';


export function asyncData<T>(data: T) {
    return defer(() => Promise.resolve(data));
}

describe("Weather-detail.component", () => {
    let weatherService: jasmine.SpyObj<WeatherService>;
    let weatherDetail: WeatherDetailComponent;
    beforeEach(() => {
        const serviceSpy = jasmine.createSpyObj('WeatherService', ['getWeatherOfCityById']);
        TestBed.configureTestingModule({
            providers: [
                {
                    provide: WeatherService,
                    useValue: serviceSpy
                }
            ]
        });
        weatherService = TestBed.get(WeatherService);
        weatherDetail = new WeatherDetailComponent(weatherService);
    });

    it("getWeatherOfCityById should return a fake value from fake service",
        fakeAsync(() => {

            const fakeValue: Array<CityWeatherDetail> = [
                {
                    "cod": "1234",
                    "message": 2,
                    "cnt": 2,
                    "list": null,
                    "city": null
                }
            ];

            weatherService.getWeatherOfCityById.and.returnValue(asyncData(fakeValue));
            weatherDetail.getWeatherForCity("2");

            tick();

            expect(weatherService.getWeatherOfCityById.calls.count()).toBe(1, "spy method is called only once");
            expect(weatherDetail.cityWeatherData).toBe(fakeValue);
        }));
})