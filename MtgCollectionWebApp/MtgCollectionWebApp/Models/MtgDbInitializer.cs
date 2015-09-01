using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

/* Api used documentation
   https://mtgapi.com/docs */

namespace MtgCollectionWebApp.Models
{
    public class MtgDbInitializer : System.Data.Entity.DropCreateDatabaseAlways<MtgCollectionDB>
    {
        protected override void Seed(MtgCollectionDB context)
        {
            String currentPage;
            String lastPage;
            String nextPage;
            int cardsPerPage;
            var initialUrl = "http://api.mtgapi.com/v2/cards?page=1";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(initialUrl);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";
            var response = request.GetResponse();
            var sr = new StreamReader(response.GetResponseStream());

            var text = sr.ReadToEnd();
            JObject o = JObject.Parse(text);
            currentPage = (string)o["links"]["current"];
            lastPage = (string)o["links"]["last"];
            nextPage = (string)o["links"]["next"];
            cardsPerPage = (int)o["perPage"];


            for (int j = 0; j < 10; j++)
            // while (!currentPage.Equals(lastPage))
            {
                request = (HttpWebRequest)WebRequest.Create(currentPage);
                request.Method = WebRequestMethods.Http.Get;
                request.Accept = "application/json";
                response = request.GetResponse();

                sr = new StreamReader(response.GetResponseStream());
                text = sr.ReadToEnd();
                o = JObject.Parse(text);

                for (int i = 0; i < cardsPerPage; i++)
                {
                    string artist = o["cards"][i]["artist"].ToString();
                    int cmc = (int)o["cards"][i]["cmc"];
                    string colors = o["cards"][i]["colors"].ToString();
                    string flavour = (string)o["cards"][i]["flavor"];
                    string foreignNames = o["cards"][i]["foreignNames"].ToString();
                    string hand = o["cards"][i]["hand"].ToString();
                    string imageUrl = "http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=" + o["cards"][i]["multiverseid"] + "&type=card";
                    string layout = (string)o["cards"][i]["layout"];
                    string legalities = o["cards"][i]["legalities"].ToString();
                    string life = o["cards"][i]["life"].ToString();
                    string loyalty = o["cards"][i]["loyalty"].ToString();
                    string manaCost = (string)o["cards"][i]["manaCost"];
                    int multiverseId = (int)o["cards"][i]["multiverseid"];
                    string name = (string)o["cards"][i]["name"];
                    string names = (string)o["cards"][i]["names"].ToString();
                    string power = (string)o["cards"][i]["power"];
                    string printings = o["cards"][i]["printings"].ToString();
                    string rarity = (string)o["cards"][i]["rarity"];
                    string set = (string)o["cards"][i]["set"];
                    string subTypes = o["cards"][i]["subtypes"].ToString();
                    string superTypes = o["cards"][i]["supertypes"].ToString();
                    string textC = (string)o["cards"][i]["text"];
                    string toughness = (string)o["cards"][i]["toughness"];
                    string type = (string)o["cards"][i]["type"];
                    string types = (string)o["cards"][i]["types"].ToString();
                    string variations = o["cards"][i]["variations"].ToString();
                    string watermark = o["cards"][i]["watermark"].ToString();
                    string url = "http://api.mtgapi.com/v1/card/id/" + (string)o["cards"][i]["multiverseid"];

                    try
                    {
                        if (multiverseId != 0)
                        {
                            if (context.Cards.Find(multiverseId) == null)
                            {
                                System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\test.txt", true);
                                file.WriteLine(name + " " + multiverseId);
                                file.Close();
                                context.Cards.Add(new Card
                                {
                                    CardId = multiverseId,
                                    CardArtist = artist,
                                    CardCmc = cmc,
                                    CardColors = colors,
                                    CardFlavour = flavour,
                                    CardForeignNames = foreignNames,
                                    CardHand = hand,
                                    CardImageUrl = imageUrl,
                                    CardLayout = layout,
                                    CardLegalities = legalities,
                                    CardLife = life,
                                    CardLoyalty = loyalty,
                                    CardManaCost = manaCost,
                                    CardMultiverseId = multiverseId,
                                    CardName = name,
                                    CardNames = names,
                                    CardPower = power,
                                    CardPrintings = printings,
                                    CardRarity = rarity,
                                    CardSet = set,
                                    CardSubTypes = subTypes,
                                    CardSuperTypes = superTypes,
                                    CardText = textC,
                                    CardToughness = toughness,
                                    CardType = type,
                                    CardTypes = types,
                                    CardVariations = variations,
                                    CardWatermark = watermark,
                                    CardUrl = url
                                });
                            }
                        }
                    }
                    catch (System.Data.DataException e)
                    {

                    }

                }

                currentPage = nextPage;
                nextPage = (string)o["links"]["next"];
                cardsPerPage = (int)o["perPage"];
            }

            base.Seed(context);
        }
    }
}