import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { PasswordService } from '../../services/password.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css'
})
export class ChangePasswordComponent implements OnInit {
  changePasswordForm!: FormGroup;
  errorMessage: string = '';
  userClaims: any;
  
    constructor(private authService: AuthService,private passwordService: PasswordService) { }
  
  ngOnInit(): void {
    this.changePasswordForm = new FormGroup({
      oldPassword: new FormControl('', Validators.required),
      newPassword: new FormControl('', [Validators.required, Validators.maxLength(20)])
    });
    this.userClaims = this.authService.getUserClaims();
      console.log('User Claims:', this.userClaims);
  }

  changePassword(): void {
    const oldPassword = this.changePasswordForm.get('oldPassword')?.value;
    const newPassword = this.changePasswordForm.get('newPassword')?.value;

    const authData = localStorage.getItem('token');
    if (authData) {
      const parsedAuthData = JSON.parse(authData);
      const token = parsedAuthData.token.token;

      if (token) {
        this.passwordService.changePassword(oldPassword, newPassword, token)
          .subscribe(response => {
            console.log(response);
          }, error => {
            console.error(error);
            this.errorMessage = 'Failed to change password. Please try again.';
          });
      } else {
        console.error('Token is missing in the stored data');
        this.errorMessage = 'No token found in local storage';
      }
    } else {
      console.error('No auth data found in local storage');
      this.errorMessage = 'No auth data found in local storage';
    }
  }
}
