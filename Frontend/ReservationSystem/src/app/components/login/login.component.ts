import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.authService.login(this.username, this.password, "").subscribe(
      (response) => {
        localStorage.setItem('token', response.token);
        this.router.navigate(['/reservations']);
      },
      (error) => {
        console.error('Login failed', error);
      }
    );
  }

  goToRegister(){
    this.router.navigate(['/Register']);
  }
}
