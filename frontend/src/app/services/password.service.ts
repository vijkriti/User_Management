import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
 
@Injectable({
  providedIn: 'root'
})
export class PasswordService {
  private apiUrl = 'https://localhost:7243/api/ChangePassword';
 
  constructor(private http: HttpClient) {}
 
  changePassword(oldPassword: string, newPassword: string, token: string): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const payload = { oldPassword, newPassword, token };
 
    return this.http.post<any>(`${this.apiUrl}/ChangePassword`, payload, { headers });
  }
}