using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nackowskisss.BusinessLayer;
using Nackowskisss.Models;
using Nackowskisss.Models.API_Models;
using Nackowskisss.Models.API_ViewModels.AuctionViewModels;
using Nackowskisss.Models.API_ViewModels.UserViewModels;
using Nackowskisss.Models.AuctionViewModels;
using Nackowskisss.Services.Identity;

namespace Nackowskisss.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IBusinessService _businessService;
        private IUserService _userService;

        public AdminController(IBusinessService businessService,
                               IUserService userService)
        {
            _businessService = businessService;
            _userService = userService;
        }

        public IActionResult CreateNewAuction()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //TODO Kolla om man blir directad till Getten av Create??
        public IActionResult CreateNewAuction(CreateAuctionViewModel newAuction)
        {
            if (ModelState.IsValid)
            {
                CreateAuctionViewModel viewModel = _businessService.SetCreateAuctionViewModel(newAuction);
                AuctionModel model = _businessService.MakeAuctionApiReady(newAuction);

                HttpResponseMessage response = _businessService.CreateNewAuction(model);

                if (response.IsSuccessStatusCode == true)
                {
                    return RedirectToAction("Index", "Home", new { message = "Auction has successfully been created" });
                }

                return RedirectToAction("CreateNewAuction", "Admin", new { message = "Auction failed to be created" });
            }

            return View(newAuction);
        }

        public IActionResult DeleteAuction(int auctionId)
        {
            bool auctionHasBids = _businessService.GetAuctionHasBids(auctionId);

            if (auctionHasBids == false)
            {
                HttpResponseMessage response = _businessService.DeleteAuction(auctionId);

                return RedirectToAction("Index", "Home", new { message = "Auction has successfully been deleted" });
            }
            else
            {
                return RedirectToAction("ViewAuctionDetails", "Auction", new { auctionId = auctionId, message = "Auction with bids cannot be deleted" });
            }
        }

        public IActionResult UpdateAuction(int auctionId)
        {
            DetailsAuctionViewModel currentAuction = _businessService.GetAuctionById(auctionId);
            UpdateAuctionViewModel viewModel = _businessService.SetUpdateViewModel(currentAuction);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAuction(UpdateAuctionViewModel input)
        {
            if (ModelState.IsValid)
            {
                DetailsAuctionViewModel currentAuction = _businessService.GetAuctionById(input.Id);
                DetailsAuctionViewModel viewModel = _businessService.UpdateAuction(input, currentAuction);

                AuctionModel model = _businessService.MakeAuctionApiReady(viewModel);

                HttpResponseMessage response = _businessService.UpdateAuction(model);

                if (response.IsSuccessStatusCode == true)
                {
                    return RedirectToAction("ViewAuctionDetails", "Auction", new { auctionId = viewModel.Id, message = "Auction was successfully updated" });
                }

                return RedirectToAction("ViewAuctionDetails", "Auction", new { auctionId = viewModel.Id, message = "Auction has not been updated" });
            }

            return View(input);
        }

        public IActionResult AddNewAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewAdmin(AddNewAdminViewModel input)
        {
            if (ModelState.IsValid)
            {
                string result = _userService.AddNewAdmin(input);

                if (result == "Succeeded")
                {
                    return RedirectToAction("Index", "Home", new { message = "New admin has successfully been added" });
                }

                return RedirectToAction("AddNewAdmin", "Admin", new { input = input, message = "New admin has not been added" });

            }

            return View(input);
        }
    }
}