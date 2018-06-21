using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace XSIS.Shop.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]


        [Display(Name = "Tanggal order")]
        [StringLength(40)]
        public string OrderDate { get; set; }

        [Display(Name = "No. Order")]
        [StringLength(10)]
        public string OrderNumber { get; set; }

      

        [Display(Name = "Nama Pembeli")]
        [StringLength(40)]
        public string CustomerName { get; set; }

        [Display(Name = "Id pembeli")]
        [StringLength(40)]
        public int CustomerId { get; set; }

        [Display(Name = "Total Harga")]
        [StringLength(40)]
        public Nullable<decimal> TotalAmount { get; set; }

       
        public List<OrderItemViewModel> ListOrderItem { get; set; }


    }

    public class OrderItemViewModel
    {
        public int Id { get; set; }
        [Required]

        [Display(Name = "Order Id")]
        [StringLength(40)]
        public int OrderId { get; set; }

        [Display(Name = "No. Order")]
        [StringLength(10)]
        public string OrderNumber { get; set; }


        [Display(Name = "Product id")]
        [StringLength(40)]
        public int ProductId { get; set; }

        [Display(Name = "Nama Produk")]
        [StringLength(40)]
        public string ProductName { get; set; }

        [Display(Name = "Harga")]
        [StringLength(40)]
        public decimal UnitPrice { get; set; }

        [Display(Name = "jumlah")]
        [StringLength(40)]
        public int Quantity { get; set; }

        [Display(Name = "Total Harga")]
        [StringLength(40)]

        public Nullable<decimal> TotalAmount { get; set; }


    }
}
