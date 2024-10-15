import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-email-sent',
  standalone: true,
  imports: [MatCardModule,MatInputModule, RouterModule],
  templateUrl: './email-sent.component.html',
  styleUrl: './email-sent.component.css'
})
export class EmailSentComponent {

}
