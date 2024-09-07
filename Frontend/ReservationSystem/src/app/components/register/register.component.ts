import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  username: string = '';
  password: string = '';
  email: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  register() {
    this.authService.resgister(this.username, this.password, this.email).subscribe(
      (response) => {
        localStorage.setItem('token', response.token);
        alert('User Created successfully!');
        this.router.navigate(['/Login']);
      },
      (error) => {
        console.error('Register failed', error);
        alert('Error to Created User!');
      }
    );
  }
}
