import { Component, OnInit } from '@angular/core';
import { NavbarService } from './services/navbar.service';
import { LoadingService } from './services/loading.service';

@Component({
  selector: 'nccp-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit
{
  constructor(public navbarService: NavbarService,
              public loadingService: LoadingService)
  { }

  public NavbarTitle: string;
  public ShowLoadingSpinner: boolean;

  ngOnInit()
  {
    this.navbarService.Title$.subscribe(title => this.NavbarTitle = title);
    this.loadingService.Loading$.subscribe(showLoading => this.ShowLoadingSpinner = showLoading);
  }
}
