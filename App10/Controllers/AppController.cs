using App10.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App10.Controllers
{
    [Route("entity")]
    public class AppController : Controller
    {
        AppDBContext DBContext { get; set; }
        public AppController(AppDBContext dbContext) 
        {
            DBContext = dbContext;
        }
        [Route("list")]
        [HttpGet]
        public ActionResult<IEnumerable<SomeEntity>> GetList() 
        {
            return DBContext.SomeEntities;
        }
        [Route("create")]
        [HttpPost]
        public IActionResult Create([FromBody] SomeEntity entity) 
        {
            if (entity.Id != 0)
                return Unauthorized();
            DBContext.SomeEntities.Add(entity);
            DBContext.SaveChanges();
            return Ok();
        }

        [Route("{Id:int}/read")]
        [HttpGet]
        public ActionResult<SomeEntity> Read(int Id) 
        {
            var res = DBContext.SomeEntities.Where(e => e.Id == Id).FirstOrDefault();
            if (res == null)
                return NotFound();
            return DBContext.SomeEntities.Where(e => e.Id == Id).FirstOrDefault();
        }
    }
}
