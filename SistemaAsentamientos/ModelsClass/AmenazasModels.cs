using Microsoft.AspNetCore.Identity;
using SistemaAsentamientos.Data;
using SistemaAsentamientos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAsentamientos.ModelsClass
{
    public class AmenazasModels
    {
        private ApplicationDbContext context;
        private Boolean estados;

        public AmenazasModels(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<IdentityError> guardarAmenaza(string descripcion, string estado)
        {
            var errorList = new List<IdentityError>();
            var amenaza = new AmenazaNatural
            {
                Descripcion = descripcion,
                Estado = Convert.ToBoolean(estado),
            };
            context.Add(amenaza);

            context.SaveChanges();
            errorList.Add(new IdentityError
            {
                Code = "Save",
                Description = "Save"
            });
            return errorList;
        }

        public List<object[]> filtrarAmenazas(int numPagina, string valor, string order)
        {
            int count = 0, cant, numRegistros = 0, inicio = 0, reg_por_pagina = 3;
            int can_paginas, pagina;
            string dataFilter = "", paginador = "", Estado = null;
            List<object[]> data = new List<object[]>();
            IEnumerable<AmenazaNatural> query;
            List<AmenazaNatural> amenazas = null;
            switch (order)
            {
                case "descripcion":
                    amenazas = context.AmenazaNatural.OrderBy(a => a.Descripcion).ToList();
                    break;
                case "estado":
                    amenazas = context.AmenazaNatural.OrderBy(a => a.Estado).ToList();
                    break;
                default:
                    break;
            }

            numRegistros = amenazas.Count;
            if ((numRegistros % reg_por_pagina) > 0)
            {
                numRegistros += 1;
            }
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);
            if (valor == "null")
            {
                query = amenazas.Skip(inicio).Take(reg_por_pagina);
            }
            else
            {
                query = amenazas.Where(a => a.Descripcion.StartsWith(valor)).Skip(inicio).Take(reg_por_pagina);
            }
            cant = query.Count();

            foreach (var item in query)
            {
                if (item.Estado == true)
                {
                    Estado = "<a data-toggle='modal' data-target='#ModalEstadoAmenaza' onclick='editarEstadoAmenaza(" + item.AmenazaNaturalID + ',' + 0 + ")' " +
                        "class='btn btn-success'>Activo</a>";
                }
                else
                {
                    Estado = "<a data-toggle='modal' data-target='#ModalEstadoAmenaza' onclick='editarEstadoAmenaza(" + item.AmenazaNaturalID + ',' + 0 + ")' " +
                        "class='btn btn-danger'>No activo</a>";
                }
                dataFilter += "<tr>" +
                      "<td>" + item.Descripcion + "</td>" +
                      "<td>" + Estado + " </td>" +
                      "<td>" +
                      "<a data-toggle='modal' data-target='#modalAN' onclick='editarEstadoAmenaza(" + item.AmenazaNaturalID + ',' + 1 + ")'" +
                      "class='btn btn-warning'>Editar</a> &nbsp;" +
                      "</td>" +
                  "</tr>";
            }
            if (valor == "null")
            {
                if (numPagina > 1)
                {
                    pagina = numPagina - 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarAmenazas(" + 1 + ',' + '"' + order + '"' + ")'> << </a>" +
                    "<a class='btn btn-default' onclick='filtrarAmenazas(" + pagina + ',' + '"' + order + '"' + ")'> < </a>";
                }
                if (1 < can_paginas)
                {
                    paginador += "<strong class='btn btn-success'>" + numPagina + ".de." + can_paginas + "</strong>";
                }
                if (numPagina < can_paginas)
                {
                    pagina = numPagina + 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarAmenazas(" + pagina + ',' + '"' + order + '"' + ")'>  > </a> " +
                                 "<a class='btn btn-default' onclick='filtrarAmenazas(" + can_paginas + ',' + '"' + order + '"' + ")'> >> </a>";
                }
            }
            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);
            return data;
        }

        public List<AmenazaNatural> getAmenazas(int id)
        {
            return context.AmenazaNatural.Where(a => a.AmenazaNaturalID == id).ToList();
        }

        public List<IdentityError> editarAmenaza(int idAmenaza, string descripcion, Boolean estado, int funcion)
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
            var amenaza = new AmenazaNatural()
            {
                AmenazaNaturalID = idAmenaza,
                Descripcion = descripcion,
                Estado = estados
            };
            try
            {
                context.Update(amenaza);
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
