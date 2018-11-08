var localStorage = window.localStorage;

class Provincias {
    constructor(nombre, estado, action) {
        this.nombre = nombre;
        this.estado = estado;
        this.action = action;
    }

    agregarProvincia() {
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
                        nombre, estado
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

    filtrarDatos(numPagina) {
        var valor = this.nombre;
        var action = this.action;
        if (valor == "") {
            valor = "null";
        }
        $.ajax({
            type: "POST",
            url: action,
            data: { valor, numPagina },
            success: (response) => {
                console.log(response);
                $.each(response, (index, val) => {

                    $("#resultSearch").html(val[0]);
                    $("#paginado").html(val[1]);
                });

            }
        });
    }

    qetProvincia(id) {
        var action = this.action;
        $.ajax({
            type: "POST",
            url: action,
            data: { id },
            success: (response) => {
                console.log(response);
                if (response[0].estado) {
                    document.getElementById("titleProvincia").innerHTML = "¿Está seguro(a) de desactivar la provincia? " + response[0].nombre;
                } else {
                    document.getElementById("titleProvincia").innerHTML = "¿Está seguro(a) de habilitar la provincia? " + response[0].nombre;
                }
                localStorage.setItem("provincia", JSON.stringify(response));
            }
        });
    }

    editarProvincia(id, funcion) {
        var nombre = null;
        var estado = null;
        var action = null;

        switch (funcion) {
            case "estado":
                var response = JSON.parse(localStorage.getItem("provincia"));
                nombre = response[0].nombre;
                estado = response[0].estado;
                localStorage.removeItem("provincia");
                this.editar(id, nombre, estado, funcion);
                break;
            default:
        }
    }

    editar(id, nombre, estado, funcion) {
        var action = this.action;
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
        filtrarDatos(1);
    }
}



