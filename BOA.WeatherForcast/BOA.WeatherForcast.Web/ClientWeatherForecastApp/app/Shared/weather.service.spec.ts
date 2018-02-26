import { WeatherService } from './weather.service';
import { TestBed, inject, async} from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpClientModule, HttpClient } from '@angular/common/http';


describe('MyNewServiceService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
                HttpClientTestingModule,
            ],

            providers: [WeatherService]
        });
    });


    //async(inject([HttpTestingController, ApiService],
    //    (httpClient: HttpTestingController, apiService: ApiService) => {
    //        expect(apiService).toBeTruthy();
    it('should be created', async(inject([WeatherService, HttpTestingController], (httpClient: HttpTestingController, service: WeatherService) => {
        expect(service).toBeTruthy();
    })));


});
