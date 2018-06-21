using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace XSIS.SHOP.Webapps.ViewModel
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        [Required]

        [Display(Name = "Nama Perusahaan")]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [Display(Name = "Nama ")]
        [StringLength(50)]
        public string ContactName { get; set; }


        [StringLength(40)]
        public string ContactTitle { get; set; }

        [Display(Name = "Kota")]
        [StringLength(40)]
        public string City { get; set; }

        [Display(Name = "Negara")]
        [StringLength(40)]
        public string Country { get; set; }

        [Display(Name = "No HP")]
        [StringLength(30)]
        public string Phone { get; set; }

        [StringLength(30)]
        public string Fax { get; set; }
    }
}