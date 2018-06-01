import { Component, OnInit } from '@angular/core';
import { ShoppingList } from '../../models/shopping-list';
import { NavbarService } from '../../services/navbar.service';
import { LoadingService } from '../../services/loading.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CreateShoppingListResult } from '../../models/command-result/create-shopping-list-result';

@Component({
  selector: 'nccp-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit
{
  constructor(public navbarService: NavbarService,
              public loadingService: LoadingService,
              public httpClient: HttpClient,
              public router: Router)
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

    // this.httpClient.post<CreateShoppingListResult>("/api/shopping-list/create", {}).subscribe(response => {
    //   this.router.navigate(["shopping-list", response.CreatedId]);
    // }, error => {
    //   console.log(error);
    // })
  }
}
