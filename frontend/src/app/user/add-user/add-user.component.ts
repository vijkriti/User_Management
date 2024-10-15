import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user';
import { SideNavComponent } from '../side-nav/side-nav.component';
import { Router } from '@angular/router';
import { HeaderComponent } from '../header/header.component';

@Component({
  selector: 'app-add-user',
  standalone: true,
  imports: [HeaderComponent, SideNavComponent , MatIconModule, MatFormFieldModule, ReactiveFormsModule , CommonModule, MatInputModule, MatDatepickerModule, MatSelectModule, MatOptionModule, MatNativeDateModule],
  templateUrl: './add-user.component.html',
  styleUrl: './add-user.component.css'
})
export class AddUserComponent {
  userForm: FormGroup;
  selectedFile: File | null = null;
  imageUrl: string | ArrayBuffer | null = '';

  constructor(private fb: FormBuilder, private userService: UserService, private router:Router) {
    this.userForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.maxLength(50)]],
      middleName: [''],
      lastName: ['', [Validators.required, Validators.maxLength(50)]],
      gender: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      email: ['', [Validators.required, Validators.email, Validators.maxLength(100)]],
      dateOfJoining: ['', Validators.required],
      phone: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      alternatePhone: [''],
      primaryAddress: ['', [Validators.required, Validators.maxLength(255)]],
      primaryCity: ['', [Validators.required, Validators.maxLength(100)]],
      primaryState: ['', [Validators.required, Validators.maxLength(100)]],
      primaryCountry: ['', [Validators.required, Validators.maxLength(100)]],
      primaryZipCode: ['', [Validators.required, Validators.maxLength(6)]],
      secondaryAddress: [''],
      secondaryCity: [''],
      secondaryState: [''],
      secondaryCountry: [''],
      secondaryZipCode: [''],
      imgPath: ['']
    });
  }

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
    if (this.selectedFile) {
      this.userForm.patchValue({ imgPath: this.selectedFile.name });
      const reader = new FileReader();
      reader.onload = () => {
        this.imageUrl = reader.result as string;
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }

  onSubmit(): void {
    if (this.userForm.invalid) {
      console.log('Form is invalid');
      return;
    }
  
    const formValue = this.userForm.value;
    const user: User = {
      ...formValue,
      dateOfBirth: new Date(formValue.dateOfBirth).toISOString().split('T')[0], 
      dateOfJoining: new Date(formValue.dateOfJoining).toISOString().split('T')[0], 
    };
  
    const formData: FormData = new FormData();
    formData.append('user', JSON.stringify(user));
    if (this.selectedFile) {
      formData.append('file', this.selectedFile, this.selectedFile.name);
    }
  
    this.userService.addUser(formData).subscribe({
      next: (response) => {
        this.router.navigate(["/user/dashboard"]);
      },
      error: (error) => {
        console.error('Error creating user:', error);
      }
    });
  }
}  