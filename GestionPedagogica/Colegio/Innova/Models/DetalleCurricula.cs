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
    
    public partial class DetalleCurricula
    {
        public int IdCurricula { get; set; }
        public int IdDetalleCurricula { get; set; }
        public int IdCompetencia { get; set; }
    
        public virtual Competencia Competencia { get; set; }
        public virtual Curricula Curricula { get; set; }
    }
}
