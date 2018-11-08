var localStorage = window.localStorage;

class Provincias {
    constructor(nombre, estado, action) {
        this.nombre = nombre;
        this.estado = estado;
        this.action = action;
    }

    agregarProvincia(id, funcion) {
        if (this.nombre == "") {
            document.getElementById("Nombre").focus();
        } else {
            if (this.estado == "0") {
                document.getElementById("mensaje").innerHTML = "Seleccione un estado";
            } else {
                var nombre = this.nombre;
                var estado = this.estado;
                var action = this.action;
                var mensaje = '';
                $.ajax({
                    type: "POST",
                    url: action,
                    data: {
                        id, nombre, estado, funcion
                    },
                    success: (response) => {
                        $.each(response, (index, val) => {
                            mensaje = val.code;
                        });
                        if (mensaje === "Save") {
                            this.restablecer();
                        } else {
                            document.getElementById("mensaje").innerHTML = "No se puede guardar la provincia";
                        }
                        //console.log(response);
                    }
                });
            }
        }
    }

    filtrarDatos(numPagina, order) {
        var valor = this.nombre;
        var action = this.action;
        if (valor == "") {
            valor = "null";
        }
        $.ajax({
            type: "POST",
            url: action,
            data: { valor, numPagina, order },
            success: (response) => {
                console.log(response);
                $.each(response, (index, val) => {

                    $("#resultSearch").html(val[0]);
                    $("#paginado").html(val[1]);
                });

            }
        });
    }

    qetProvincia(id, funcion) {
        var action = this.action;
        $.ajax({
            type: "POST",
            url: action,
            data: { id },
            success: (response) => {
                console.log(response);
                if (funcion == 0) {
                    if (response[0].estado) {
                        document.getElementById("titleProvincia").innerHTML = "¿Está seguro(a) de desactivar la categoría? " + response[0].nombre;
                    } else {
                        document.getElementById("titleProvincia").innerHTML = "¿Está seguro(a) de habilitar la categoría? " + response[0].nombre;
                    }
                } else {
                    document.getElementById("Nombre").value = response[0].nombre;
                    if (response[0].estado) {
                        document.getElementById("Estado").selectedIndex = 1;
                    } else {
                        document.getElementById("Estado").selectedIndex = 2;
                    }
                }
                localStorage.setItem("provincia", JSON.stringify(response));
            }
        });
    }

    editarProvincia(id, funcion) {
        var action = this.action;
        var response = JSON.parse(localStorage.getItem("provincia"));
        var nombre = response[0].nombre;
        var estado = response[0].estado;
        localStorage.removeItem("provincia");
        $.ajax({
            type: "POST",
            url: action,
            data: { id, nombre, estado, funcion },
            success: (response) => {
                console.log(response);
                this.restablecer();
            }
        });
    }

    restablecer() {
        document.getElementById("Nombre").value = "";
        document.getElementById("mensaje").innerHTML = "";
        document.getElementById("Estado").selectedIndex = 0;
        $('#modalAC').modal('hide');
        $('#ModalEstado').modal('hide');
        filtrarDatos(1, "nombre");
    }
}



