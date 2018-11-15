using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskisss.Models.API_ViewModels.AuctionViewModels
{
    public class UpdateAuctionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is mandatory")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is mandatory")]
        public string Description { get; set; }

        [Display(Name = "Start price")]
        [Required(ErrorMessage = "Start price is mandatory")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Start price must be over zero")]
        public decimal StartPrice { get; set; }
    }
}
