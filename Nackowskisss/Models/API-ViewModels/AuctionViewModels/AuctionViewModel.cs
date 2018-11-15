using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskisss.Models.API_ViewModels.AuctionViewModels
{
    public class AuctionViewModel
    {
        public IEnumerable<IndexAuctionViewModel> IndexAuctionViewModels { get; set; }

        public DetailsAuctionViewModel DetailsAuctionViewModel { get; set; }

        public UpdateAuctionViewModel UpdateAuctionViewModel { get; set; }

        public IEnumerable<SearchResultAuctionViewModel> SearchResultAuctionViewModel { get; set; }

        public string SearchInput { get; set; }
    }
}
