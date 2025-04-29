import { Routes } from '@angular/router';
import { MonkeyFormComponent } from './add-monkey/add-monkey.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { AppComponent } from './app.component';
import { RemoveMonkeyComponent } from './remove-monkey/remove-monkey.component';

export const routes: Routes = [
  { path: 'add-monkey', component: MonkeyFormComponent },
  { path: 'remove-monkey', component: RemoveMonkeyComponent },
  { path: '', redirectTo: 'add-monkey', pathMatch: 'full' }
];

