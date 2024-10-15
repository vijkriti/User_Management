import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter, RouterModule } from '@angular/router';
import { routes } from '../app.routes';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    UserRoutingModule,
    RouterModule
  ],
  providers:[provideRouter(routes),provideHttpClient(),]
})
export class UserModule { }
