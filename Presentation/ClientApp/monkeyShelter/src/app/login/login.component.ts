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
        this.getMonkeysBySpeciesForm = fb.group({
            username: ['', Validators.required]
        });
    }

    onLogin(): void {
        const username = this.getMonkeysBySpeciesForm.get('username')?.value;

        if (username) {
            this.authService.login(username).subscribe({
                next: (response: any) => {
                    localStorage.setItem('token', response.token);
                    
                    Swal.fire({
                        icon: 'success',
                        title: 'Login Successful',
                        text: 'You have successfully logged in!',
                    });

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

                    Swal.fire({
                        icon: 'error',
                        title: 'Login Failed',
                        text: errorMessage,
                    });
                }
            });
        } else {
            console.error('Username is required');
            Swal.fire({
                icon: 'error',
                title: 'Input Error',
                text: 'Username is required to log in.',
            });
        }
    }
}
