import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  usename: string = '';
  password: string = '';

  error: string = '';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) {
  }

  onSubmit() {
    this.error = '';
    if (!!this.usename && this.password) {
      this.router.navigate(['/']);
    } else {
      this.error = 'Please fill user name and password.'
    }
  }
}
