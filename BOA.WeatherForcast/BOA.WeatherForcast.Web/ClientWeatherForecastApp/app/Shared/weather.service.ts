import { Injectable } from "@angular/core";
import { ICityViewModel } from "../CityList/CityViewModel";
import { HttpClient, HttpErrorResponse} from "@angular/common/http";
import {catchError, retry, map, tap, filter} from "rxjs/operators";
import { Observable} from "rxjs/Observable";
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

    public cities: Array<ICityViewModel> = [];

    getWeatherOfCityById(id: string): Observable<Array<CityWeatherDetail>> {
        return this.http.get<Array<CityWeatherDetail>>(this._serviceUrlBase + "/" + id)
            .pipe(
                tap(data => console.log("All: " + JSON.stringify(data))),
                retry(2),
                catchError(this.handleError));
    }

    getCities(): Observable<Array<ICityViewModel>> {
        return this.http.get<Array<ICityViewModel>>(this._serviceUrlBase)
            .pipe(
                tap(data => console.log("All: " + JSON.stringify(data))),
                retry(2),
                catchError(this.handleError));
    }



}