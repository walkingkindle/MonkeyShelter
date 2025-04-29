import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MonkeyService } from '../services/monkey.service';
import { MonkeyDepartureRequest } from '../models/MonkeyDepartureRequest';

@Component({
  selector: 'app-remove-monkey',
  imports: [ReactiveFormsModule],
  templateUrl: './remove-monkey.component.html',
  styleUrl: './remove-monkey.component.css'
})
export class RemoveMonkeyComponent {

removeMonkeyForm:FormGroup
    constructor(private fb: FormBuilder,private monkeyService: MonkeyService) {
          this.removeMonkeyForm = this.fb.group({
            id: ['', Validators.required],
    })
}


   onSubmit() : void{
    if (this.removeMonkeyForm.valid) {
      const request :MonkeyDepartureRequest = {monkeyId:this.removeMonkeyForm.get('id')?.value};
      console.log(request)
      this.monkeyService.departMonkeyFromShelter(request).subscribe({
        next: (response) => {
          console.log('Submitted:', response);
          // Handle successful submission (e.g., show a message, reset form)
        },
        error: (err) => {
          console.error('Error:', err);
          // Handle error (e.g., show an error message)
        }
      })
    }
  }
}
