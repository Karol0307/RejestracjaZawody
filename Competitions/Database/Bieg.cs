//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Competitions.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bieg
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bieg()
        {
            this.Zawody = new HashSet<Zawody>();
        }
    
        public int ID { get; set; }
        public string Nazwa { get; set; }
        public System.DateTime Data { get; set; }
        public double Dystans { get; set; }
        public string Miejsce { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zawody> Zawody { get; set; }
    }
}
