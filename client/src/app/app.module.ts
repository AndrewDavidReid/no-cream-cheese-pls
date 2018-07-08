import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { StoreRouterConnectingModule, routerReducer, RouterStateSerializer } from '@ngrx/router-store';
import { AppComponent } from "./containers/app/app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import {
  MatToolbarModule,
} from "@angular/material";
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { HttpClientModule } from "@angular/common/http";
import { SharedModule } from "../shared/shared.module";
import { Routes, RouterModule } from "@angular/router";
import { environment } from "../environments/environment";
import { StoreModule } from "../../node_modules/@ngrx/store";

import * as fromStore from "./store";
import { EffectsModule } from "../../node_modules/@ngrx/effects";

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
    MatToolbarModule,
    SharedModule,
    RouterModule.forRoot(ROUTES),
    StoreModule.forRoot(fromStore.reducers),
    StoreRouterConnectingModule.forRoot({
      stateKey: 'router'
    }),
    EffectsModule.forRoot(fromStore.effects),
    environment.production ? [] : StoreDevtoolsModule.instrument()
  ],
  providers: [{provide: RouterStateSerializer, useClass: fromStore.CustomSerializer}],
  bootstrap: [AppComponent]
})
export class AppModule {}
