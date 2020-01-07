import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";
import { AppModule } from "./components/app/app.module";
// import { enableProdMode } from "@angular/core";

// TODO: Enable on production
// enableProdMode();

platformBrowserDynamic()
  .bootstrapModule(AppModule)
  .catch(err => console.log(err));
