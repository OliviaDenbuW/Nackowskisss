using Nackowskisss.Models;
using Nackowskisss.Models.API_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nackowskisss.DataLayer
{
    public interface IAuctionRepository
    {
        AuctionModel GetAuctionById(int auctionId);

        IEnumerable<AuctionModel> GetAllAuctionss();

        //TODO Ta bort
        List<AuctionModel> GetAllAuctions();

        HttpResponseMessage CreateNewAuction(AuctionModel newAuction);

        HttpResponseMessage DeleteAuction(int auctionId);

        HttpResponseMessage UpdateAuction(AuctionModel currentAuction);

        IEnumerable<BidModel> GetBidsForAuction(int auctionId);

        HttpResponseMessage MakeBid(BidModel bid);
    }
}
