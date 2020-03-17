using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FirebaseConsumer2 {
    class Consumer {
        static void Main(string[] args) {
            //using (var client = new HttpClient()) {
            //    client.BaseAddress = new Uri("https://us-central1-mapspeople-45db3.cloudfunctions.net/respond");
            //    var responseTask = client.GetAsync("source");
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode) {
            //        var readTask = result.Content.ReadAsAsync<Source[]>();
            //        readTask.Wait();

            //        var sources = readTask.Result;
            //        foreach (var source in sources) {
            //            Console.WriteLine(source.TimeStamp);
            //        }

            //    }

            //}

            //Console.ReadLine();
            //List<JObject> obs = new List<JObject>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://us-central1-mapspeople-45db3.cloudfunctions.net/respond");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("https://us-central1-mapspeople-45db3.cloudfunctions.net/respond").Result;

            if (response.IsSuccessStatusCode) {
                var result = response.Content.ReadAsStringAsync().Result;
                //JObject s = JObject.Parse(result);
                //string yourPrompt = (string)s["dialog"]["prompt"];
                var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
             
                Console.WriteLine(yourPrompt);
            } else {
                Console.WriteLine("failed"); ;
            }
            Console.ReadLine();
        }
       
    }

}


        

    

