using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using SharedModels;
using Newtonsoft.Json;

namespace WebAPI.Tests
{
    [TestClass]
    public class TestSaveOrder
    {
        [TestMethod]
        public void Test1()
        {
            SaveOrderController controller = new SaveOrderController();
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            var body = new List<OrderModel>();

            //Dummy order on prduct 91
            body.Add(new OrderModel()
            {
                ClientId = 1,
                OrderCount = 1,
                ProductId = 91,
            });

            //Dummy order on prduct 103
            body.Add(new OrderModel()
            {
                ClientId = 1,
                OrderCount = 2,
                ProductId = 103,
            });

            HttpResponseMessage response = controller.Post(body);
            var jsonData = response.Content.ReadAsStringAsync().Result;
            var resultObj = JsonConvert.DeserializeAnonymousType<List<OrderModel>> (jsonData, new List<OrderModel>());

            Assert.AreEqual(response.IsSuccessStatusCode, true); //status code 200 OK
            Assert.IsTrue(resultObj.Count > 0); //got some data
        }
    }
}
