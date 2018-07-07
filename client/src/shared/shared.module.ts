import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import * as fromServices from "./services";
import * as fromComponents from "./components";

@NgModule({
  imports: [CommonModule],
  providers: [fromServices.services],
  declarations: [...fromComponents.components],
  exports: [...fromComponents.components]
})
export class SharedModule {}
