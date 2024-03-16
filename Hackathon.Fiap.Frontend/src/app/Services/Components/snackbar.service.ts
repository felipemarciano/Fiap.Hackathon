import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {

  constructor(private readonly snackBar: MatSnackBar) { }

  openErrorSnackBar(message: string) {
    this.snackBar.open(message, "X", {
      duration: 2000,
      panelClass: ['style-error']
    });
  }

  openSuccessSnackBar(message: string) {
    this.snackBar.open(message, "X", {
      duration: 2000,
      panelClass: ['style-success'],
      
    });
  }
}