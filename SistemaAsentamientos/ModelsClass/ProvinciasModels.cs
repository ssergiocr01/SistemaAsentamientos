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

        public List<IdentityError> guardarProvincia(string nombre,  string estado)
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

        public List<object[]> filtrarDatos(int numPagina, string valor)
        {
            int count = 0, cant, numRegistros = 0, inicio = 0, reg_por_pagina = 3;
            int can_paginas, pagina;
            string dataFilter = "", paginador = "", Estado = null;
            List<object[]> data = new List<object[]>();
            IEnumerable<Provincia> query;
            var provincias = context.Provincia.OrderBy(c => c.Nombre).ToList();
            numRegistros = provincias.Count;
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
                    Estado = "<a data-toggle='modal' data-target='#ModalEstado' onclick='editarEstado(" + item.ProvinciaID + ")' class='btn btn-success'>Activo</a>";
                }
                else
                {
                    Estado = "<a data-toggle='modal' data-target='#ModalEstado' onclick='editarEstado(" + item.ProvinciaID + ")' class='btn btn-danger'>No activo</a>";
                }
                dataFilter += "<tr>" +
                    "<td>" + item.Nombre + "</td>" +
                    "<td>" + Estado + "</td>" +
                    "<td>" +
                    "<a data-toggle='modal' data-target='#myModal' class='btn btn-warning'>Editar</a> &nbsp;" +
                    "<a data-toggle='modal' data-target='#myModal3' class='btn btn-danger'>Eliminar</a>" +
                    "</td>" +
                "</tr>";
            }
            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);
            return data;
        }

        public List<Provincia> getProvincias(int id)
        {
            return context.Provincia.Where(p => p.ProvinciaID == id).ToList();
        }

        public List<IdentityError> editarProvincia(int idProvincia, string nombre, Boolean estado, string funcion)
        {
            var errorList = new List<IdentityError>();
            string code = "", des = "";
            switch (funcion)
            {
                case "estado":
                    if (estado)
                    {
                        estados = false;
                    }
                    else
                    {
                        estados = true;
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

                    break;
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
