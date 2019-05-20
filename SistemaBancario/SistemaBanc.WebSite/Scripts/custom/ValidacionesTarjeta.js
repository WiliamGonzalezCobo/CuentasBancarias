// Validar tarjeta
$.validator.unobtrusive.adapters.add('tarjeta', ['nombrecliente'], function (options) {
    options.rules['tarjeta'] = options.params;
    options.messages['tarjeta'] = options.message;
    persistirMensajes(options.message);
});

$.validator.unobtrusive.adapters.add(
'vencetc', ['campomes'], function (options) {
    options.rules['vencetc'] = options.params;
    options.messages['vencetc'] = options.message;
});

var _mensajesIos = '';

function persistirMensajes(datos) {
    if (esValidoLocalStorage()) {
        sessionStorage.setItem('mensajeFranquicias', datos);
        console.log("finaliza creación en sessionStorage: " + datos);
    } else {
        _mensajesIos = datos;
        console.log("finaliza obtención y guardado en memoria: " + datos);
    }
}

function esValidoLocalStorage() {
    try {
        localStorage.setItem('localStorage', 1);
        localStorage.removeItem('localStorage');
    } catch (e) {
        console.log("Explorador no permite usar localStorage");
        return false;
    }

    return true;
}

// Validación número tarjeta
$.validator.addMethod("tarjeta", function (value, element, params) {
    var settngs = $.data($('form')[0], 'validator').settings;

    console.log(params.nombrePropiedad);

    if (esValidoLocalStorage()) {
        var jsonMensajes = sessionStorage.getItem("mensajeFranquicias");
    } else {
        var jsonMensajes = _mensajesIos;
    }

    var erroresFranquicias = JSON.parse(jsonMensajes);
    var franquiciaValue = $("select").find('[name="' + params.nombrepropiedad + '"]').val();

    if (franquiciaValue == "1") { // Visa
        var re = /^4\d{3}-?\d{4}-?\d{4}-?\d{4}$/;
        var msg = erroresFranquicias.visa;
    }

    if (franquiciaValue == "2") { // MasterCard
        var re = /^(2|5)[1-5]\d{2}-?\d{4}-?\d{4}-?\d{4}$/;
        var msg = erroresFranquicias.master;
    }

    if (franquiciaValue == "3") { // Dinners
        var re = /^3[0,6,8]\d{12}$/;
        var msg = erroresFranquicias.diners;
    }

    if (franquiciaValue == "4") { // AmericanExpress
        var re = /^3[4,7]\d{13}$/;
        var msg = erroresFranquicias.american;
    }

    if (franquiciaValue == "5") { // Discover
        var re = /^6\d{3}-?\d{4}-?\d{4}-?\d{4}$/;
        var msg = erroresFranquicias.discover;
    }

    settngs.messages["Pago.NumeroTarjeta"].tarjeta = msg;

    if (franquiciaValue == null || franquiciaValue == "") {
        return true;
    }

    if (value.length < 14 || value.length > 16) {
        return false;
    }

    value = value == null ? "" : value;

    if (!re.test(value)) {
        return false;
    } else {
        return validacionLongitudTarjetaCredito(value);
    }
}, '');


function validacionLongitudTarjetaCredito(value) {
    var checksum = 0;
    for (var i = (2 - (value.length % 2)) ; i <= value.length; i += 2) {
        checksum += parseInt(value.charAt(i - 1));
    }

    for (var i = (value.length % 2) + 1; i < value.length; i += 2) {
        var digit = parseInt(value.charAt(i - 1)) * 2;
        if (digit < 10) {
            checksum += digit;
        }
        else {
            checksum += (digit - 9);
        }
    }

    if ((checksum % 10) == 0) {
        return true;
    }
    else {
        return false;
    }

    return;
}

// Validación año y mes tarjeta
$.validator.addMethod("fechavencimiento", function (value, element, params) {
    console.log(params.nombrePropiedad);
    var mesValue = $("select").find('[name="' + params.campomes + '"]').val();

    if (mesValue == null || mesValue == "" || value == null || value == "") {
        return true;
    }

    var seleccionada = new Date(value, (mesValue - 1), 1, 0, 0, 0, 0);

    if (seleccionada > (new Date())) {
        return true;
    }

    return false;
}, '');