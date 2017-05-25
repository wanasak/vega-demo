import { ErrorHandler, Inject, NgZone } from "@angular/core";
import { ToastyService } from "ng2-toasty";

export class AppErrorHanlder implements ErrorHandler {

    constructor(@Inject(ToastyService) private toastyService: ToastyService, private ngZone: NgZone) { }

    handleError(error: any): void {
        this.ngZone.run(() => {
                this.toastyService.error({
                title: 'Error',
                msg: 'An unexpected error happened.',
                theme: 'bootstrap',
                showClose: true,
                timeout: 3000
            });
        });
    }
}