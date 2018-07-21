import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";

const ROUTES: Routes = [
  { path: "", pathMatch: "full", redirectTo: "shopping-lists" },
  {
    path: "shopping-lists",
    loadChildren: "./shopping-list/shopping-list.module#ShoppingListModule"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(ROUTES)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
