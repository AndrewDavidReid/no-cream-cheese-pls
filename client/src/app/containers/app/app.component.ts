import { Component, OnInit } from "@angular/core";
import { NavbarService } from "../../../shared/services";

@Component({
  selector: "nccp-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent implements OnInit {
  constructor(public navbarService: NavbarService) {}

  public NavbarTitle: string;
  public ShowLoadingSpinner: boolean;

  ngOnInit() {
    this.navbarService.Title$.subscribe(title => (this.NavbarTitle = title));
  }
}
