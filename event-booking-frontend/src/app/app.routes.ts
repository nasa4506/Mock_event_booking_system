import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { 
    path: 'login', 
    loadComponent: () => import('./features/auth/login/login.component').then(m => m.LoginComponent) 
  },
  {
    path: '',
    canActivate: [authGuard],
    loadComponent: () => import('./core/layout/main-layout/main-layout.component').then(m => m.MainLayoutComponent),
    children: [
      { 
          path: 'events', 
          loadComponent: () => import('./features/events/event-list/event-list.component').then(m => m.EventListComponent) 
      },
      { 
          path: 'events/:id', 
          loadComponent: () => import('./features/events/event-detail/event-detail.component').then(m => m.EventDetailComponent) 
      },
      { 
          path: 'events/:id/book', 
          loadComponent: () => import('./features/events/booking/booking.component').then(m => m.BookingComponent) 
      }
    ]
  },
  { path: '**', redirectTo: '/login' }
];
