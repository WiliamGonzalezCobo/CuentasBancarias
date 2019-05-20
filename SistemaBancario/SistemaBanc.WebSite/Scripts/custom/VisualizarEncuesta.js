function readJson(json, vistaPrevia, controlador) {
    var encuesta = json;

    getContenedorEncuesta().append(getInfoEncuesta(encuesta.Nombre, encuesta.Descripcion));
    getContenedorEncuesta().append(adicionarFormulario(encuesta.IdEncuesta, controlador, 'post'));

    $.each(encuesta.Preguntas, function (key, val) {
        getFormulario(encuesta.IdEncuesta).append(adicionarPregunta(encuesta, val));
    });

    if (!vistaPrevia) {
        getFormulario(encuesta.IdEncuesta).append(adicionarBotones());
    }
}

function getContenedorEncuesta() {
    return $("#col-encuesta");
}

function getInfoEncuesta(titulo, descripcion) {
    $("#titulo-encuesta").append(getTituloEncuesta(titulo))

    str = '<div id="detalle-encuesta">';
    str += getDescripcionEncuesta(descripcion);
    str += '</div>'

    return str;
}

function getTituloEncuesta(titulo) {
    return titulo;
}

function getDescripcionEncuesta(descripcion) {
    return '<p class="lead">' + descripcion + '</p>';
}

function adicionarFormulario(id, controlador, metodo) {
    //return '<form id="' + id + '" action="' + controlador '" method="' + metodo + '"></form>';
    return '<div id="' + id + '"></div>';
}

function getFormulario(id) {
    return $("#" + id);
}

function adicionarPregunta(encuesta, pregunta) {
    str = '<div class="form-group">';
    str += getDescripcionPregunta(pregunta.Descripcion);
    str += getElementoPregunta(pregunta);
    str += '</div>';

    return str;
}

function getElementoPregunta(pregunta) {
    if (pregunta.MetadataPregunta.Nombre == 'TextBox') {
        return getInputText(pregunta.IdPregunta, pregunta.Requerido);
    } if (pregunta.MetadataPregunta.Nombre == 'TextDate') {
        return getInputDate(pregunta.IdPregunta, pregunta.Requerido);
    } if (pregunta.MetadataPregunta.Nombre == 'TextNumeric') {
        return getInputNumeric(pregunta.IdPregunta, pregunta.Requerido);
    } else if (pregunta.MetadataPregunta.Nombre == 'TextArea') {
        return getTextarea(pregunta.IdPregunta);
    } else if (pregunta.MetadataPregunta.Nombre == 'ComboBox') {
        return getSelect(pregunta.IdPregunta, pregunta.ItemsPreguntas);
    } else if (pregunta.MetadataPregunta.Nombre == 'RadioButton') {
        return getRadioButton(pregunta.IdPregunta, pregunta.ItemsPreguntas);
    } else if (pregunta.MetadataPregunta.Nombre == 'CheckBox') {
        return getCheckBox(pregunta.IdPregunta, pregunta.ItemsPreguntas);
    } else {
        console.log('La Metadata "' + pregunta.MetadataPregunta.Nombre + '" de la pregunta "' + pregunta.Descripcion + '" no es válido');

        return '';
    }
}

function getDescripcionPregunta(texto) {
    return '<p class="bs-callout bs-callout-info">' + texto + '</p>';
}

function getInputText(id, requerido) {
    if (requerido) {
        return '<input id="' + id + '" class="form-control" type="text" name="' + id + '" required>';
    }

    return '<input id="' + id + '" class="form-control" type="text" name="' + id + '">';
}

function getInputDate(id, requerido) {
    if (requerido) {
        return '<input id="' + id + '" class="form-control" type="date" name="' + id + '" required>';
    }

    return '<input id="' + id + '" class="form-control" type="date" name="' + id + '">';
}

function getInputNumeric(id, requerido) {
    if (requerido) {
        return '<input id="' + id + '" class="form-control" type="number" name="' + id + '" required>';
    }

    return '<input id="' + id + '" class="form-control" type="number" name="' + id + '">';
}

function getTextarea(id) {
    return '<textarea id="' + id + '" name="' + id + '" class="form-control" rows="3"> </textarea>';
}

function getSelect(id, opciones) {
    str = '<select id="' + id + '" class="form-control" >';
    str += '<option value="0">Seleccione</option>'

    $.each(opciones, function (key, val) {
        str += '<option value="' + val.IdItemPregunta + '">' + val.Valor + '</option>'
    });

    str += '<select>';

    return str;
}

function getRadioButton(id, opciones) {
    return construirElementosMultiples('radio', id, opciones);
}

function getCheckBox(id, opciones) {
    return construirElementosMultiples('checkbox', id, opciones);
}

function construirElementosMultiples(tipo, id, opciones) {
    str = "";

    $.each(opciones, function (key, val) {
        str += '<input type="' + tipo + '" name="' + id + '" value="' + val.IdItemPregunta + '">' + val.Valor + '<br/>';
    });

    return str;
}

function adicionarBotones() {
    str = '<div class="btn-control">'
    str += getSubmit();
    str += getCancel();
    str += '</div>';

    return str;
}

function getSubmit() {
    return '<button type="submit" name="Guardar" class="btn btn-primary separar">Enviar</button>';
}

function getCancel() {
    return '<button type="reset" class="btn btn-danger">Cancel</button>';
}

//Accion ) true agrega o false elimina
function agregarRespuesta(objjson, IdPregunta, Respuesta, Type, Accion) {
    var encuesta = objjson;

    Array
    $.each(encuesta.Preguntas, function (key, val) {
        if (val.IdPregunta == IdPregunta) {
            console.log(val.MetadataPregunta);

            if (val.Respuestas == null) {
                val.Respuestas = new Array();
            };

            if (Type == "checkbox") {
                if (Accion) {
                    val.Respuestas.push({ "IdRespuesta": null, "Valor": '' + Respuesta + '', "IdPregunta": '' + IdPregunta + '' });
                } else {
                    $.each(val.Respuestas, function (key, resp) {
                        if (resp.Valor == Respuesta) {
                            val.Respuestas.splice(key, 1);

                            return false;
                        }
                    });
                }
            }
            else {
                val.Respuestas = new Array();
                val.Respuestas.push({ "IdRespuesta": null, "Valor": '' + Respuesta + '', "IdPregunta": '' + IdPregunta + '' });
            }
        };
    });

    return encuesta;
}