using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskisss.Models.API_ViewModels.BidViewModels
{
    public class ClosedAuctionBidViewModel
    {
        public int Id { get; set; }

        //public int AuctionId { get; set; }

        public string Bidder { get; set; }
    }
}
