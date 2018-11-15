using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Nackowskisss.Models;
using Nackowskisss.Models.API_Models;
using Newtonsoft.Json;

namespace Nackowskisss.DataLayer
{
    public class AuctionRepository : IAuctionRepository
    {
        private Uri baseAddressAuction;
        private Uri baseAddressBid;
        private string _apiKey;

        public AuctionRepository()
        {
            baseAddressAuction = new Uri("http://nackowskis.azurewebsites.net/api/Auktion/");
            baseAddressBid = new Uri("http://nackowskis.azurewebsites.net/api/Bud/");
            _apiKey = "1080";
        }

        public AuctionModel GetAuctionById(int auctionId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseAddressAuction;

                //Hanterar headers som borde komma vid anrop
                client.DefaultRequestHeaders.Accept.Clear();

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AuctionModel));

                HttpResponseMessage response = client.GetAsync(baseAddressAuction + _apiKey + "/" + auctionId.ToString()).Result;

                //Om response statusCode är fales så Throws exception
                response.EnsureSuccessStatusCode();

                //Tar emot en stream som ska serialiseras
                Stream responseStream = response.Content.ReadAsStreamAsync().Result;

                //Omvandlar den response stream som tagits emot till ett Auction objekt
                AuctionModel model = (AuctionModel)serializer.ReadObject(responseStream);

                return model;
            }
        }

        public IEnumerable<AuctionModel> GetAllAuctionss()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseAddressAuction;

                //Hanterar headers som borde komma vid anrop
                client.DefaultRequestHeaders.Accept.Clear();

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(IEnumerable<AuctionModel>));

                HttpResponseMessage response = client.GetAsync(_apiKey).Result;

                //Om response statusCode är fales så Throws exception
                response.EnsureSuccessStatusCode();

                //Tar emot en stream som ska serialiseras
                Stream responseStream = response.Content.ReadAsStreamAsync().Result;

                //Omvandlar den response stream som tagits emot till ett Auction objekt
                IEnumerable<AuctionModel> model = (IEnumerable<AuctionModel>)serializer.ReadObject(responseStream);

                return model;
            }
        }

        //TODO Ta bort
        public List<AuctionModel> GetAllAuctions()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseAddressAuction;

                //Hanterar headers som borde komma vid anrop
                client.DefaultRequestHeaders.Accept.Clear();

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<AuctionModel>));

                HttpResponseMessage response = client.GetAsync(_apiKey).Result;

                //Om response statusCode är fales så Throws exception
                response.EnsureSuccessStatusCode();

                //Tar emot en stream som ska serialiseras
                Stream responseStream = response.Content.ReadAsStreamAsync().Result;

                //Omvandlar den response stream som tagits emot till ett Auction objekt
                List<AuctionModel> model = (List<AuctionModel>)serializer.ReadObject(responseStream);

                return model;
            }
        }

        public HttpResponseMessage CreateNewAuction(AuctionModel newAuction)
        {
            using (HttpClient client = new HttpClient())
            {
                var modelJson = JsonConvert.SerializeObject(newAuction);

                var stringContent = new StringContent(modelJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(baseAddressAuction, stringContent).Result;
                return response;
                //Ev return på response
            }
        }

        public HttpResponseMessage DeleteAuction(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.DeleteAsync(baseAddressAuction + _apiKey + "/" + id.ToString()).Result;

                return response;
            }
        }
        //public void DeleteAuction(int id)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        HttpResponseMessage response = client.DeleteAsync(baseAddressAuction + _apiKey + "/" + id.ToString()).Result;
        //    }
        //}

        public HttpResponseMessage UpdateAuction(AuctionModel currentAuction)
        {
            var auctionID = GetAllAuctions().FirstOrDefault(x => x.AuktionID == currentAuction.AuktionID).AuktionID;

            AuctionModel model = new AuctionModel
            {
                AuktionID = auctionID,
                Titel = currentAuction.Titel,
                Beskrivning = currentAuction.Beskrivning,
                StartDatum = currentAuction.StartDatum,
                SlutDatum = currentAuction.SlutDatum,
                Gruppkod = currentAuction.Gruppkod,
                Utropspris = currentAuction.Utropspris,
                SkapadAv = currentAuction.SkapadAv
            };

            using (HttpClient client = new HttpClient())
            {
                var modelJson = JsonConvert.SerializeObject(model);

                var stringContent = new StringContent(modelJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync(baseAddressAuction, stringContent).Result;

                return response;
            }
        }

        public HttpResponseMessage MakeBid(BidModel bid)
        {
            using (HttpClient client = new HttpClient())
            {
                var modelJson = JsonConvert.SerializeObject(bid);

                var stringContent = new StringContent(modelJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(baseAddressBid, stringContent).Result;

                return response;
            }
        }

        public IEnumerable<BidModel> GetBidsForAuction(int auctionId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseAddressBid;
                ;
                client.DefaultRequestHeaders.Accept.Clear();

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(IEnumerable<BidModel>));

                HttpResponseMessage response = client.GetAsync(_apiKey + "/" + auctionId.ToString()).Result;
                response.EnsureSuccessStatusCode();
                Stream responseStream = response.Content.ReadAsStreamAsync().Result;

                IEnumerable<BidModel> model = (IEnumerable<BidModel>)serializer.ReadObject(responseStream);

                return model;
            }
        }
    }
}
