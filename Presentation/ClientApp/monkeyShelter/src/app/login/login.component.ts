import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {
    username: string;

    constructor(private authService: AuthService, private router: Router) {}

    onLogin(): void {
        this.authService.login(this.username).subscribe((response: any) => {
            localStorage.setItem('token', response.token); // Store the JWT token
            this.router.navigate(['/add-monkey']); // Redirect to a protected page
        }, error => {
            console.error('Login failed', error);
        });
    }
}
