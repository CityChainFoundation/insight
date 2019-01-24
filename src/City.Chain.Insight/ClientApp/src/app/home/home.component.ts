import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public currency;

  constructor(private http: HttpClient) {

  }

  ngOnInit() {
    this.http.get('/api/economy/currency').subscribe((data: any) => {
      this.currency = data.result;
      console.log(data);
    });
  }
}
