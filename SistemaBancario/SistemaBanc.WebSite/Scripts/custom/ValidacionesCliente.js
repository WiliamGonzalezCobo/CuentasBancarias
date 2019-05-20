//Validar combos
$.validator.unobtrusive.adapters.add('validarcombos', ['combo'], function (options) {
    options.rules['validarcombos'] = options.params;
    options.messages['validarcombos'] = options.message;
});

$.validator.addMethod("validarcombos", function (value, element, params) {
    if (value == "0") {
        return false;
    }

    return true;
}, '');

//Validar números
$.validator.unobtrusive.adapters.add('validarnumeros', ['numero'], function (options) {
    options.rules['validarnumeros'] = options.params;
    options.messages['validarnumeros'] = options.message;
});

$.validator.addMethod("validarnumeros", function (value, element, params) {
    if (!$.isNumeric(value)) {
        return false;
    }

    return true;
}, '');