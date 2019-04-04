using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Homework.Models;
using Microsoft.AspNetCore.Http;

namespace Homework.Controllers
{
    public class WorkController : Controller
    {
        public WorkController(IWorkRepository workItems)
        {
            WorkItems = workItems;
        }

        public IWorkRepository WorkItems { get; set; }
        
        [Route("~/api/work/Create",
            Name = "create")]
        [HttpPost]
        public IActionResult Create(WorkItem item)
        {
            RequestLoger.DoLog(Request.Method, HttpContext.Connection.LocalIpAddress.ToString(), string.Join("/", new[] { Request.Host, RouteData.Values["controller"], RouteData.Values["action"] }));
            if (item == null)
            {
                return BadRequest();
            }
            WorkItems.Add(item);
            return new NoContentResult();
        }

        [Route("~/api/work/Update",
            Name = "update")]
        public IActionResult Update(string key, WorkItem item)
        {
            Request.Method = "PUT";
            RequestLoger.DoLog(Request.Method, HttpContext.Connection.LocalIpAddress.ToString(), string.Join("/", new[] { Request.Host, RouteData.Values["controller"], RouteData.Values["action"] }));
            if (item == null || item.Key != key)
            {
                return BadRequest();
            }

            var work = WorkItems.Find(key);
            if (work == null)
            {
                return NotFound();
            }

            WorkItems.Update(item);
            return new NoContentResult();
        }
        /*
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
        }*/

        [Route("~/api/work/Delete",
            Name = "delete")]
        public IActionResult Delete(string key)
        {
            Request.Method = "DELETE";
            RequestLoger.DoLog(Request.Method, HttpContext.Connection.LocalIpAddress.ToString(), string.Join("/", new[] { Request.Host, RouteData.Values["controller"], RouteData.Values["action"] }));
            var work = WorkItems.Find(key);
            if (work == null)
            {
                return NotFound();
            }

            WorkItems.Remove(key);
            return new NoContentResult();
        }

        [Route("~/api/work/GetAll",
            Name = "getall")]
        public IEnumerable<WorkItem> GetAll()
        {
            RequestLoger.DoLog(Request.Method, HttpContext.Connection.LocalIpAddress.ToString(), string.Join("/", new[]{Request.Host, RouteData.Values["controller"],RouteData.Values["action"]}));
            return WorkItems.GetAll();
        }
    }
}