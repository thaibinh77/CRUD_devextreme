using DevExtremeMvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DevExtremeMvc.Service
{
    public class Api_Service
    {
        //Uri apiAddress = new Uri("https://192.168.1.108:7018/");

        Uri apiAddress = new Uri("https://localhost:7018/");

        private readonly HttpClient _httpClient;

        public Api_Service()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = apiAddress;
        }


        //        CUSTOMER
        public async Task<List<CustomerModel>> GetAllCustomers()
        {
            List<CustomerModel> listCustomers = new List<CustomerModel>();

            HttpResponseMessage response = await _httpClient.GetAsync
                (_httpClient.BaseAddress + "Customer/GetAllCustomers");

            if(response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                listCustomers = JsonConvert.DeserializeObject<List<CustomerModel>>(jsonData);
            }

            return listCustomers;
        }


        //        PRODUCT
        public async Task<List<ProductModel>> GetAllProducts()
        {
            List<ProductModel> listProducts = new List<ProductModel>();

            HttpResponseMessage response = await _httpClient.GetAsync
                (_httpClient.BaseAddress + "Product/GetAllProducts");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                listProducts = JsonConvert.DeserializeObject<List<ProductModel>>(jsonData);
            }

            return listProducts;
        }

        public async Task<ProductModel> GetProductById(int productId)
        {
            ProductModel product = null;

            HttpResponseMessage response = await _httpClient.GetAsync
                (_httpClient.BaseAddress + "Product/GetProductById/" + productId);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<ProductModel>(jsonData);
            }

            return product;
        }

        public async Task<ProductModel> InsertProduct(ProductModel product)
        {
            ProductModel insertedProduct = new ProductModel();

            // Chuyển đổi đối tượng ProductModel sang JSON để gửi đi
            var jsonContent = JsonConvert.SerializeObject(product);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Thực hiện POST request tới API InsertProduct
            HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "Product/InsertProduct", contentString);

            if (response.IsSuccessStatusCode)
            {
                // Đọc dữ liệu phản hồi từ API và chuyển đổi sang ProductModel
                var jsonData = await response.Content.ReadAsStringAsync();
                insertedProduct = JsonConvert.DeserializeObject<ProductModel>(jsonData);
            }

            return insertedProduct;
        }

        public async Task<ProductModel> UpdateProduct(ProductModel product)
        {
            ProductModel updateProduct = null;

            // Chuyển đổi đối tượng ProductModel sang JSON để gửi đi
            var jsonContent = JsonConvert.SerializeObject(product);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Thực hiện POST request tới API InsertProduct
            HttpResponseMessage response = await _httpClient.PutAsync(_httpClient.BaseAddress + "Product/UpdateProduct", contentString);

            if (response.IsSuccessStatusCode)
            {
                // Đọc dữ liệu phản hồi từ API và chuyển đổi sang ProductModel
                var jsonData = await response.Content.ReadAsStringAsync();
                updateProduct = JsonConvert.DeserializeObject<ProductModel>(jsonData);
            }

            return updateProduct;
        }

        public async Task<string> DeleteProduct(int productId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + "Product/DeleteProduct/" + productId);

            if (response.IsSuccessStatusCode)
            {
                // Đọc dữ liệu phản hồi từ API
                var jsonData = await response.Content.ReadAsStringAsync();
                return jsonData; // Trả về chuỗi phản hồi
            }
            else
            {
                return "Failed to delete the product.";
            }
        }

    }
}
