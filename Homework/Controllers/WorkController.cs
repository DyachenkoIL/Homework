using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Homework.Models;
using Microsoft.AspNetCore.Http;

namespace Homework.Controllers
{
    [Route("api/work")]
    public class WorkController : Controller
    {
        public WorkController(IWorkRepository workItems)
        {
            WorkItems = workItems;
        }

        public IWorkRepository WorkItems { get; set; }


        public IEnumerable<WorkItem> GetAll()
        {
            RequestLoger.DoLog(Request.Method, HttpContext.Connection.LocalIpAddress.ToString(), string.Join("/", new[]{Request.Host, RouteData.Values["controller"],RouteData.Values["action"]}));
            return WorkItems.GetAll();
        }

        [HttpGet("{id}", Name = "GetWork")]
        public IActionResult GetById(string id)
        {
            RequestLoger.DoLog(Request.Method, HttpContext.Connection.LocalIpAddress.ToString(), string.Join("/", new[] { Request.Host, RouteData.Values["controller"], RouteData.Values["action"] }));
            var item = WorkItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create(WorkItem item)
        {
            RequestLoger.DoLog(Request.Method, HttpContext.Connection.LocalIpAddress.ToString(), string.Join("/", new[] { Request.Host, RouteData.Values["controller"], RouteData.Values["action"] }));
            if (item == null)
            {
                return BadRequest();
            }
            WorkItems.Add(item);
            return CreatedAtRoute("GetWork", new { id = item.Key }, item);
        }
        /*[HttpPost]
        public IActionResult Create(string plan, string status)
        {
            RequestLoger.DoLog(Request.Method, HttpContext.Connection.LocalIpAddress.ToString(), string.Join("/", new[] { Request.Host, RouteData.Values["controller"], RouteData.Values["action"] }));
            WorkItems.Add(plan,status);
            return View("~/Views/Home/Index.cshtml");
        }*/

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] WorkItem item)
        {
            RequestLoger.DoLog(Request.Method, HttpContext.Connection.LocalIpAddress.ToString(), string.Join("/", new[] { Request.Host, RouteData.Values["controller"], RouteData.Values["action"] }));
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var work = WorkItems.Find(id);
            if (work == null)
            {
                return NotFound();
            }

            WorkItems.Update(item);
            return new NoContentResult();
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] WorkItem item, string id)
        {
            RequestLoger.DoLog(Request.Method, HttpContext.Connection.LocalIpAddress.ToString(), string.Join("/", new[] { Request.Host, RouteData.Values["controller"], RouteData.Values["action"] }));
            if (item == null)
            {
                return BadRequest();
            }

            var work = WorkItems.Find(id);
            if (work == null)
            {
                return NotFound();
            }

            item.Key = work.Key;

            WorkItems.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            RequestLoger.DoLog(Request.Method, HttpContext.Connection.LocalIpAddress.ToString(), string.Join("/", new[] { Request.Host, RouteData.Values["controller"], RouteData.Values["action"] }));
            var work = WorkItems.Find(id);
            if (work == null)
            {
                return NotFound();
            }

            WorkItems.Remove(id);
            return new NoContentResult();
        }
    }
}