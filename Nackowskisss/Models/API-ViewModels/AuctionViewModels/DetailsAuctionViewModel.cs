using Nackowskisss.Models.API_ViewModels.BidViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskisss.Models.API_ViewModels.AuctionViewModels
{
    public class DetailsAuctionViewModel
    {
        public DetailsAuctionViewModel()
        {
            AllBids = new List<OpenAuctionBidViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Start date")]
        public string StartDateString { get; set; }

        [Display(Name = "End date")]
        public string EndDateString { get; set; }

        [Display(Name = "Start price")]
        public decimal StartPrice { get; set; }

        public string CreatedBy { get; set; }

        public string GroupCode { get; set; }

        public bool AuctionIsOpen { get; set; }

        public List<OpenAuctionBidViewModel> AllBids { get; set; }

        public OpenAuctionBidViewModel NewBid { get; set; }

        public decimal HighestBid { get; set; }
    }
}
