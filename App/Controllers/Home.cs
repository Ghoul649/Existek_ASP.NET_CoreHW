using App.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class Home : Controller
    {
        IPublishingService _publisher;
        IStorageService _storage;
        public Home(IPublishingService publishingService, IStorageService storage) 
        {
            _publisher = publishingService;
            _storage = storage;
        }
        [Route("publish")]
        public IActionResult Publish(Article a) 
        {
            try
            {
                _publisher.Publish(a);
            }
            catch (ArgumentException e)
            {
                return ValidationProblem(e.Message);
            }
            return Ok();
        }
        [Route("list")]
        public IActionResult List() 
        {
            return Json(_storage.Select(new Article() { IsPublished = true }));
        }
    }
}
