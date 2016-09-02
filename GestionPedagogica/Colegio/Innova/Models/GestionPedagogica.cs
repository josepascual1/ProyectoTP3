using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innova.Models
{
    public class GestionPedagogica
    {

        public static List<AreaCurricular> ListarAreaCurricular()
        {

            using (var cn = new InnovaEntities())
            {
                var lista = (from a in cn.AreaCurricular
                             select a).ToList();

                return lista;
            }

        }

        public static List<Competencia> ListarCompetencia()
        {

            using (var cn = new InnovaEntities())
            {
                var lista = (from c in cn.Competencia
                             select c).ToList();

                return lista;
            }
        }

        public static bool RegistrarCurriculaBase(CurriculaBase Base)
        {
            using (var cn = new InnovaEntities())
            {
                try
                {
                    if (Base.IdCurriculaBase > 0)
                    {
                        var baseEdit = cn.CurriculaBase.FirstOrDefault(b => b.IdCurriculaBase == Base.IdCurriculaBase);
                        if (baseEdit != null)
                        {
                            baseEdit.Año = Base.Año;
                            baseEdit.NumeroResolucion = Base.NumeroResolucion;
                            baseEdit.Descripcion = Base.Descripcion;
                            baseEdit.Vigencia = Base.Vigencia;
                            cn.SaveChanges();
                        }
                    }
                    else
                    {
                        cn.CurriculaBase.Add(Base);
                        cn.SaveChanges();
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }


        public static List<CurriculaBase> ListarBase()
        {

            using (var cn = new InnovaEntities())
            {
                var lista = (from c in cn.CurriculaBase
                             select c).ToList();
                return lista;
            }
        }

        public static List<CurriculaBase> BuscarListaBase(string inicio, string fin, string resolucion) {
            using (var cn = new InnovaEntities())
            {
                List<CurriculaBase> lista = null;
               
                if (inicio != "" && fin == "" && resolucion == "")
                {
                    int iInicio = Convert.ToInt32(inicio);
                     lista = (from c in cn.CurriculaBase
                              where c.Año >= iInicio
                                 select c).ToList();
                   
                }

                if (inicio == "" && fin != "" && resolucion == "")
                {
                    int iFin = Convert.ToInt32(fin);
                    lista = (from c in cn.CurriculaBase
                             where c.Año <= iFin
                             select c).ToList();

                }

                if (inicio == "" && fin == "" && resolucion != "")
                {
                    lista = (from c in cn.CurriculaBase
                             where c.NumeroResolucion.Contains(resolucion)
                             select c).ToList();

                }

                if (inicio != "" && fin != "" && resolucion == "")
                {

                    int iInicio = Convert.ToInt32(inicio);
                    int iFin = Convert.ToInt32(fin);
                    lista = (from c in cn.CurriculaBase
                             where c.Año >= iInicio && c.Año <= iFin
                             select c).ToList();

                }

                return lista;
                               
            }
        }

        public static CurriculaBase ObtenerBase(int idCurriculaBase)
        {
            using (var cn = new InnovaEntities())
            {
                return cn.CurriculaBase.FirstOrDefault(b => b.IdCurriculaBase == idCurriculaBase);
            }
        }

        public static bool RegistrarCompetenciaArea(CurriculaBaseCompetencia BaseCompetencia)
        {
            using (var cn = new InnovaEntities())
            {
                try
                {
                    cn.CurriculaBaseCompetencia.Add(BaseCompetencia);
                    cn.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }


        public static List<ListaCompetenciaCapadidadArea> ListarCompetenciaCapadidadArea(int idCurriculaBase){

            using (var cn = new InnovaEntities())
            {

                var listaRetorno = new List<ListaCompetenciaCapadidadArea>();

                listaRetorno = (from c in cn.CurriculaBaseCompetencia
                             join a in cn.AreaCurricular on c.IdAreaCurricular equals a.IdAreaCurricular
                             join comp in cn.Competencia on c.IdCompetencia equals comp.IdCompetencia
                             join cap in cn.Capacidad on comp.IdCompetencia equals cap.IdCompetencia into x
                             from lj in x.DefaultIfEmpty()
                             where c.IdCurriculaBase == idCurriculaBase
                             select new ListaCompetenciaCapadidadArea
                             {
                                idCurriculaBaseCompetencia = c.IdCurriculaBaseCompetencia,
                                idCurriculaBase = idCurriculaBase,
                                areaCurricular = a.Nombre,
                                competencia = comp.Nombre,
                                capacidad = lj.Nombre
                             }).ToList();
                return listaRetorno;
                    
            }
        }

        public static bool EliminarCompetenciaCapadidadArea(int idCurriculaBaseCompetencia) {

            using (var cn = new InnovaEntities())
            {
                try
                {
                    var baseCompetencia = cn.CurriculaBaseCompetencia.FirstOrDefault(c => c.IdCurriculaBaseCompetencia == idCurriculaBaseCompetencia);
                    if (baseCompetencia!=null)
                    {
                        cn.CurriculaBaseCompetencia.Remove(baseCompetencia);
                        cn.SaveChanges();
                    }
                    
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        
        }

        public class ListaCompetenciaCapadidadArea
        {
            public int idCurriculaBaseCompetencia { get; set; }
            public int idCurriculaBase { get; set; }
            public string areaCurricular { get; set; }
            public string competencia { get; set; }
            public string capacidad { get; set; }


           
        }


    }
}