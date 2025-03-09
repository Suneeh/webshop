import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./shell/shell.component').then((c) => c.ShellComponent),
    children: [
      {
        path: 'dashboard',
        loadComponent: () => import('./dashboard/dashboard.component').then((c) => c.DashboardComponent),
      },
      {
        path: 'category/:id',
        loadComponent: () => import('./category/category.component').then((c) => c.CategoryComponent),
      },
      {
        path: 'product/:id',
        loadComponent: () => import('./product/product-detail/product-detail.component').then((c) => c.ProductDetailComponent),
      },
    ],
  },
];
