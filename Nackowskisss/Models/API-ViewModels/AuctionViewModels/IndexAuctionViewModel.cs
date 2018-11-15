using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskisss.Models.API_ViewModels.AuctionViewModels
{
    public class IndexAuctionViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal StartPrice { get; set; }

        [Display (Name = "End date")]
        public string EndDate { get; set; }

        public bool AuctionIsOpen { get; set; }
    }
}
