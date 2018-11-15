using Nackowskisss.Models;
using Nackowskisss.Models.API_Models;
using Nackowskisss.Models.API_ViewModels.AuctionViewModels;
using Nackowskisss.Models.API_ViewModels.BidViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nackowskisss.BusinessLayer
{
    public interface IBusinessService
    {
        HttpResponseMessage DeleteAuction(int auctionId);

        UpdateAuctionViewModel SetUpdateViewModel(DetailsAuctionViewModel currentAuction);

        DetailsAuctionViewModel UpdateAuction(UpdateAuctionViewModel newValues, DetailsAuctionViewModel oldValues);

        AuctionModel MakeAuctionApiReady(DetailsAuctionViewModel viewModel);

        HttpResponseMessage UpdateAuction(AuctionModel model);

        CreateAuctionViewModel SetCreateAuctionViewModel(CreateAuctionViewModel newAuction);

        AuctionModel MakeAuctionApiReady(CreateAuctionViewModel viewModel);

        HttpResponseMessage CreateNewAuction(AuctionModel newAuction);

        IEnumerable<IndexAuctionViewModel> GetOpenAuctions();

        //TODO Ta bort om man märker att Interface inte behövde den
        //IEnumerable<IndexAuctionViewModel> SetIndexAuctionViewModel(IEnumerable<AuctionModel> auctions);

        IEnumerable<SearchResultAuctionViewModel> GetSearchResult(string searchInput);

        //TODO Ta bort om man märker att Interface inte behövde den
        //IEnumerable<SearchResultAuctionViewModel> SetSearchResultViewModel(IEnumerable<AuctionModel> auctions);

        //TODO Fixa sortering
        //IEnumerable<SearchResultAuctionViewModel> GetAuctionsSorted(string sortParam);

        DetailsAuctionViewModel GetAuctionById(int auctionId);

        //TODO Ta bort om man märker att Interface inte behövde den
        //bool GetAuctionIsOpen(int auctionId);

        //TODO Ta bort om man märker att Interface inte behövde den
        //DetailsAuctionViewModel SetDetailsAuctionViewModel(AuctionModel model);

        #region BID
        //TODO Ta bort om man märker att Interface inte behövde den
        //List<OpenAuctionBidViewModel> GetBidsForAuction(int auctionId);

        bool GetBidIsValid(decimal newBid, int auctionId);

        bool GetAuctionHasBids(int auctionId);

        //TODO Ta bort om man märker att Interface inte behövde den
        //decimal GetHighestBidForAuction(int auctionId);

        //TODO Ta bort om man märker att Interface inte behövde den
        //List<decimal> GetBidValues(int auctionId);

        //TODO Ta bort om man märker att Interface inte behövde den
        //List<OpenAuctionBidViewModel> GetBidViewModels(int auctionId);

        //TODO Ta bort om man märker att Interface inte behövde den
        //List<OpenAuctionBidViewModel> SetBidViewModel(IEnumerable<BidModel> model);

        BidModel MakeBidApiReady(OpenAuctionBidViewModel newBid, string bidder);

        HttpResponseMessage MakeBid(BidModel bid);
        #endregion
    }
}
