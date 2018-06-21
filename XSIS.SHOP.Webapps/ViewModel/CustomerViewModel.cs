﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace XSIS.SHOP.Webapps.ViewModel
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [Required]


        [Display (Name = "Nama Depan")]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Display(Name = "Nama Belakang")]
        [StringLength(40)]
        public string LastName { get; set; }

        [Display(Name = "Kota")]
        [StringLength(40)]
        public string City { get; set; }

        [Display(Name = "Negara")]
        [StringLength(40)]
        public string Country { get; set; }

        [Display(Name = "No. HP")]
        [StringLength(20)]
        public string Phone { get; set; }

       
        [StringLength(35)]
        public string Email { get; set; }
       
    }
}