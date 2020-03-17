using System;
using System.Net.Http;
using DataModels;

namespace FirebaseConsumer {
    class Consumer {
        static void Main(string[] args) {
            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri("https://us-central1-mapspeople-45db3.cloudfunctions.net/respond");
                var responseTask = client.GetAsync("source");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode) {
                    var readTask = result.Content.ReadAsAsync<Source[]>();
                    readTask.Wait();

                    var sources = readTask.Result;
                    foreach (var source in sources) {
                        Console.WriteLine(source.TimeStamp);
                    }

                }

            }

            Console.ReadLine();

        }
    }
}
