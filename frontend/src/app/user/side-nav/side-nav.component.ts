import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [MatIconModule,RouterModule],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.css'
})
export class SideNavComponent {
  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) {}

  logout(){
    try{
    this.authService.logout();
    this.router.navigate(['']);
    }
    catch(error){
      this.toastr.error('Login Failed', 'Error!');
    }
  }
}
