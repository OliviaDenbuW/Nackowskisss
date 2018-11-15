using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nackowskisss.DataLayer;
using Nackowskisss.Models;
using Nackowskisss.Models.API_Models;

namespace Nackowskisss.BusinessLayer
{
    public class AuctionBusinessService : IAuctionBusinessService
    {
        private IAuctionRepository _repository;

        public AuctionBusinessService(IAuctionRepository repository)
        {
            _repository = repository;
        }

        public List<AuctionModel> GetAllAuctions()
        {
            return _repository.GetAllAuctions();
        }

        public void CreateNewAuction(AuctionModel newAuction)
        {
            _repository.CreateNewAuction(newAuction);
        }

        public void DeleteAuction(int id)
        {
            _repository.DeleteAuction(id);
        }
    }
}
