import { Component } from '@angular/core';
import { MonkeyService } from '../services/monkey.service';
import { CommonModule } from '@angular/common';
import { MonkeyTableComponent } from '../monkey-report-table/monkey-report-table.component';
import { MonkeyCheckupResponse } from '../models/MonkeyCheckupResponse';
import { FormsModule } from '@angular/forms';
import { MonkeyReportResponse } from '../models/MonkeyReportResponse';

@Component({
  selector: 'app-get-monkey-checkup-info',
  standalone: true,
  imports: [CommonModule, MonkeyTableComponent,FormsModule],
  templateUrl: './get-monkey-checkup-info.component.html',
})


export class GetMonkeyCheckupInfoComponent {
  monkeys: MonkeyReportResponse[] = [];


  constructor(private monkeyService:MonkeyService){}

  onSubmit(): void {
    this.monkeyService.getMonkeysCheckup().subscribe({
      next: (response) => {
        this.monkeys = [response];
      },
      error: (err) => {
        console.error('Error:', err);
      }
    });
  }
}

