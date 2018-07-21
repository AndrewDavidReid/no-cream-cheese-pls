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
import { SharedModule } from "./shared/shared.module";
import { environment } from "../environments/environment";
import { StoreModule } from "@ngrx/store";

import * as fromStore from "./store";
import { EffectsModule } from "@ngrx/effects";
import { AppRoutingModule } from "./app-routing.module";

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatToolbarModule,
    SharedModule,
    AppRoutingModule,
    StoreModule.forRoot(fromStore.reducers),
    StoreRouterConnectingModule.forRoot({
      stateKey: 'router'
    }),
    EffectsModule.forRoot(fromStore.effects),
    StoreDevtoolsModule.instrument({
      name: "NoCreamCheesePls Store DevTools",
      logOnly: environment.production
    })
  ],
  providers: [{provide: RouterStateSerializer, useClass: fromStore.CustomSerializer}],
  bootstrap: [AppComponent]
})
export class AppModule {}
