using DevExpress.Utils.About;
using DevExpress.XtraPrinting.Native;
using DevExtreme.AspNet.Mvc;
using DevExtremeMvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DevExtremeMvc.Controllers
{
    public class ProductController : Controller
    {
        private ProductDataController data = new ProductDataController();
        // GET: Product
        public async Task<ActionResult> Index()
        {
            #region
            //// Call the GetByIdAsync method in ProductDataController
            //HttpResponseMessage response = await data.GetByIdAsync(productId);

            //// Kiểm tra mã phản hồi
            //if (response.IsSuccessStatusCode)
            //{
            //    // Đọc dữ liệu sản phẩm từ phản hồi API
            //    var productJson = await response.Content.ReadAsStringAsync();
            //    var product = JsonConvert.DeserializeObject<ProductModel>(productJson);

            //    // Truyền đối tượng sản phẩm tới view
            //    return View(product);
            //}
            //else
            //{
            //    TempData["ErrorMessage"] = "Product not found.";
            //    return RedirectToAction("Index"); // Redirect back to insert page on failure
            //}
            #endregion

            var listProducts = await MvcApplication.ApiService.GetAllProducts();

            if (listProducts == null)
            {
                TempData["ErrorMessage"] = "Products not found";
                return RedirectToAction("Index");
            }
            return View(listProducts);
        }

        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Insert(ProductModel product, HttpPostedFileBase image)
        {
            try
            {
                #region
                //// Call the PostAsync method in ProductDataController
                //HttpResponseMessage response = await data.PostAsync(product);

                //// Check the response status code
                //if (response.IsSuccessStatusCode)
                //{
                //    return RedirectToAction("Index"); // Redirect to the index page on success
                //}
                //else
                //{
                //    TempData["ErrorMessage"] = "Failed to insert product.";
                //    return RedirectToAction("Insert"); // Redirect back to insert page on failure
                //}
                #endregion

                if (image == null)
                {
                    TempData["ErrorMessage"] = "Please select photo.";
                    return RedirectToAction("Insert");
                }

                var fileName = Path.GetFileName(image.FileName);

                if (!fileName.Contains(".png") && !fileName.Contains(".jpg") && !fileName.Contains(".jpeg"))
                {
                    TempData["ErrorMessage"] = "Image is not in the correct format (must be png/jpg/jpeg).";
                    return RedirectToAction("Insert");
                }

                var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                image.SaveAs(path);
                product.imgLink = fileName;

                var insertedProduct = await MvcApplication.ApiService.InsertProduct(product);
                if (insertedProduct == null)
                {
                    TempData["ErrorMessage"] = "Failed to insert product.";
                    return RedirectToAction("Insert"); // Redirect back to insert page on failure   
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e;
                return RedirectToAction("Insert");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Update(int productId)
        {
            #region
            //// Call the GetByIdAsync method in ProductDataController
            //HttpResponseMessage response = await data.GetByIdAsync(productId);

            //// Kiểm tra mã phản hồi
            //if (response.IsSuccessStatusCode)
            //{
            //    // Đọc dữ liệu sản phẩm từ phản hồi API
            //    var productJson = await response.Content.ReadAsStringAsync();
            //    var product = JsonConvert.DeserializeObject<ProductModel>(productJson);

            //    // Truyền đối tượng sản phẩm tới view
            //    return View(product);
            //}
            //else
            //{
            //    TempData["ErrorMessage"] = "Product not found.";
            //    return RedirectToAction("Index"); // Redirect back to insert page on failure
            //}
            #endregion

            var product = await MvcApplication.ApiService.GetProductById(productId);

            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProductModel product, HttpPostedFileBase image)
        {
            try
            {
                #region
                //// Call the PostAsync method in ProductDataController
                //HttpResponseMessage response = await data.PutAsync(product);

                //// Check the response status code
                //if (response.IsSuccessStatusCode)
                //{
                //    return RedirectToAction("Index"); // Redirect to the index page on success
                //}
                //else
                //{
                //    TempData["ErrorMessage"] = "Failed to update product.";
                //    return RedirectToAction("Update"); // Redirect back to insert page on failure
                //}
                #endregion

                if (image != null)
                {
                    //Lấy tên file của hình được up lên
                    var fileName = Path.GetFileName(image.FileName);

                    if (!fileName.Contains(".png") && !fileName.Contains(".jpg") && !fileName.Contains(".jpeg"))
                    {
                        TempData["ErrorMessage"] = "Image is not in the correct format (must be png/jpg/jpeg).";
                        return RedirectToAction("Insert");
                    }

                    //Tạo đường dẫn tới file
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                    //Lưu tên
                    image.SaveAs(path);
                    product.imgLink = fileName;

                }

                var updatedProduct = await MvcApplication.ApiService.UpdateProduct(product);
                if (updatedProduct == null)
                {
                    TempData["ErrorMessage"] = "Failed to update product.";
                    return RedirectToAction("Update"); // Redirect back to insert page on failure 
                }
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Failed to update product.";
                return RedirectToAction("Update");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int productId)
        {
            #region
            //// Call the GetByIdAsync method in ProductDataController
            //HttpResponseMessage response = await data.GetByIdAsync(productId);

            //// Kiểm tra mã phản hồi
            //if (response.IsSuccessStatusCode)
            //{
            //    // Đọc dữ liệu sản phẩm từ phản hồi API
            //    var productJson = await response.Content.ReadAsStringAsync();
            //    var product = JsonConvert.DeserializeObject<ProductModel>(productJson);

            //    // Truyền đối tượng sản phẩm tới view
            //    return View(product);
            //}
            //else
            //{
            //    TempData["ErrorMessage"] = "Product not found.";
            //    return RedirectToAction("Index"); // Redirect back to insert page on failure
            //}
            #endregion

            var product = await MvcApplication.ApiService.GetProductById(productId);

            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(ProductModel product)
        {
            try
            {
                #region
                //// Call the PostAsync method in ProductDataController
                //HttpResponseMessage response = await data.DeleteAsync(productId);

                //// Check the response status code
                //if (response.IsSuccessStatusCode)
                //{
                //    return RedirectToAction("Index"); // Redirect to the index page on success
                //}
                //else
                //{
                //    TempData["ErrorMessage"] = "Failed to delete product.";
                //    return RedirectToAction("Index"); // Redirect back to insert page on failure
                //}
                #endregion

                var deleteProduct = await MvcApplication.ApiService.DeleteProduct(product.productId);
                if (deleteProduct == null)
                {
                    TempData["ErrorMessage"] = "Failed to delete product.";
                    return RedirectToAction("Index"); // Redirect back to insert page on failure 
                }
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Failed to delete product.";
                return RedirectToAction("Index");
            }
        }
    }
}