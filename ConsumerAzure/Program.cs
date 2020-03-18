using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ConsumerAzure.JsonModel;
using DataModels;
using Newtonsoft.Json;

namespace ConsumerAzure {
    class Program {
        //TODO: make this better polling and not garbage as it is right now
        static void Main(string[] args) {
          while (true) {
                Thread.Sleep(3000);
                List<Location> data = GetData();
                foreach (Location l in data) {
                    Console.WriteLine(l.Id);
                }
            }
        }



        private static List<Location> GetData() {
            string jsonstr;
            var request = WebRequest.Create("https://mi-ucn-live-data.azurewebsites.net/occupancy?datasetid=6fbb3035c7e2436ba335edac") as HttpWebRequest;
            var response = request.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream())) {
                jsonstr = sr.ReadToEnd();
            }
            List<RootObject> sources = JsonConvert.DeserializeObject<List<RootObject>>(jsonstr);

            return ConvertFromJsonToInternalModel(sources);
        }

        private static List<Location> ConvertFromJsonToInternalModel(List<RootObject> sources) {
            List<Location> locations = new List<Location>();
            foreach(RootObject r in sources) {
                Location location = new Location();
                location.Id = r.spaceRefId;
                location.Sources = new List<Source>();
                foreach(LastReport lr in r.lastReports) {
                    Source source = new Source();
                    State state = new State();
                    state.MotionDetected = lr.motionDetected;
                    state.PersonCount = lr.personCount;
                    state.SignsOfLife = state.SignsOfLife;
                    string json = JsonConvert.SerializeObject(state);
                    source.Id = lr.id;
                    source.Type = "Occupancy";
                    source.State = json;
                    source.TimeStamp = lr.timeStamp;
                    location.Sources.Add(source);
                }
                locations.Add(location);
            }
            return (locations);
        }
    }
}
