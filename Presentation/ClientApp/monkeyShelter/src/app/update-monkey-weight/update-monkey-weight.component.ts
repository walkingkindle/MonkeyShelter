import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MonkeyService } from '../services/monkey.service';
import { CommonModule } from '@angular/common';
import { MonkeyWeightRequest } from '../models/MonkeyWeightRequest';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-monkey-weight',
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './update-monkey-weight.component.html',
  styleUrl: './update-monkey-weight.component.css'
})
export class UpdateMonkeyWeightComponent {
monkeyForm: FormGroup;


constructor(private fb: FormBuilder,private monkeyService: MonkeyService) {
      this.monkeyForm = this.fb.group({
        id: ['', Validators.required],
        weight: [0, [Validators.required, Validators.min(0.1)]],
      });

}

onSubmit(): void {
  if (this.monkeyForm.valid) {
    const formValue = this.monkeyForm.value;
    const request: MonkeyWeightRequest = {
      monkeyId: formValue.id,
      newMonkeyWeight: formValue.weight
    };

    this.monkeyService.updateMonkeyWeight(request).subscribe({
      next: (response) => {
        console.log('Submitted:', response);
        Swal.fire({
          icon: 'success',
          title: 'Weight Updated',
          text: 'Monkey weight has been successfully updated!',
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
})
}
}
}
