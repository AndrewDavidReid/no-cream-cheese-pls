import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppComponent } from "./containers/app/app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import {
  MatIconModule,
  MatMenuModule,
  MatToolbarModule,
  MatButtonModule,
  MatListModule,
  MatProgressSpinnerModule
} from "@angular/material";
import { HttpClientModule } from "@angular/common/http";
import { SharedModule } from "../shared/shared.module";
import { Routes, RouterModule } from "@angular/router";

const ROUTES: Routes = [
  { path: "", pathMatch: "full", redirectTo: "shopping-lists" },
  {
    path: "shopping-lists",
    loadChildren: "../shopping-list/shopping-list.module#ShoppingListModule"
  }
];

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatMenuModule,
    MatIconModule,
    MatToolbarModule,
    MatButtonModule,
    MatListModule,
    MatProgressSpinnerModule,
    SharedModule,
    RouterModule.forRoot(ROUTES)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
