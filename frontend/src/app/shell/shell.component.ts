import { Component, ChangeDetectionStrategy } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterModule } from '@angular/router';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Observable, of } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-shell',
  standalone: true,
  imports: [MatSidenavModule, RouterModule, MatListModule, MatIconModule, MatToolbarModule, CommonModule],
  templateUrl: './shell.component.html',
  styleUrl: './shell.component.scss',
})
export class ShellComponent {
  public categories = new Observable<Category[]>();
  year = new Date().getFullYear();
  constructor() {
    this.categories = of([
      { id: 1, name: '1st Category', childCategories: [] },
      { id: 2, name: '2nd Category', childCategories: [] },
      {
        id: 3,
        name: '3rd Category',
        childCategories: [
          { id: 4, name: '4th Category', childCategories: [] },
          { id: 5, name: '4th Category', childCategories: [] },
        ],
      },
    ]);
  }
}

interface Category {
  id: number;
  name: string;
  childCategories: Category[];
}
