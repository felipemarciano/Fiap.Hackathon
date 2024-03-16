import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatSnackBarModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Hackathon.Fiap.Frontend';

  constructor(private _http: HttpClient){
    this.getBaseUrl().subscribe((data) => {
      localStorage.setItem('apiUrl', data.apiServer.url);
      localStorage.setItem('blobUrl', data.blobServer.url);

      console.log(`[AppComponent][Url]${JSON.stringify(data)}`);
    });
  }

  private getBaseUrl(): Observable<any>{
    return this._http.get('./assets/config/config.json')
  }
}
