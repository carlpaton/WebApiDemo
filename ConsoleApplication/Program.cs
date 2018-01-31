using Newtonsoft.Json;
using SharedModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            CallMockService();

            Console.Read();
        }

        private static void CallMockService()
        {
            var api = "http://localhost:8089/api/saveorder"; //SoapUI address
            api = "http://localhost:53610/api/saveorder"; //WebAPI Address

            var apiPassKey = "0000-0000-0000-0000"; //Password validation to be done at Web API application

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

            var request = (HttpWebRequest)WebRequest.Create(api);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("apiPassKey", apiPassKey); //Authentication in header if required

            //Append body content
            var sw = new StreamWriter(request.GetRequestStream());
            var jsonBody = JsonConvert.SerializeObject(body);
            sw.Write(jsonBody);
            sw.Close();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    try
                    {
                        var json = sr.ReadToEnd();
                        Console.WriteLine("------ JSON DATA");
                        Console.WriteLine(json);

                        var responseData = JsonConvert.DeserializeObject<List<OrderModel>>(json);
                        Console.WriteLine("------ COUNT = {0}", responseData.Count);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
