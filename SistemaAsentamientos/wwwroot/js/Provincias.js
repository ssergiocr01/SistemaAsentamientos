
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
                document.getElementById("mensaje").innerHTML = "seleccione un estado";
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

                    }
                });
            }
        }
    }
}
