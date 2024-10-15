import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ResetPassword } from '../../models/reset-password';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ResetPasswordService } from '../../services/reset-password.service';
import { ToastrService } from 'ngx-toastr';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';


@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [MatProgressSpinnerModule ,MatCardModule,MatInputModule,MatButtonModule,CommonModule,ReactiveFormsModule],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm!: FormGroup;
  emailToReset!:string;
  emailToken!:string;
  resetPasswordObj = new ResetPassword();


  constructor(private formBuilder: FormBuilder,private router:Router, private route: ActivatedRoute, private resetService: ResetPasswordService, private toast:ToastrService){}

  ngOnInit(): void {
    this.resetPasswordForm = this.formBuilder.group({
      password: ['', [Validators.required, Validators.maxLength(20)]],
      confirmPassword: ['', Validators.required]
    }, { validators: this.passwordMatchValidator });

    this.route.queryParams.subscribe(val=>{
      this.emailToReset = val['email'];
      let token = val['code'];
      this.emailToken = token.replace(/ /g,'+');
  })
  }

    passwordMatchValidator(group: FormGroup): { [key: string]: boolean } | null {
      const password = group.get('password')?.value;
      const confirmPassword = group.get('confirmPassword')?.value;
      return password === confirmPassword ? null : { 'mismatch': true };
    }

  onSubmit(){
    if(this.resetPasswordForm.valid){

      this.resetPasswordObj.email = this.emailToReset;
      this.resetPasswordObj.newPassword = this.resetPasswordForm.value.password;
      this.resetPasswordObj.confirmPassword = this.resetPasswordForm.value.confirmPassword;
      this.resetPasswordObj.emailToken= this.emailToken;


      this.resetService.resetPassword(this.resetPasswordObj)
      .subscribe({
        next:(res)=>
        {
          this.toast.success('Password Reset Succesfully');
          this.router.navigate(['']);
        },
        error:(err)=>
        {
          this.toast.error('Something went wrong');
        }
      })
    }
  }


}