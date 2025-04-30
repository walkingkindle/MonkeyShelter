import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MonkeyService } from '../services/monkey.service';
import { MonkeyReportResponse } from '../models/MonkeyReportResponse';
import { CommonModule } from '@angular/common';
import { MonkeyTableComponent } from '../monkey-report-table/monkey-report-table.component';
import { MonkeySpecies } from '../enums/species';

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

  // Create an array of the enum values
  monkeySpecies = Object.values(MonkeySpecies);

  constructor(private fb: FormBuilder, private monkeyService: MonkeyService) {
    this.getMonkeysBySpeciesForm = this.fb.group({
      species: [''], // Default value for species
    });
  }

  onSubmit() {
    if (this.getMonkeysBySpeciesForm.valid) {
      const species = this.getMonkeysBySpeciesForm.value.species; // Get the selected species
      console.log('Selected Species:', species); // Log the selected species

      // Pass the selected species as an argument to the service method
      this.monkeyService.getMonkeysBySpecies(species).subscribe({
        next: (response) => {
          console.log('API Response:', response);
          this.monkeyData = response;
        },
        error: (err) => {
          console.error('Error:', err);
        },
      });
    }
  }
}

