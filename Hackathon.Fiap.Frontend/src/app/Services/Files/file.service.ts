import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor() { }

  downloadFile(data: any) {
    const blob = new Blob([data], { type: 'application/zip' });
    const url= window.URL.createObjectURL(blob);
    window.open(url);
  }
}
