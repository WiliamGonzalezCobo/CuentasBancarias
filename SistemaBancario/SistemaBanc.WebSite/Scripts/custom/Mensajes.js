function mensaje(state, message) {
    toastr.options.hideDuration = 0;
    toastr.clear();

    toastr.options.closeButton = false;
    toastr.options.progressBar = true;
    toastr.options.debug = false;
    toastr.options.positionClass = "toast-bottom-full-width";
    toastr.options.showDuration = 330;
    toastr.options.hideDuration = 330;
    toastr.options.timeOut = 5000;
    toastr.options.extendedTimeOut = 1000;
    toastr.options.showEasing = "swing";
    toastr.options.hideEasing = "swing";
    toastr.options.showMethod = "slideDown";
    toastr.options.hideMethod = "slideUp";

    toastr[state](message);
}