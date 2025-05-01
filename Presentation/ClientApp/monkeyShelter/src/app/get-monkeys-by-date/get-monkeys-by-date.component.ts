import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MonkeyService } from '../services/monkey.service';
import { MonkeyReportResponse } from '../models/MonkeyReportResponse';
import { MonkeyTableComponent } from '../monkey-report-table/monkey-report-table.component';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-get-monkeys-by-date',
  imports: [ReactiveFormsModule,MonkeyTableComponent,CommonModule],
  templateUrl: './get-monkeys-by-date.component.html',
  styleUrl: './get-monkeys-by-date.component.css'
})
export class GetMonkeysByDateComponent {
getMonkeysByDateForm: FormGroup;
  columns = [
    { field: 'id', header: 'ID' },
    { field: 'name', header: 'Name' },
    { field: 'species', header: 'Species' },
    {field: 'weight', header : 'Weight'},
  ];
monkeyData: MonkeyReportResponse[] = [];
  constructor(private fb: FormBuilder,private monkeyService: MonkeyService) {
          this.getMonkeysByDateForm = this.fb.group({
            dateFrom: ['', Validators.required],
            dateTo: ['', Validators.required],
    })

}
onSubmit(): void {
  const formValues = this.getMonkeysByDateForm.value;

  const request = {
    dateFrom: new Date(formValues.dateFrom),
    dateTo: new Date(formValues.dateTo),
  };

  this.monkeyService.getMonkeysByDate(request).subscribe({
    next: (response) => {
      this.monkeyData = response;
      
      if (this.monkeyData.length === 0) {
        Swal.fire({
          icon: 'info',
          title: 'No Monkeys Found',
          text: 'No monkeys were found with the specified date range.',
        });
      }
    },
    error: (err) => {
      console.error('Error:', err);
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: 'An error occurred while fetching the data. Please try again later.',
      });
    }
  });
}



}


