import { Injectable } from "@angular/core";
import { City } from "../CityList/City";
import { HttpClient, HttpErrorResponse} from "@angular/common/http";
import "rxjs/add/operator/map";
import "rxjs/add/operator/do";
import {catchError, retry} from "rxjs/operators";
import { Observable } from "rxjs/Observable";
import { of } from "rxjs/Observable/of";
import { ErrorObservable } from "rxjs/observable/ErrorObservable";
import { CityWeatherDetail } from "../Weather/weather-detail.component";

@Injectable()
export class WeatherService {
    readonly _serviceUrlBase: string = "/api/forecast";
    
    private handleError(error: HttpErrorResponse): ErrorObservable {
        if (error.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            console.error('An error occurred:', error.error.message);
        } else {
            // The backend returned an unsuccessful response code.
            // The response body may contain clues as to what went wrong,
            console.error(
                `Backend returned code ${error.status}, ` +
                `body was: ${error.error}`);
        }
        // return an ErrorObservable with a user-facing error message
        return new ErrorObservable(
            'Something bad happened; please try again later.');
    };


    constructor(private http: HttpClient) {

    }

    public cities: Array<City> = [];

    getWeatherOfCityById(id: string): Observable<Array<CityWeatherDetail>> {
        return this.http.get<Array<CityWeatherDetail>>(this._serviceUrlBase + "/" + id)
            .do(data => console.log("All: " + JSON.stringify(data)))
            .pipe(
                retry(3),
                catchError(this.handleError));
    }

    getCities() : Observable<Array<City>> {
        return this.http.get<Array<City>>(this._serviceUrlBase)
        .do(data => console.log("All: " + JSON.stringify(data)))
            .pipe(
                retry(3),
                catchError(this.handleError));
    }



}