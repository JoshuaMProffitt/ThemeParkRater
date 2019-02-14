﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThemeParkRater.Models.ThemeParkRatingModels;
using ThemeParkRater.Services;

namespace ThemeParkRater.WebMVC.Controllers
{
    public class ThemeParkRatingController : Controller
    {
        // GET: ThemeParkRating
        public ActionResult Index()
        {
            return View();
        }

        // GET: ThemeParkRating/Create
        public ActionResult Create()
        {
            var parkService = new ThemeParkService();
            var parkList = parkService.GetThemeParks();

            ViewBag.ThemeParkID = new SelectList(parkList, "ThemeParkID", "ThemeParkName");

            return View();
        }

        // POST: ThemeParkRating/Create
        [HttpPost]
        public ActionResult Create(ThemeParkRatingCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = GetRatingService();

            if (service.CreateRating(model))
            {
                return RedirectToAction("Index","ThemePark");
            }

            var parkService = new ThemeParkService();
            var parkList = parkService.GetThemeParks();

            ViewBag.ThemeParkID = new SelectList(parkList, "ThemeParkID", "ThemeParkName");

            return View(model);
        }

        // GET: ThemeParkRating/Details/{id}
        public ActionResult Details(int id)
        {
            var service = GetRatingService();
            var model = service.GetRatingsByParkID(id);
            return View(model);
        }

        // Helper method that gets our user ID and creates a ThemeParkService with it
        private ThemeParkRatingService GetRatingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ThemeParkRatingService(userId);
            return service;
        }
    }
}