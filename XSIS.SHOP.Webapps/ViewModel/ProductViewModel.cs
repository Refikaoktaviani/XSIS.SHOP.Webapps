using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace XSIS.SHOP.Webapps.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]

        [Display(Name ="Nama Produk")]
        [StringLength(50)]
        public string ProductName { get; set; }

        public string CompanyName { get; set; }

        public int SupplierId { get; set; }

        public Nullable<decimal> UnitPrice { get; set; }

        [StringLength(30)]
        public string Package { get; set; }

        public bool IsDiscontinued { get; set; }

    }
}