import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MonkeyService } from '../services/monkey.service';
import { MonkeyDepartureRequest } from '../models/MonkeyDepartureRequest';
import Swal from 'sweetalert2';

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
      this.monkeyService.departMonkeyFromShelter(request.monkeyId).subscribe({
        next: (response) => {
          console.log('Submitted:', response);
          Swal.fire({
          icon: 'success',
          title: 'Monkey Deleted',
          text: 'The monkey has been sucessfully deleted.',
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
