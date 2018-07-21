import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import * as fromServices from "./services";
import * as fromComponents from "./components";
import * as fromContainers from "./containers";
import { MatListModule, MatIconModule } from "@angular/material";

export const ROUTES: Routes = [
  {
    path: '',
    component: fromContainers.ShoppingListsComponent
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(ROUTES),
    MatListModule,
    MatIconModule,
  ],
  providers: [fromServices.services],
  declarations: [...fromComponents.components, ...fromContainers.containers],
  exports: [...fromComponents.components, ...fromContainers.containers]
})
export class ShoppingListModule {}
