import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";
import { enableProdMode }         from "@angular/core";
import { AppModule }              from "./components/app/app.module";

const isDevelopment = true;
const platform = platformBrowserDynamic();

if (isDevelopment) {
    localStorage.removeItem("access_token");
    localStorage.removeItem("role");

    if (module.hot) {
        module.hot.accept();
        module.hot.dispose(() => {
            const oldRootElement = document.querySelector("app");
            const newRootElement = document.createElement("app");
            oldRootElement!.parentNode!.insertBefore(newRootElement, oldRootElement);
            platform.destroy();
        });
    }
} else {
    enableProdMode();
}

platform.bootstrapModule(AppModule);
