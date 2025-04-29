import { Routes } from '@angular/router';
import { MonkeyFormComponent } from './monkey-form/monkey-form.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { AppComponent } from './app.component';

export const routes: Routes = 
[
    {path:'add-monkey', component: MonkeyFormComponent},
    {path:'',component:AppComponent}

];
