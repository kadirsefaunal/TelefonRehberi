//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TelefonRehberi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Calisanlar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Calisanlar()
        {
            this.Calisanlar1 = new HashSet<Calisanlar>();
        }
    
        public int ID { get; set; }
        public string CalisanAd { get; set; }
        public string CalisanSoyad { get; set; }
        public string Telefon { get; set; }
        public Nullable<int> DepartmanID { get; set; }
        public Nullable<int> YoneticiID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calisanlar> Calisanlar1 { get; set; }
        public virtual Calisanlar Calisanlar2 { get; set; }
        public virtual Departmanlar Departmanlar { get; set; }
    }
}