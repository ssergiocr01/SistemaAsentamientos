using Microsoft.AspNetCore.Identity;
using SistemaAsentamientos.Data;
using SistemaAsentamientos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAsentamientos.ModelsClass
{
    public class ProvinciasModels
    {
        private ApplicationDbContext context;
        private Boolean estados;

        public ProvinciasModels(ApplicationDbContext context)
        {
            this.context = context;
            //filtrarDatos(1, "Alajuela");
        }

        public List<IdentityError> guardarProvincia(string nombre, string estado)
        {
            var errorList = new List<IdentityError>();
            var provincia = new Provincia
            {
                Nombre = nombre,
                Estado = Convert.ToBoolean(estado),
            };
            context.Add(provincia);

            context.SaveChanges();
            errorList.Add(new IdentityError
            {
                Code = "Save",
                Description = "Save"
            });
            return errorList;
        }

        public List<object[]> filtrarDatos(int numPagina, string valor, string order)
        {
            int count = 0, cant, numRegistros = 0, inicio = 0, reg_por_pagina = 7;
            int can_paginas, pagina;
            string dataFilter = "", paginador = "", Estado = null;
            List<object[]> data = new List<object[]>();
            IEnumerable<Provincia> query;
            List<Provincia> provincias = null;
            switch (order)
            {
                case "nombre":
                    provincias = context.Provincia.OrderBy(c => c.Nombre).ToList();
                    break;
                case "estado":
                    provincias = context.Provincia.OrderBy(c => c.Estado).ToList();
                    break;
                default:
                    break;
            }

            numRegistros = provincias.Count;
            if ((numRegistros % reg_por_pagina) > 0)
            {
                numRegistros += 1;
            }
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);
            if (valor == "null")
            {
                query = provincias.Skip(inicio).Take(reg_por_pagina);
            }
            else
            {
                query = provincias.Where(c => c.Nombre.StartsWith(valor)).Skip(inicio).Take(reg_por_pagina);
            }
            cant = query.Count();

            foreach (var item in query)
            {
                if (item.Estado == true)
                {
                    Estado = "<a data-toggle='modal' data-target='#ModalEstado' onclick='editarEstado(" + item.ProvinciaID + ',' + 0 + ")' " +
                        "class='btn btn-success'>Activo</a>";
                }
                else
                {
                    Estado = "<a data-toggle='modal' data-target='#ModalEstado' onclick='editarEstado(" + item.ProvinciaID + ',' + 0 + ")' " +
                        "class='btn btn-danger'>No activo</a>";
                }
                dataFilter += "<tr>" +
                      "<td>" + item.Nombre + "</td>" +
                      "<td>" + Estado + " </td>" +
                      "<td>" +
                      "<a data-toggle='modal' data-target='#modalAC' onclick='editarEstado(" + item.ProvinciaID + ',' + 1 + ")'" +
                      "class='btn btn-warning'>Editar</a> &nbsp;" +
                      "</td>" +
                  "</tr>";
            }
            if (valor == "null")
            {
                if (numPagina > 1)
                {
                    pagina = numPagina - 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarDatos(" + 1 + ',' + '"' + order + '"' + ")'> << </a>" +
                    "<a class='btn btn-default' onclick='filtrarDatos(" + pagina + ',' + '"' + order + '"' + ")'> < </a>";
                }
                if (1 < can_paginas)
                {
                    paginador += "<strong class='btn btn-success'>" + numPagina + ".de." + can_paginas + "</strong>";
                }
                if (numPagina < can_paginas)
                {
                    pagina = numPagina + 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarDatos(" + pagina + ',' + '"' + order + '"' + ")'>  > </a> " +
                                 "<a class='btn btn-default' onclick='filtrarDatos(" + can_paginas + ',' + '"' + order + '"' + ")'> >> </a>";
                }
            }
            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);
            return data;
        }

        public List<Provincia> getProvincias(int id)
        {
            return context.Provincia.Where(p => p.ProvinciaID == id).ToList();
        }

        public List<IdentityError> editarProvincia(int idProvincia, string nombre, Boolean estado, int funcion)
        {
            var errorList = new List<IdentityError>();
            string code = "", des = "";
            switch (funcion)
            {
                case 0:
                    if (estado)
                    {
                        estados = false;
                    }
                    else
                    {
                        estados = true;
                    }
                    break;
                case 1:
                    estados = estado;
                    break;
            }
            var provincia = new Provincia()
            {
                ProvinciaID = idProvincia,
                Nombre = nombre,
                Estado = estados
            };
            try
            {
                context.Update(provincia);
                context.SaveChanges();
                code = "Save";
                des = "Save";
            }
            catch (Exception ex)
            {
                code = "error";
                des = ex.Message;
            }
            errorList.Add(new IdentityError
            {
                Code = code,
                Description = des
            });

            return errorList;
        }
    }
}
