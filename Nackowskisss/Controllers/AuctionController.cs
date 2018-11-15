using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nackowskisss.BusinessLayer;
using Nackowskisss.Models.API_Models;
using Nackowskisss.Models.API_ViewModels.AuctionViewModels;
using Nackowskisss.Models.API_ViewModels.BidViewModels;
using Nackowskisss.Services.Identity;

namespace Nackowskisss.Controllers
{
    public class AuctionController : Controller
    {
        private IBusinessService _businessService;
        private IUserService _userService;

        public AuctionController(IBusinessService businessService,
                                 IUserService userService)
        {
            _businessService = businessService;
            _userService = userService;
        }

        public IActionResult ViewAuctionDetails(int auctionId)
        {
            DetailsAuctionViewModel currentAuction = _businessService.GetAuctionById(auctionId);

            return View(currentAuction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //TODO Fixa validering av bud (client(isf förklarat), annars själv i bussLayer)
        public IActionResult MakeBid(OpenAuctionBidViewModel newBid)
        {
            if (ModelState.IsValid)
            {
                bool currentBidIsValid = _businessService.GetBidIsValid(newBid.BidPrice, newBid.AuctionId);

                if (currentBidIsValid == true)
                {
                    string bidder = _userService.GetCurrentUserName();

                    BidModel model = _businessService.MakeBidApiReady(newBid, bidder);

                    HttpResponseMessage response = _businessService.MakeBid(model);

                    return RedirectToAction("ViewAuctionDetails", "Auction", new { auctionId = newBid.AuctionId, message = "Bid has successfully been made" });
                }
                else
                {
                    return RedirectToAction("ViewAuctionDetails", "Auction", new { auctionId = newBid.AuctionId, message = "Bid is too low" });
                }
            }

            return View(newBid);
        }
    }
}