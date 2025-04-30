import { Routes } from '@angular/router';
import { MonkeyFormComponent } from './add-monkey/add-monkey.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { AppComponent } from './app.component';
import { RemoveMonkeyComponent } from './remove-monkey/remove-monkey.component';
import { GetMonkeysByDateComponent } from './get-monkeys-by-date/get-monkeys-by-date.component';
import { Component } from '@angular/core';
import { GetMonkeyBySpeciesComponent } from './get-monkey-by-species/get-monkey-by-species.component';
import { UpdateMonkeyWeightComponent } from './update-monkey-weight/update-monkey-weight.component';
import { GetMonkeyCheckupInfoComponent } from './get-monkey-checkup-info/get-monkey-checkup-info.component';

export const routes: Routes = [
  { path: 'add-monkey', component: MonkeyFormComponent },
  { path: 'remove-monkey', component: RemoveMonkeyComponent },
  {path: 'monkeys-date', component: GetMonkeysByDateComponent},
  {path: 'monkeys-species', component: GetMonkeyBySpeciesComponent},
  {path:'monkey-checkup-info', component:GetMonkeyCheckupInfoComponent},
  {path:'monkey-weight', component:UpdateMonkeyWeightComponent},
  { path: '', redirectTo: 'add-monkey', pathMatch: 'full' }
];

