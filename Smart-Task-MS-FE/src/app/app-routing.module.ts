import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { AuthGuard } from './guards/auth.guard';
import { ProjectsComponent } from './features/projects/projects.component';
import { TasksComponent } from './features/tasks/tasks.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },


  {
    path: 'register',
    component: RegisterComponent
  },


  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard]
  },

  {
    path: 'projects',
    component: ProjectsComponent,
    canActivate: [AuthGuard]
  },

  {
    path: 'projects/:id/tasks',

    component: TasksComponent,

    canActivate: [
      AuthGuard
    ]

  },

  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
