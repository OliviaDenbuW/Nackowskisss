using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nackowskisss.BusinessLayer;
using Nackowskisss.Models;
using Nackowskisss.Models.API_Models;
using Nackowskisss.Models.API_ViewModels.AuctionViewModels;

namespace Nackowskisss.Controllers
{
    public class HomeController : Controller
    {
        private IBusinessService _businessService;
        private IAuctionBusinessService _businessServiceGammal;

        public HomeController(IBusinessService bussinessService,
                              IAuctionBusinessService businessServiceGammal)
        {
            _businessService = bussinessService;
            _businessServiceGammal = businessServiceGammal;
        }

        public IActionResult Index()
        {
            IEnumerable<IndexAuctionViewModel> openAuctions = _businessService.GetOpenAuctions();

            AuctionViewModel viewModel = new AuctionViewModel
            {
                IndexAuctionViewModels = openAuctions
            };

            return View(openAuctions);
        }

        [HttpPost]
        public IActionResult SearchForAuction(string searchInput)
        {
            IEnumerable<SearchResultAuctionViewModel> searchResult = _businessService.GetSearchResult(searchInput);

            AuctionViewModel viewModel = new AuctionViewModel
            {
                SearchResultAuctionViewModel = searchResult,
                SearchInput = searchInput
            };


            return View("SearchResult", viewModel);
            /*return View("SearchResult", searchResult)*/
        }


        [HttpGet]
        public JsonResult EmpDetails()
        {
            IEnumerable<IndexAuctionViewModel> auctions = _businessService.GetOpenAuctions();

            return Json(auctions);
        }

        [HttpGet]
        public IActionResult SortAuctions(string searchInput, string sortParam)
        {
            IEnumerable<SearchResultAuctionViewModel> searchResult = _businessService.GetSearchResult(searchInput);
            if (sortParam == "endDate")
            {
                searchResult = searchResult.OrderBy(r => r.EndDate);
            }
            else if (sortParam == "startPrice")
            {
                searchResult = searchResult.OrderBy(r => r.StartPrice);
            }

            AuctionViewModel viewModel = new AuctionViewModel
            {
                SearchResultAuctionViewModel = searchResult,
                SearchInput = searchInput
            };

            return View("SearchResult", viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
