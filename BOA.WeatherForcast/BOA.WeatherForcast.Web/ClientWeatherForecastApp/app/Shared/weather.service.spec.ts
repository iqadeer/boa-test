import { WeatherService } from './weather.service';
import { TestBed, inject, async, getTestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { ErrorObservable } from "rxjs/observable/ErrorObservable";
import { ICityViewModel } from '../CityList/CityViewModel';
import { City, List, CityWeatherDetail } from '../Weather/weather-detail.component';

describe('WeatherService', () => {

    let listArray: Array<List> = [
    ];

    let mockedCity : City = null;

    const mockedCityWeatherDetails: Array<CityWeatherDetail> = [
        {
            "cod": "1234",
            "message": 2,
            "cnt": 2,
            "list": listArray,
            "city": null
        }
    ];

    const mockedCities: Array<ICityViewModel> = [
        {
            "id": "1",
            "nameOfCity": "London"
        },
        {
            "id": "2",
            "nameOfCity": "Reading"
        },
        {
            "id": "3",
            "nameOfCity": "Manchester"
        },
        {
            "id": "4",
            "nameOfCity": "Bristol"
        }
    ];

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
                HttpClientTestingModule,
            ],

            providers: [WeatherService]
        });

    });

    it('should be created', async(inject([WeatherService, HttpTestingController], (service: WeatherService, httpClient: HttpTestingController) => {
        expect(service).toBeTruthy();
    })));

    it('getCities should return an  Observable<Array<City>>',
        async(inject([WeatherService, HttpTestingController], (service: WeatherService, httpMock: HttpTestingController) => {

            service.getCities().subscribe(list => {
                expect(list.length).toBe(4);
                expect(list).toEqual(mockedCities);
            });

            const req = httpMock.expectOne(`${service._serviceUrlBase}`);
            expect(req.request.method).toBe("GET");

            expect(req.cancelled).toBeFalsy();
            req.flush(mockedCities);
            httpMock.verify();
        })));

    it('getCities should return a json response',
        async(inject([WeatherService, HttpTestingController], (service: WeatherService, httpMock: HttpTestingController) => {

            service.getCities().subscribe(list => {
                expect(list.length).toBe(4);
                expect(list).toEqual(mockedCities);
            });

            const req = httpMock.expectOne(`${service._serviceUrlBase}`);
            expect(req.request.responseType).toEqual('json');
            req.flush(mockedCities);
            httpMock.verify();
        })));

    it("getCities should return error if city request failed",
        async(inject([WeatherService, HttpTestingController], (service: WeatherService, httpMock: HttpTestingController) => {
            service.getCities().subscribe(() => { }, err => {
                expect(err).toBe(`An error has occurred`);
            });
            let errorReq = httpMock.expectNone(`${service._serviceUrlBase}/abcd`);

            let serverErrorReq = httpMock.expectOne(`${service._serviceUrlBase}`);
            serverErrorReq.error(new ErrorEvent('An error has occurred'));
        })));

    it("getWeatherOfCityById should return error if city request failed",
        async(inject([WeatherService, HttpTestingController], (service: WeatherService, httpMock: HttpTestingController) => {
            service.getWeatherOfCityById("2").subscribe(() => { }, err => {
                expect(err).toBe(`An error has occurred`);
            });
            let errorReq = httpMock.expectNone(`${service._serviceUrlBase}/abcd/3`);

            let serverErrorReq = httpMock.expectOne(`${service._serviceUrlBase}/2`);
            serverErrorReq.error(new ErrorEvent('An error has occurred'));
        })));

    it('getWeatherOfCityById should return an  Observable<Array<CityWeatherDetail>>',
        async(inject([WeatherService, HttpTestingController], (service: WeatherService, httpMock: HttpTestingController) => {

            service.getWeatherOfCityById("2").subscribe(list => {
                expect(list).toEqual(mockedCityWeatherDetails);
            });

            const req = httpMock.expectOne(`${service._serviceUrlBase}/2`);

            req.flush(mockedCityWeatherDetails);
            httpMock.verify();
        })));

    it('getWeatherOfCityById on cancel should return false',
        async(inject([WeatherService, HttpTestingController], (service: WeatherService, httpMock: HttpTestingController) => {

            service.getWeatherOfCityById("2").subscribe(list => {
            });

            const req = httpMock.expectOne(`${service._serviceUrlBase}/2`);

            expect(req.cancelled).toBeFalsy();
            httpMock.verify();
        })));

    it('getWeatherOfCityById should returns single item',
        async(inject([WeatherService, HttpTestingController], (service: WeatherService, httpMock: HttpTestingController) => {

            service.getWeatherOfCityById("2").subscribe(list => {
                expect(list.length).toBe(1);
            });

            const req = httpMock.expectOne(`${service._serviceUrlBase}/2`);

            req.flush(mockedCityWeatherDetails);
            httpMock.verify();
        })));


    it('getWeatherOfCityById should should use get metthod',
        async(inject([WeatherService, HttpTestingController], (service: WeatherService, httpMock: HttpTestingController) => {

            service.getWeatherOfCityById("2").subscribe(list => {
            });

            const req = httpMock.expectOne(`${service._serviceUrlBase}/2`);

            expect(req.request.method).toBe("GET");

            req.flush(mockedCityWeatherDetails);
            httpMock.verify();
        })));

    it('getWeatherOfCityById should return a json response',
        async(inject([WeatherService, HttpTestingController], (service: WeatherService, httpMock: HttpTestingController) => {

            service.getWeatherOfCityById("2").subscribe(list => {
            });

            const req = httpMock.expectOne(`${service._serviceUrlBase}/2`);
            expect(req.request.responseType).toEqual('json');
            req.flush(mockedCityWeatherDetails);
            httpMock.verify();
        })));

    //getWeatherOfCityById
});
