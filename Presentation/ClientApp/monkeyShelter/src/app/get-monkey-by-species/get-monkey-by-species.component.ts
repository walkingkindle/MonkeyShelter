import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MonkeyService } from '../services/monkey.service';

@Component({
  selector: 'app-get-monkey-by-species',
  imports: [ReactiveFormsModule],
  templateUrl: './get-monkey-by-species.component.html',
  styleUrl: './get-monkey-by-species.component.css'
})
export class GetMonkeyBySpeciesComponent {

  constructor(private fb: FormBuilder,private monkeyService: MonkeyService){
  }

  onSubmit() {
      this.monkeyService.getMonkeysBySpecies().subscribe({
        next: (response) => {
          console.log('Submitted:', response);
        },
        error: (err) => {
          console.error('Error:', err);
        }
      });
    } 
}
