using Nackowskisss.DataLayer;
using Nackowskisss.Models;
using Nackowskisss.Models.API_Models;
using Nackowskisss.Models.API_ViewModels.AuctionViewModels;
using Nackowskisss.Models.API_ViewModels.BidViewModels;
using Nackowskisss.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nackowskisss.BusinessLayer
{
    public class BusinessService : IBusinessService
    {
        private IUserService _userService;
        private IAuctionRepository _auctionRepository;
        private string _apiKey;

        public BusinessService(IUserService userService,
                               IAuctionRepository auctionRepository)
        {
            _userService = userService;
            _auctionRepository = auctionRepository;
            _apiKey = "1080";
        }

        public HttpResponseMessage DeleteAuction(int auctionId)
        {
            return _auctionRepository.DeleteAuction(auctionId);
        }

        public UpdateAuctionViewModel SetUpdateViewModel(DetailsAuctionViewModel currentAuction)
        {
            UpdateAuctionViewModel viewModel = new UpdateAuctionViewModel
            {
                Id = currentAuction.Id,
                Title = currentAuction.Title,
                Description = currentAuction.Description,
                StartPrice = currentAuction.StartPrice
            };

            return viewModel;
        }

        public DetailsAuctionViewModel UpdateAuction(UpdateAuctionViewModel newValues, DetailsAuctionViewModel oldValues)
        {
            DetailsAuctionViewModel viewModel = new DetailsAuctionViewModel
            {
                Id = oldValues.Id,
                Title = newValues.Title,
                Description = newValues.Description,
                StartDateString = oldValues.StartDateString,
                EndDateString = oldValues.EndDateString,
                GroupCode = _apiKey,
                StartPrice = newValues.StartPrice,
                CreatedBy = _userService.GetCurrentUserName()
            };

            return viewModel;
        }

        public AuctionModel MakeAuctionApiReady(DetailsAuctionViewModel viewModel)
        {
            AuctionModel model = new AuctionModel
            {
                AuktionID = viewModel.Id.ToString(),
                Titel = viewModel.Title,
                Beskrivning = viewModel.Description,
                StartDatum = viewModel.StartDateString,
                SlutDatum = viewModel.EndDateString,
                Gruppkod = _apiKey,
                Utropspris = viewModel.StartPrice.ToString(),
                SkapadAv = _userService.GetCurrentUserName()
            };

            return model;
        }

        public HttpResponseMessage UpdateAuction(AuctionModel model)
        {
            return _auctionRepository.UpdateAuction(model);
        }

        public CreateAuctionViewModel SetCreateAuctionViewModel(CreateAuctionViewModel newAuction)
        {
            CreateAuctionViewModel viewModel = new CreateAuctionViewModel
            {
                Title = newAuction.Title,
                Description = newAuction.Description,
                StartDateString = newAuction.StartDateString,
                EndDateString = newAuction.EndDateString,
                GroupCode = _apiKey,
                StartPrice = newAuction.StartPrice,
                CreatedBy = _userService.GetCurrentUserName()
            };

            return viewModel;
        }

        public AuctionModel MakeAuctionApiReady(CreateAuctionViewModel viewModel)
        {
            AuctionModel model = new AuctionModel
            {
                AuktionID = viewModel.Id.ToString(),
                Titel = viewModel.Title,
                Beskrivning = viewModel.Description,
                StartDatum = viewModel.StartDateString,
                SlutDatum = viewModel.EndDateString,
                Gruppkod = _apiKey,
                Utropspris = viewModel.StartPrice.ToString(),
                SkapadAv = _userService.GetCurrentUserName()
            };

            return model;
        }

        public HttpResponseMessage CreateNewAuction(AuctionModel newAuction)
        {
            return _auctionRepository.CreateNewAuction(newAuction);
        }

        //TODO Ev. fixa så man inte först behöver hämta alla
        public IEnumerable<IndexAuctionViewModel> GetOpenAuctions()
        {
            IEnumerable<AuctionModel> allAuctionsDb = _auctionRepository.GetAllAuctions();
            IEnumerable<IndexAuctionViewModel> allAuctions = SetIndexAuctionViewModel(allAuctionsDb);

            IEnumerable<IndexAuctionViewModel> openAuctions = allAuctions.
                                                           Where(auction => auction.AuctionIsOpen == true).ToList();

            return openAuctions;
        }

        //TODO Fixa GetAuctionIsOpen
        public IEnumerable<IndexAuctionViewModel> SetIndexAuctionViewModel(IEnumerable<AuctionModel> auctions)
        {
            var viewModelList = auctions.Select(x => new IndexAuctionViewModel
            {
                Id = Int32.Parse(x.AuktionID),
                Title = x.Titel,
                StartPrice = decimal.Parse(x.Utropspris),
                EndDate = x.SlutDatum,
                AuctionIsOpen = GetAuctionIsOpen(x.SlutDatum)
            }).OrderBy(auction => auction.Title);

            return viewModelList;
        }

        //TODO Ev. fixa så man inte först behöver hämta alla
        public IEnumerable<SearchResultAuctionViewModel> GetSearchResult(string searchInput)
        {
            IEnumerable<AuctionModel> allAuctionsDb = _auctionRepository.GetAllAuctions();
            IEnumerable<SearchResultAuctionViewModel> allAuctions = SetSearchResultViewModel(allAuctionsDb);

            if (searchInput != null)
            {
                IEnumerable<SearchResultAuctionViewModel> searchResult = allAuctions.Where(auction =>
                                                                      auction.Title.ToLower().Contains(searchInput.ToLower()) ||
                                                                      auction.Description.ToLower().Contains(searchInput.ToLower())).ToList();

                return searchResult;
            }

            return allAuctions;
        }

        public IEnumerable<SearchResultAuctionViewModel> SetSearchResultViewModel(IEnumerable<AuctionModel> auctions)
        {
            var viewModel = auctions.Select(x => new SearchResultAuctionViewModel
            {
                Id = Int32.Parse(x.AuktionID),
                Title = x.Titel,
                Description = x.Beskrivning,
                StartPrice = decimal.Parse(x.Utropspris),
                EndDate = x.SlutDatum,
            }).OrderBy(auction => auction.Title);

            AuctionViewModel vm = new AuctionViewModel
            {
                SearchResultAuctionViewModel = viewModel
            };

            return viewModel;
        }

        //TODO Kolla hur man inte först behöver hämta alla
        //public IEnumerable<SearchResultAuctionViewModel> GetAuctionsSorted(string sortParam)
        //{
        //    //IEnumerable<SearchResultAuctionViewModel> searchResult = GetSearchResult()

        //    //IEnumerable<TestAuctionViewModel> allAuctions = SetGeneralAuctionViewModelList(allAuctionsDb);

        //    //if (sortParam == "endDate")
        //    //{
        //    //    allAuctions = allAuctions.OrderBy(x => x.GeneralAuctionViewModel.EndDateString);
        //    //}
        //    //else if (sortParam == "startPrice")
        //    //{
        //    //    allAuctions = allAuctions.OrderBy(x => x.GeneralAuctionViewModel.StartPrice);
        //    //}

        //    //return allAuctions;

        //    //IEnumerable<AuctionModel> allAuctionsDb = _auctionRepository.GetAllAuctions();
        //    //IEnumerable<TestAuctionViewModel> allAuctions = SetGeneralAuctionViewModelList(allAuctionsDb);

        //    //if (sortParam == "endDate")
        //    //{
        //    //    allAuctions = allAuctions.OrderBy(x => x.GeneralAuctionViewModel.EndDateString);
        //    //}
        //    //else if (sortParam == "startPrice")
        //    //{
        //    //    allAuctions = allAuctions.OrderBy(x => x.GeneralAuctionViewModel.StartPrice);
        //    //}

        //    //return allAuctions;
        //}

        public DetailsAuctionViewModel GetAuctionById(int auctionId)
        {
            AuctionModel currentAuctionDb = _auctionRepository.GetAuctionById(auctionId);
            DetailsAuctionViewModel currentAuction = SetDetailsAuctionViewModel(currentAuctionDb);

            return currentAuction;
        }

        public bool GetAuctionIsOpen(string endDateString)
        {
            DateTime endDate = DateTime.Parse(endDateString);
            DateTime today = DateTime.Now;            

            if (endDate > today)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //TODO Fixa GetAuctionIsOpen
        public DetailsAuctionViewModel SetDetailsAuctionViewModel(AuctionModel model)
        {
            DetailsAuctionViewModel viewModel = new DetailsAuctionViewModel
            {
                Id = Int32.Parse(model.AuktionID),
                Title = model.Titel,
                Description = model.Beskrivning,
                StartDateString = model.StartDatum,
                EndDateString = model.SlutDatum,
                StartPrice = decimal.Parse(model.Utropspris),
                CreatedBy = model.SkapadAv,
                AllBids = GetBidsForAuction(Int32.Parse(model.AuktionID)),
                AuctionIsOpen = GetAuctionIsOpen(model.SlutDatum)
            };

            viewModel.HighestBid = viewModel.AllBids.OrderByDescending(r => r.BidPrice).Select(r => r.BidPrice).FirstOrDefault();

            return viewModel;
        }

        public List<OpenAuctionBidViewModel> GetBidsForAuction(int auctionId)
        {
            IEnumerable<BidModel> model = _auctionRepository.GetBidsForAuction(auctionId);
            List<OpenAuctionBidViewModel> viewModel = SetBidViewModel(model);

            return viewModel;
        }

        //TODO Kolla hur den här valideringen kan göras på klienten
        public bool GetBidIsValid(decimal newBid, int auctionId)
        {   
            DetailsAuctionViewModel currentAuction = GetAuctionById(auctionId);

            decimal currentHighestBid = GetHighestBidForAuction(auctionId);
            decimal startPrice = currentAuction.StartPrice;

            if ((newBid > currentHighestBid) && (newBid > startPrice))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //TODO Byta viewModel(???)
        public bool GetAuctionHasBids(int auctionId)
        {
            List<OpenAuctionBidViewModel> bids = GetBidsForAuction(auctionId);

            if (bids.Any())
            {
                return true;
            }

            return false;
        }

        public decimal GetHighestBidForAuction(int auctionId)
        {
            List<decimal> bidValues = GetBidValues(auctionId);

            decimal highestBid = 0;

            if (bidValues.Any())
            {
                highestBid = bidValues.Max();

                return highestBid;
            }

            return highestBid;
        }

        public List<decimal> GetBidValues(int auctionId)
        {
            List<OpenAuctionBidViewModel> bidList = GetBidViewModels(auctionId);

            List<decimal> bidPriceList = new List<decimal>();

            foreach (var bidViewModel in bidList)
            {
                bidPriceList.Add(bidViewModel.BidPrice);
            }

            return bidPriceList;
        }

        public List<OpenAuctionBidViewModel> GetBidViewModels(int auctionId)
        {
            IEnumerable<BidModel> model = _auctionRepository.GetBidsForAuction(auctionId);
            List<OpenAuctionBidViewModel> viewModel = SetBidViewModel(model);

            return viewModel;
        }

        public List<OpenAuctionBidViewModel> SetBidViewModel(IEnumerable<BidModel> model)
        {
            List<OpenAuctionBidViewModel> viewModel = model
                .Select(bidDb => new OpenAuctionBidViewModel()
                {
                    Id = int.Parse(bidDb.AuktionID),
                    BidPrice = decimal.Parse(bidDb.Summa),
                    AuctionId = int.Parse(bidDb.AuktionID),
                    Bidder = bidDb.Budgivare
                }).ToList();

            return viewModel;
        }

        public BidModel MakeBidApiReady(OpenAuctionBidViewModel viewModel, string bidder)
        {
            BidModel model = new BidModel
            {
                Summa = viewModel.BidPrice.ToString(),
                AuktionID = viewModel.AuctionId.ToString(),
                Budgivare = bidder
            };

            return model;
        }

        public HttpResponseMessage MakeBid(BidModel newBid)
        {
            return _auctionRepository.MakeBid(newBid);
        }
    }
}
