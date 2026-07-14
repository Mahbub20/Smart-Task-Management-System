import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  dashboard: any;

  constructor(private service: DashboardService) { }

  ngOnInit(): void {
    this.service.getDashboard()
      .subscribe({

        next: (response) => {

          this.dashboard = response;

        }


      });
  }

}
