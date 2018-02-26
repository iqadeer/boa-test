import { TestBed, async } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { CityListComponent } from './CityList/cityList.component';
import { WeatherDetailComponent } from './Weather/weather-detail.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { WeatherService } from './Shared/weather.service';
import { HttpClientModule } from '@angular/common/http';

describe('AppComponent', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [FormsModule,
                HttpClientModule
            ],
            providers: [
                WeatherService
            ],

            declarations: [
                AppComponent,
            CityListComponent,
            WeatherDetailComponent
            ],
        }).compileComponents();
    }));

    it('should create the app', async(() => {
        const fixture = TestBed.createComponent(AppComponent);
        const app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it(`should have as title 'five day weather forecast'`, async(() => {
        const fixture = TestBed.createComponent(AppComponent);
        const app = fixture.debugElement.componentInstance;
        expect(app.title).toEqual('five day weather forecast');
    }));

    it('should render title in a h1 tag', async(() => {
        const fixture = TestBed.createComponent(AppComponent);
        fixture.detectChanges();
        const compiled = fixture.debugElement.nativeElement;
        expect(compiled.querySelector('h1').textContent).toContain('Welcome to five day weather forecast');
    }));
});
