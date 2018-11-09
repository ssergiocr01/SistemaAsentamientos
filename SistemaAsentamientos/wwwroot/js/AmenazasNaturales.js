var localStorage = window.localStorage;

class AmenazasNaturales {
    constructor(descripcion, estado, action) {
        this.descripcion = descripcion;
        this.estado = estado;
        this.action = action;
    }

    agregarAmenaza(id, funcion) {
        if (this.descripcion == "") {
            document.getElementById("Descripcion").focus();
        } else {
            if (this.estado == "0") {
                document.getElementById("mensaje").innerHTML = "Seleccione un estado";
            } else {
                var descripcion = this.descripcion;
                var estado = this.estado;
                var action = this.action;
                var mensaje = '';
                $.ajax({
                    type: "POST",
                    url: action,
                    data: {
                        id, descripcion, estado, funcion
                    },
                    success: (response) => {
                        $.each(response, (index, val) => {
                            mensaje = val.code;
                        });
                        if (mensaje === "Save") {
                            this.restablecer();
                        } else {
                            document.getElementById("mensaje").innerHTML = "No se puede guardar la amenaza natural";
                        }
                        //console.log(response);
                    }
                });
            }
        }
    }    

    filtrarAmenazas(numPagina, order) {
        var valor = this.descripcion;
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

    qetAmenaza(id, funcion) {
        var action = this.action;
        $.ajax({
            type: "POST",
            url: action,
            data: { id },
            success: (response) => {
                console.log(response);
                if (funcion == 0) {
                    if (response[0].estado) {
                        document.getElementById("titleAmenaza").innerHTML = "¿Está seguro(a) de desactivar la amenaza? " + response[0].descripcion;
                    } else {
                        document.getElementById("titleAmenaza").innerHTML = "¿Está seguro(a) de habilitar la amenaza? " + response[0].descripcion;
                    }
                } else {
                    document.getElementById("Descripcion").value = response[0].descripcion;
                    if (response[0].estado) {
                        document.getElementById("Estado").selectedIndex = 1;
                    } else {
                        document.getElementById("Estado").selectedIndex = 2;
                    }
                }
                localStorage.setItem("amenaza", JSON.stringify(response));
            }
        });
    }

    editarAmenaza(id, funcion) {
        var action = this.action;
        var response = JSON.parse(localStorage.getItem("amenaza"));
        var descripcion = response[0].descripcion;
        var estado = response[0].estado;
        localStorage.removeItem("amenaza");
        $.ajax({
            type: "POST",
            url: action,
            data: { id, descripcion, estado, funcion },
            success: (response) => {
                console.log(response);
                this.restablecer();
            }
        });
    }

    restablecer() {
        document.getElementById("Descripcion").value = "";
        document.getElementById("mensaje").innerHTML = "";
        document.getElementById("Estado").selectedIndex = 0;
        $('#modalAN').modal('hide');
        $('#ModalEstadoAmenaza').modal('hide');
        this.filtrarAmenazas(1, "descripcion");
    }
}
