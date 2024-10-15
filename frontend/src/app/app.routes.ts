import { Routes } from '@angular/router';
import { LoginComponent } from './user/login/login.component';


export const routes: Routes = [
    {path:'',component:LoginComponent},
    {path:'user',loadChildren:()=>import('./user/user.module').then(m=>m.UserModule)},
    
];
