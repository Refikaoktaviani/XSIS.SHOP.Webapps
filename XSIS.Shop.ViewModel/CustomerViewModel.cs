using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSIS.Shop.ViewModel
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [Required]


        [Display(Name = "Nama Depan")]
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
        [RegularExpression("^[-()0-9/s]*$", ErrorMessage = "Please enter valid phone no.")]
        public string Phone { get; set; }


        [StringLength(35)]
        public string Email { get; set; }
    }
}
