import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MonkeyService } from '../services/monkey.service';
import { MonkeyReportResponse } from '../models/MonkeyReportResponse';
import { CommonModule } from '@angular/common';
import { MonkeyTableComponent } from '../monkey-report-table/monkey-report-table.component';
import { MonkeySpecies } from '../enums/species';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-get-monkey-by-species',
  standalone: true,
  imports: [ReactiveFormsModule, MonkeyTableComponent, CommonModule,FormsModule],
  templateUrl: './get-monkey-by-species.component.html',
  styleUrls: ['./get-monkey-by-species.component.css'],
})
export class GetMonkeyBySpeciesComponent {
  monkeyData: MonkeyReportResponse[] = [];
  getMonkeysBySpeciesForm: FormGroup;

  monkeySpecies = Object.values(MonkeySpecies);

  constructor(private fb: FormBuilder, private monkeyService: MonkeyService) {
    this.getMonkeysBySpeciesForm = this.fb.group({
      species: [''],
    });
  }

 onSubmit() {
  if (this.getMonkeysBySpeciesForm.valid) {
    const species = this.getMonkeysBySpeciesForm.value.species; 
    console.log('Selected Species:', species);

    this.monkeyService.getMonkeysBySpecies(species).subscribe({
      next: (response) => {
        console.log('API Response:', response);
        this.monkeyData = response;

        if (this.monkeyData.length === 0) {
          Swal.fire({
            icon: 'info',
            title: 'No Monkeys Found',
            text: `No monkeys found for the selected species: ${species}.`,
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
      },
    });
  }
}
}

