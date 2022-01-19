using LongRuningAPI.Data;
using LongRuningAPI.Entities;
using LongRuningAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LongRuningAPI.Controllers
{
    public class ApiController : Controller
    {
        private readonly LRADataContext _dataContext;

        public ApiController(LRADataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IActionResult> LongRunningRequest(LongRunningRequest request)
        {
            var executionTime = new Random().Next(10, 20);

            var longRequest = new LongRequest()
            {
                Complete = false,
                ExternalId = request.ExternalId,
                StartDate = DateTime.Now,
                ThreadId = request.ThreadId,
                ExecutionTime = executionTime
            };

            _dataContext.LongRequests.Add(longRequest);
            await _dataContext.SaveChangesAsync();

            Thread.Sleep(executionTime * 1000);

            longRequest.Complete = true;
            longRequest.CompleteDate = DateTime.Now;
            await _dataContext.SaveChangesAsync();

            return Content(longRequest.Id.ToString());
        }

        public JsonResult RunningRequests()
        {
            var requests = _dataContext.LongRequests.Where(r => !r.Complete).OrderByDescending(r => r.Id).ToList();

            return Json(requests);
        }
    }
}
