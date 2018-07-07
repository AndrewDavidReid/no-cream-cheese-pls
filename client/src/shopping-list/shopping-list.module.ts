import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import * as fromServices from "./services";
import * as fromComponents from "./components";
import * as fromContainers from "./containers";

@NgModule({
  imports: [CommonModule],
  providers: [fromServices.services],
  declarations: [...fromComponents.components, ...fromContainers.containers],
  exports: [...fromComponents.components, ...fromContainers.containers]
})
export class SharedModule {}
