//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Innova.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Curricula
    {
        public Curricula()
        {
            this.DetalleCurricula = new HashSet<DetalleCurricula>();
        }
    
        public int IdCurricula { get; set; }
        public Nullable<int> IdNivel { get; set; }
        public Nullable<int> IdCurriculaBase { get; set; }
        public Nullable<int> Estado { get; set; }
    
        public virtual CurriculaBase CurriculaBase { get; set; }
        public virtual Nivel Nivel { get; set; }
        public virtual ICollection<DetalleCurricula> DetalleCurricula { get; set; }
    }
}