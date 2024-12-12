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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace DevExtremeMvc.Controllers
{
    public class ProductDataController : ApiController
    {
        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync(DataSourceLoadOptions loadOptions)
        {
            var products = await MvcApplication.ApiService.GetAllProducts();

            return Request.CreateResponse(DataSourceLoader.Load(products, loadOptions));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetByIdAsync(int productId)
        {
            var product = await MvcApplication.ApiService.GetProductById(productId);

            if (product == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed");
            }
            //test
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, product);


            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> PostAsync(ProductModel product)
        {
            if (product == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed");
            }

            // Gọi service để thêm sản phẩm vào cơ sở dữ liệu
            var insertedProduct = await MvcApplication.ApiService.InsertProduct(product);

            if (insertedProduct == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed");
            }

            return Request.CreateResponse(HttpStatusCode.Created, insertedProduct);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> PutAsync(ProductModel product)
        {
            if (product == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed");
            }

            // Gọi service để thêm sản phẩm vào cơ sở dữ liệu
            var updatedProduct = await MvcApplication.ApiService.UpdateProduct(product);

            if (updatedProduct == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed");
            }

            return Request.CreateResponse(HttpStatusCode.Created, updatedProduct);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAsync(int productId)
        {
            // Gọi service để thêm sản phẩm vào cơ sở dữ liệu
            var deleteProduct = await MvcApplication.ApiService.DeleteProduct(productId);

            if (deleteProduct != "Delete success")
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed");
            }

            return Request.CreateResponse(HttpStatusCode.OK, deleteProduct);
        }
    }
}