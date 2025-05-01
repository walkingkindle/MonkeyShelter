import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MonkeySpecies } from '../enums/species';
import { MonkeyService } from '../services/monkey.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { MonkeyEntryRequest } from '../models/MonkeyEntryRequest';
import Swal from 'sweetalert2';
import { MonkeyReportResponse } from '../models/MonkeyReportResponse';
import { MonkeyTableComponent } from "../monkey-report-table/monkey-report-table.component";

@Component({
  selector: 'app-monkey-form',
  imports: [ReactiveFormsModule, CommonModule, MonkeyTableComponent],
  templateUrl: './add-monkey.component.html',
  styleUrl: './add-monkey.component.css',
})
export class MonkeyFormComponent {
  monkeyForm: FormGroup;
  monkeyData: MonkeyReportResponse[] = [];
  monkeySpeciesList = Object.values(MonkeySpecies)
    constructor(private fb: FormBuilder,private monkeyService: MonkeyService) {
      this.monkeyForm = this.fb.group({
        name: ['', Validators.required],
        species: ['', Validators.required],
        weight: [0, [Validators.required, Validators.min(0.1)]],
      });
    }
    successMessage: string | null = null;
    errorMessage: string | null = null;

    onSubmit(): void {
  this.successMessage = null;
  this.errorMessage = null;

  if (this.monkeyForm.valid) {
    const formValue = this.monkeyForm.value;
    const request: MonkeyEntryRequest = {
      name: formValue.name,
      species: formValue.species,
      weight: formValue.weight,
    };

    this.monkeyService.admitMonkeyToShelter(request).subscribe((response) => {
      if (typeof response === 'string') {
        // Handle error messages (e.g. from 401, 403, 400)
        Swal.fire({
          icon: 'error',
          title: response === 'You are not authorized to perform this action.'
            ? 'Unauthenticated'
            : 'Creation Failed',
          text: response,
        });
        return;
      }

      // Success
      this.monkeyData = [response];
      Swal.fire({
        icon: 'success',
        title: 'Monkey Created',
        text: 'Monkey entry has been successfully submitted!',
      });
      this.monkeyForm.reset();
    });
  } else {
    this.errorMessage = 'Form is invalid. Please check the inputs.';
    Swal.fire({
      icon: 'error',
      title: 'Form Invalid',
      text: this.errorMessage,
    });
  }
}

private isMonkeyReportResponse(response: string | MonkeyReportResponse): response is MonkeyReportResponse {
  return (response as MonkeyReportResponse).Id !== undefined; // Check if it's a MonkeyReportResponse based on the properties it should have
}

}


