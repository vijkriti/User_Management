import { Component, OnInit } from '@angular/core';
import { SideNavComponent } from '../side-nav/side-nav.component';
import { User } from '../../models/user';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import * as XLSX from 'xlsx';
import { HeaderComponent } from '../header/header.component';
import { MatPaginatorModule } from '@angular/material/paginator';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [MatPaginatorModule,HeaderComponent,SideNavComponent,CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css' 
})
export class DashboardComponent  implements OnInit {
  users: User[] = [];
  activeUsers: User[] = [];
  inactiveUsers: User[] = [];
  fileName= 'ExcelSheet.xlsx';
  filteredUsers: User[] = [];
  selectedFilter = 'all';
  
  totalItems = 0;
  pageSize = 4;
  currentPage = 0;
  items: User[] = [];

  constructor(private userService: UserService, private router:Router) {}

  ngOnInit(): void {
    this.loadUsers();
  }


  loadUsers(): void {
    this.userService.getUsers().subscribe(users => {
      this.users = users;
      this.activeUsers = users.filter(user => user.isActive);
      this.inactiveUsers = users.filter(user => !user.isActive);
      this.filteredUsers = users;
      this.totalItems = this.users.length;
      this.currentPage = 0;
      this.items = this.getData(this.currentPage, this.pageSize);
    });
  }

  editUser(user: User): void {
    this.router.navigate(['user/edit-user', user.userId]);
  }

  deleteUser(userId: number): void {
    this.userService.deleteUser(userId).subscribe(() => {
      this.loadUsers();
    });
  }

  pageChanged(event: any): void {
    this.currentPage = event.pageIndex;
    this.items = this.getData(this.currentPage, this.pageSize);
  }

  getData(currentPage: number, pageSize: number): User[] {
    const startIndex = currentPage * pageSize;
    const endIndex = startIndex + pageSize;
    return this.filteredUsers.slice(startIndex, endIndex);
  }
  
  filterUsers(filter: string): void {
    this.selectedFilter = filter;
    switch (filter) {
      case 'active':
        this.filteredUsers = this.activeUsers;
        break;
      case 'inactive':
        this.filteredUsers = this.inactiveUsers;
        break;
      default:
        this.filteredUsers = this.users;
        break;
    }
    this.totalItems = this.filteredUsers.length;
    this.currentPage = 0;
    this.items = this.getData(this.currentPage, this.pageSize);
  
  }


exportexcel(): void {
  const columnsToExport = ['firstName','middleName ','lastName', 'dateOfBirth' ,'email', 'phone','primaryCity','primaryState'];
  const data = this.users.map(user => {
    return columnsToExport.map(column => {
      switch (column) {
        case 'firstName':
          return user.firstName;
        case 'middleName':
          return user.middleName;
        case 'lastName':
          return user.lastName;
        case 'dateOfBirth':
          return user.dateOfBirth;
        case 'email':
          return user.email;
        case 'phone':
          return user.phone;
        case 'primaryCity':
          return user.primaryCity;
        case 'primaryState':
          return user.primaryState;
        default:
          return '';
      }
    });
  });
 

  const ws: XLSX.WorkSheet = XLSX.utils.aoa_to_sheet([['FirstName','MiddleName', 'LastName', 'DOB' ,'Email', 'Phone','City','State'], ...data]);
  const wb: XLSX.WorkBook = XLSX.utils.book_new();
  XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
  XLSX.writeFile(wb, this.fileName);
}
}