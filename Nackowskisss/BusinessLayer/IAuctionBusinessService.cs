using Nackowskisss.Models;
using Nackowskisss.Models.API_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskisss.BusinessLayer
{
    public interface IAuctionBusinessService
    {
        List<AuctionModel> GetAllAuctions();

        void CreateNewAuction(AuctionModel newAuction);

        void DeleteAuction(int id);
    }
}
