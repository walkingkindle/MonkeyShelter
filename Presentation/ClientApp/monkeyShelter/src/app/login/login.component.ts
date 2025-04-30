import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import Swal from 'sweetalert2';  // Import SweetAlert

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    imports:[ReactiveFormsModule],
    styleUrls: ['./login.component.css']
})
export class LoginComponent {
    getMonkeysBySpeciesForm: FormGroup;

    constructor(private authService: AuthService, private router: Router, fb: FormBuilder) {
        // Initialize the form group correctly
        this.getMonkeysBySpeciesForm = fb.group({
            username: ['', Validators.required] // Corrected here
        });
    }

    onLogin(): void {
        const username = this.getMonkeysBySpeciesForm.get('username')?.value; // Get the username from the form

        if (username) {
            this.authService.login(username).subscribe({
                next: (response: any) => {
                    // Store the JWT token
                    localStorage.setItem('token', response.token);
                    
                    // Show success message using SweetAlert
                    Swal.fire({
                        icon: 'success',
                        title: 'Login Successful',
                        text: 'You have successfully logged in!',
                    });

                    // Redirect to a protected page
                    this.router.navigate(['/add-monkey']);
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

                    // Show error message using SweetAlert
                    Swal.fire({
                        icon: 'error',
                        title: 'Login Failed',
                        text: errorMessage,
                    });
                }
            });
        } else {
            // Handle case when username is not provided
            console.error('Username is required');
            Swal.fire({
                icon: 'error',
                title: 'Input Error',
                text: 'Username is required to log in.',
            });
        }
    }
}
