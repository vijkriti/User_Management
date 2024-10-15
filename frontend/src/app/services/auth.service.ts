import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import {jwtDecode} from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7243/api/Login/authenticate';
  private tokenKey = 'token';

  constructor(private http: HttpClient, private router: Router, private toastr: ToastrService) {}

  login(email: string, password: string): Observable<any> {
    return this.http.post<any>(this.apiUrl, { email, password }).pipe(
      tap(response => {
        if (response && response.token) {
          localStorage.setItem(this.tokenKey, JSON.stringify(response)); 
          this.toastr.success('Login Successful', 'Success!');
        }
      }),
      catchError(error => {
        if (error.status === 401 && error.error === 'Please activate your account.') {
          this.toastr.error('Please activate your account.', 'Inactive Account');
        } else {
          this.toastr.error('Invalid Credentials','Login Failed');
        }
        return throwError(error);
      })
    );
  }

  getToken(): string | null {
    const storedResponse = localStorage.getItem(this.tokenKey);
    if (storedResponse) {
      const response = JSON.parse(storedResponse);
      return response.token.token;
    }
    return null;
  }  
 

  getUserClaims(): any {
    const token = this.getToken();
    if (token) {
      try {
        console.log(token);
        return jwtDecode(token);
      } catch (e) {
        console.error('Failed to decode JWT token:', e);
        return null;
      }
    }
    return null;
  }


  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.toastr.success('Logged out successfully', 'Success!');
    this.router.navigate(['']);
  }

  isAuthenticated(): boolean {
    return this.getToken() !== null; 
  }
}
