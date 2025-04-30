import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MonkeySpecies } from '../enums/species';
import { MonkeyService } from '../services/monkey.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { MonkeyEntryRequest } from '../models/MonkeyEntryRequest';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-monkey-form',
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './add-monkey.component.html',
  styleUrl: './add-monkey.component.css',
})
export class MonkeyFormComponent {
  monkeyForm: FormGroup;
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
      species: formValue.species,  // Ensure species is an integer
      weight: formValue.weight,
    };
    this.monkeyService.admitMonkeyToShelter(request).subscribe({
        next: (response) => {
        console.log('Submitted:', response);
        Swal.fire({
          icon: 'success',
          title: 'Monkey created',
          text: 'Monkey entry has been sucessfully submitted!',
        });
      },
    error: (err) => {
  console.error('Full error response:', err);

  let errorMessage = 'An unexpected error occurred.';

  if (err.error) {
    if (typeof err.error === 'string') {
      errorMessage = err.error;

    } else if (err.error.message) {
      errorMessage = err.error.message;

    } else {
      try {
        errorMessage = JSON.stringify(err.error);
      } catch {
        errorMessage = 'Error occurred but could not parse details.';
      }
    }
  }

  Swal.fire({
    icon: 'error',
    title: 'Creation Failed',
    text: errorMessage,
  });
}
    });
  } else {
    this.errorMessage = 'Form is invalid. Please check the inputs.';
  }
}

  }
