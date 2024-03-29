﻿using System.Threading.Tasks;
using RealEstate.Models.Home;
using RealEstate.Services;

namespace RealEstate.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.Diagnostics;
    using static Common.GlobalConstants;

    public class HomeController : Controller
    {
        private readonly IHomeService Service;

        public HomeController(IHomeService service)
        {
            this.Service = service;
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel estates = new IndexViewModel();

            estates.Statistics = await this.Service.GetStatistics();

            estates.AddEstates(await this.Service.GetLastAddedEstatesAsync(DefaultLastEstatesCount));

            return View(estates);
        }

        public async Task<IActionResult> About()
        {
            byte[] comapanyLogo = await this.Service.GetCompanyLogoAsync();
            return View(comapanyLogo);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
