import { Component, OnInit } from '@angular/core';
import { ShoppingList } from '../../models/shopping-list';
import { NavbarService } from '../../services/navbar.service';
import { LoadingService } from '../../services/loading.service';

@Component({
  selector: 'nccp-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit
{
  constructor(public navbarService: NavbarService,
              public loadingService: LoadingService)
  { }

  public ShoppingLists: ShoppingList[];

  ngOnInit()
  {
    this.navbarService.SetTitle("My Lists");
  }

  public AddNewList()
  {
    this.loadingService.ShowOverlay();

    setTimeout(() => {
      this.loadingService.HideOverlay();
    }, 3000)
  }
}
