using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult Index()
        {
            List<ListaPersonaViewModels> lst;
            using (IntentoEntities db =new IntentoEntities())
            {
                 lst = (from d in db.Persona
                           select new ListaPersonaViewModels
                           {
                               Id = d.Id,
                               Nombre = d.Nombre,
                               Apellido = d.Apellido
                           }).ToList();
            }


            return View(lst);
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(PersonaViewModels persona)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    using (IntentoEntities db = new IntentoEntities())
                    {
                        var tabla = new Persona();
                        tabla.Nombre = persona.Nombre;
                        tabla.Apellido = persona.Apellido;
                        db.Persona.Add(tabla);
                        db.SaveChanges();
                    }
                    return Redirect("~/Persona/");
                }
                return View(persona);
               

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpGet]
        public ActionResult Editar(int Id)
        {
            PersonaViewModels model = new PersonaViewModels();
            using(IntentoEntities db = new IntentoEntities())
            {
                var Persona = db.Persona.Find(Id);
                model.Id = Persona.Id;
                model.Nombre = Persona.Nombre;
                model.Apellido = Persona.Apellido;
            }
            return View(model);
        }



        [HttpPost]
        public ActionResult Editar(PersonaViewModels persona)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (IntentoEntities db = new IntentoEntities())
                    {
                        
                        var tabla = db.Persona.Find(persona.Id);
                        tabla.Nombre = persona.Nombre;
                        tabla.Apellido = persona.Apellido;
                        db.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Persona/");
                }
                return View(persona);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        [HttpGet]
        public ActionResult Eliminar(int Id)
        {
             
            using (IntentoEntities db = new IntentoEntities())
            {
                var Persona = db.Persona.Find(Id);
                db.Persona.Remove(Persona);
                db.SaveChanges();
            }
            return Redirect("~/Persona/");
        }
    }
}