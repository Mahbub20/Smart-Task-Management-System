import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  email = '';
  password = '';

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  login() {

    const data = {

      email: this.email,

      password: this.password

    };


    this.authService.login(data)
      .subscribe({

        next: (response) => {

          this.authService.saveToken(
            response.token
          );

          this.router.navigate([
            '/dashboard'
          ]);
        },

        error: (error) => {

          alert(
            'Invalid email or password'
          );

        }

      });


  }
}
