import { Component, OnInit } from "@angular/core";
import { ShoppingList } from "../../models/shopping-list";

@Component({
  selector: "nccp-shopping-lists",
  templateUrl: "./shopping-lists.component.html",
  styleUrls: ["./shopping-lists.component.scss"]
})
export class ShoppingListsComponent implements OnInit {
  constructor() {}

  public ShoppingLists: ShoppingList[];

  ngOnInit() {}

  public AddNewList() {}
}
