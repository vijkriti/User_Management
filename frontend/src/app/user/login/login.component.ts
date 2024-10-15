import { Component } from '@angular/core';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatCardModule,MatInputModule,MatButtonModule,CommonModule,ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  signInForm!: FormGroup;


  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) {}

  ngOnInit():void {
    this.signInForm = new FormGroup({
      Email: new FormControl('', [Validators.required, Validators.email, Validators.maxLength(100)]),
      Password: new FormControl('', [Validators.required])
    });
  }

  onSubmit() {
    if (this.signInForm.valid) {
      const email = this.signInForm.get('Email')?.value;
      const password = this.signInForm.get('Password')?.value;

      this.authService.login(email, password).subscribe({
        next: () => {
          this.router.navigate(['/user/dashboard']);
        }
      });}
      else{
        this.toastr.error('Invalid credentials');
      }
    }
  }
