import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit{

  username : string | null | undefined;
  imageUrl: any;

  ngOnInit(): void {
    const storedResponse = localStorage.getItem('token');
    if (storedResponse) {
      const response = JSON.parse(storedResponse);
        this.username = response.token.firstName;
        this.imageUrl = response.token.imagePath;
    }
  }
}
