import { Component, OnInit } from "@angular/core";
import { ShoppingList } from "../../models/shopping-list";

@Component({
  selector: "nccp-shopping-lists",
  templateUrl: "./shopping-lists.component.html",
  styleUrls: ["./shopping-lists.component.scss"]
})
export class ShoppingListsComponent implements OnInit {
  constructor(private store: Store<>) {
  }

  public ShoppingLists: ShoppingList[];

  ngOnInit() {
    console.log("Arrived at ShoppingListsComponent");
  }

  public AddNewList() {}
}
