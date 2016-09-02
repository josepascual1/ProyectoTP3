using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using Innova.Models;


namespace Innova.Controllers
{
    public class CurriculaController : Controller
    {
        //
        // GET: /Curricula/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Base()
        {
            return PartialView();
        }

        public ActionResult ListaBase(FormCollection form)
        {
            //string inicio = "", string fin = "", string resolucion = ""
            var inicio = form["txtDesde"];
            var fin = form["txtHasta"];
            var resolucion = form["txtResolucion"];

            if (inicio == null && fin == null && resolucion == null)
            {
                ViewBag.Lista = GestionPedagogica.ListarBase();
                return PartialView();
            }
            else if (inicio == "" && fin == "" && resolucion == "")
            {
                ViewBag.Lista = GestionPedagogica.ListarBase();
                return PartialView();
            }
            else
            {
                ViewBag.Lista = GestionPedagogica.BuscarListaBase(inicio, fin, resolucion);
                return PartialView();
            }
        }

        public ActionResult BuscarBase()
        {
            return PartialView();
        }
        public ActionResult FormBase(int idCurriculaBase = 0)
        {

            ViewBag.Base = GestionPedagogica.ObtenerBase(idCurriculaBase);
            ViewBag.IdCurriculaBase = idCurriculaBase;

            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegistrarBase(FormCollection form)
        {
            var idCurriculaBase = Convert.ToInt32(form["hidIdCurriculaBase"]);
            var anno = Convert.ToInt32(form["txtAnno"]);
            var resolucion = form["txtResolucion"];
            var descripcion = form["txtDescripcion"];
            var vigencia = false;
            if (form["chkVigencia"] != null)
            {
                vigencia = form["chkVigencia"].IndexOf("true", System.StringComparison.Ordinal) != -1;
            }

            var setBase = new CurriculaBase()
            {
                IdCurriculaBase = idCurriculaBase,
                Año = anno,
                NumeroResolucion = resolucion,
                Descripcion = descripcion,
                Vigencia = vigencia
            };

            var exito = GestionPedagogica.RegistrarCurriculaBase(setBase);

            var js = new JavaScriptSerializer();
            return Content(js.Serialize(exito));
        }

        public ActionResult FormCompetenciaCapacidadArea(int idCurriculaBase = 0)
        {

            ViewBag.listaArea = new SelectList(GestionPedagogica.ListarAreaCurricular(), "IdAreaCurricular", "Nombre");
            ViewBag.listaCompetencia = new SelectList(GestionPedagogica.ListarCompetencia(), "IdCompetencia", "Nombre");
            ViewBag.IdCurriculaBase = idCurriculaBase;
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegistrarCompetenciaxArea(FormCollection form)
        {
            var idCurriculaBase = Convert.ToInt32(form["hidIdCurriculaBase"]);
            var area = Convert.ToInt32(form["cboArea"]);
            var competencia = Convert.ToInt32(form["cboCompetencia"]);

            var setBaseCompetencia = new CurriculaBaseCompetencia()
            {
                IdCurriculaBase = idCurriculaBase,
                IdAreaCurricular = area,
                IdCompetencia = competencia
            };

            var exito = GestionPedagogica.RegistrarCompetenciaArea(setBaseCompetencia);

            var js = new JavaScriptSerializer();
            return Content(js.Serialize(exito));
        }


        public ActionResult ElininarCompetenciaxArea(int idCurriculaBaseCompetencia)
        {

            var exito = GestionPedagogica.EliminarCompetenciaCapadidadArea(idCurriculaBaseCompetencia);
            var js = new JavaScriptSerializer();
            return Content(js.Serialize(exito));
        }


        public ActionResult ListaAreaCompetencia(int idCurriculaBase = 0)
        {

            ViewBag.Lista = GestionPedagogica.ListarCompetenciaCapadidadArea(idCurriculaBase);

            return PartialView();
        }
    }
}
