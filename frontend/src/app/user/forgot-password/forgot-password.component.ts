import { Component } from '@angular/core';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ResetPasswordService } from '../../services/reset-password.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [MatProgressSpinnerModule,MatCardModule,MatInputModule,MatButtonModule, CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent {
  forgotPasswordForm!: FormGroup;

  isLoading = false;

  constructor(private router:Router, private resetService:ResetPasswordService) { }

  ngOnInit():void {
    this.forgotPasswordForm = new FormGroup({
      Email: new FormControl('', [Validators.required, Validators.email, Validators.maxLength(100)]),
    });
  }

  onSubmit(){
    if(this.forgotPasswordForm.valid)
    {
      this.isLoading = true;
      const email = this.forgotPasswordForm.get('Email')?.value;
      this.resetService.sendResetPasswordLink(email).subscribe({ next:(response) => {
        this.router.navigate(['/user/sent']);
        
        this.isLoading = false;
      }, error:(error) => {
        console.error(error);
      }});
    }
  }
}
