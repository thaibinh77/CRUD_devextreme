using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DevExtremeMvc.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DevExtremeMvc.Service;
using System.Threading.Tasks;

namespace DevExtremeMvc.Controllers
{
    public class CustomerDataController : ApiController
    {

        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync(DataSourceLoadOptions loadOptions)
        {
            var customers = await MvcApplication.ApiService.GetAllCustomers();

            return Request.CreateResponse(DataSourceLoader.Load(customers, loadOptions));
        }

    }
}