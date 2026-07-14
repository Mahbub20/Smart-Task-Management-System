import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  fullName = '';
  email = '';
  password = '';

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  register() {


    const data = {

      fullName: this.fullName,

      email: this.email,

      password: this.password

    };


    this.authService.register(data)
      .subscribe({

        next: () => {

          alert(
            'Registration successful'
          );


          this.router.navigate([
            '/login'
          ]);

        }


      });


  }

}
