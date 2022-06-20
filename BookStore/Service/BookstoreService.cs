using Bookstore.Model;
using Bookstore.Model.Payment;
using Bookstore.Service.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Bookstore.App.Service
{
    public class BookstoreService
    {
        readonly IConfiguration _config;
        readonly IBook _bookDb;
        readonly IOrder _order;
        private object key;
        private string endpoint;
        public BookstoreService(IConfiguration _config, IBook _bookDb, IOrder _order)
        {
            this._config = _config;
            this._bookDb = _bookDb;
            this._order = _order;
        }
        public void GetSecrets()
        {
            key = _config["paystack_Secret"];
            endpoint = _config["paystack_Endpoint"];
        }
        public async Task<string> InitializeCharge(Charge charge)
        {
            try
            {
                var client = GetClient();
                var url = endpoint;
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var json = JsonConvert.SerializeObject(charge);
                return await GetContent(httpResponse, json, url, client);
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }
        public int IsQuantity(int availableItem, int quantity)
        {
            if (quantity > availableItem)
            {
                quantity = availableItem;
                return quantity;
            }
            return quantity;
        }
        public HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {key}");
            return client;
        }
        public int GetPrice(List<OrderCart> currentCarts)
        {
            int price = 0;
            foreach (var currentCart in currentCarts)
            {
                price = currentCart.Quantity * currentCart.Book.Price;
            }
            return price;
        }
        public Pin CreatePin(string reference)
        {
            Pin pin = new Pin
            {
                Reference = reference
            };
            return pin;
        }
        public async Task<string> GetContent(HttpResponseMessage httpResponse, string json, string url, HttpClient client)
        {
            using (StringContent content = new StringContent(json))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpResponse = await client.PostAsync(url, content);
            }
            string contentString = await httpResponse.Content.ReadAsStringAsync();
            var newContent = JToken.Parse(contentString).ToString();
            return newContent;
        }
        public async Task<string> SubmitPin(Pin pin)
        {
            try
            {
                HttpClient client = GetClient();
                var url = endpoint + "/submit_pin";
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var json = JsonConvert.SerializeObject(pin);
                return await GetContent(httpResponse, json, url, client);
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
        public Charge SetCharge(int price, string email)
        {
            var charge = new Charge
            {
                Amount = (price).ToString(),
                Email = email,
                CardDetails = new Card()
            };
            return charge;
        }

        public async Task UpdateOrder(Guid orderId,OrderStatus status)
        {
            Order order = new Order { OrderId= orderId, Status = status};
            await _order.UpdateOrder(order);
        }

        public async Task UpdateItem(List<OrderCart> carts)
        {
            foreach (var cart in carts)
            {
                cart.Book.Detail.Quantity = cart.Book.Detail.Quantity - cart.Quantity;

                await _bookDb.UpdateBook(cart.Book);
            }
        }
    }
}
