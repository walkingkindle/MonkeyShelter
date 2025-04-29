import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MonkeySpecies } from '../enums/species';
import { MonkeyService } from '../services/monkey.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

@Component({
  selector: 'app-monkey-form',
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './monkey-form.component.html',
  styleUrl: './monkey-form.component.css',
})
export class MonkeyFormComponent {
  monkeyForm: FormGroup;
   // Inject the service
  monkeySpeciesList = Object.values(MonkeySpecies)
    constructor(private fb: FormBuilder,private monkeyService: MonkeyService) {
      this.monkeyForm = this.fb.group({
        name: ['', Validators.required],
        species: ['', Validators.required],
        weight: [0, [Validators.required, Validators.min(0.1)]],
      });
    }
    onSubmit(): void {
    if (this.monkeyForm.valid) {
      const request = this.monkeyForm.value;
      this.monkeyService.admitMonkeyToShelter(request).subscribe({
        next: (response) => {
          console.log('Submitted:', response);
          // Handle successful submission (e.g., show a message, reset form)
        },
        error: (err) => {
          console.error('Error:', err);
          // Handle error (e.g., show an error message)
        }
      });
    } else {
      console.error('Form is invalid');
    }
  }

  ngOnInit(){
    console.log("Component loaded.")
  }
  }
