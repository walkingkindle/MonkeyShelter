import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { config } from './app/app.config.server';
import { provideHttpClient } from '@angular/common/http';
import { appConfig } from './app/app.config';



const bootstrap = () => bootstrapApplication(AppComponent, appConfig);

export default bootstrap;
