import { Component } from '@angular/core';
import { MonkeyService } from '../services/monkey.service';
import { CommonModule } from '@angular/common';
import { MonkeyTableComponent } from '../monkey-report-table/monkey-report-table.component';
import { MonkeyCheckupResponse } from '../models/MonkeyCheckupResponse';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-get-monkey-checkup-info',
  standalone: true,
  imports: [CommonModule, MonkeyTableComponent,FormsModule],
  templateUrl: './get-monkey-checkup-info.component.html',
})


export class GetMonkeyCheckupInfoComponent {
  lessThan30: MonkeyCheckupResponse[] = [];
  moreThan30: MonkeyCheckupResponse[] = [];


  constructor(private monkeyService:MonkeyService){}

  onSubmit(): void {
    this.monkeyService.getMonkeysCheckup().subscribe({
      next: (response) => {
        this.lessThan30 = response.scheduledInTheNext30;
        this.moreThan30 = response.upcomingVetChecks;
      },
      error: (err) => {
        console.error('Error:', err);
      }
    });
  }
}

