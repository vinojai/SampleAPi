using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using itemratingsapi.Models;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System.Linq;

namespace Controllers
{
    public class ItemsClientController
	{
		public List<Item> Items { get; set; }

		public async Task<List<Item>> Get()
        {

            string url = "http://localhost:5001/api/Items";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            {
				Items = new List<Item>();
                using (Stream stream = response.GetResponseStream())
                {
					StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    string content = reader.ReadToEnd();
					Items = (List<Item>)JsonConvert.DeserializeObject(content, typeof(List<Item>));
					reader.Close();
					return Items;
                }
            }
        }
        
		public async Task<String> Put(Item ico)
        {

            string url = "http://localhost:5001/api/Items";
			string param = Convert.ToString(ico.Id);

			var content = new StringContent(ico.ToString());

            
			HttpClient client = new HttpClient();

			HttpResponseMessage response = await client.PutAsync(new Uri(String.Format("{0}?{1}", url, param)), content);
			return (response.ToString());
            
        }
    }
}
