import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private url = environment.apiUrl + '/dashboard';

  constructor(private http: HttpClient) { }

  getDashboard() {

    return this.http.get<any>(
      this.url
    );

  }
}
