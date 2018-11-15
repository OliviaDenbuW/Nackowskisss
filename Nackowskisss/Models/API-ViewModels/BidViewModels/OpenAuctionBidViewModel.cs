using Nackowskisss.BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskisss.Models.API_ViewModels.BidViewModels
{
    public class OpenAuctionBidViewModel
    {
        public int Id { get; set; }

        [Display (Name = "Bid")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Bid must be over zero")]
        public decimal BidPrice { get; set; }

        public int AuctionId { get; set; }

        public string Bidder { get; set; }

        public bool BidIsValid { get; set; }
    }
}
