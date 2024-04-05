using Newtonsoft.Json;
using System.Xml;
using ConsoulLibrary;

namespace Terminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var filepath = ConsoulLibrary.Consoul.PromptForFilepath("Enter filepath for dinosaur json", true);
            var dinosaurs = JsonConvert.DeserializeObject<List<Dinosaur>>(File.ReadAllText(filepath));

            foreach (var item in dinosaurs)
            {
                string searchRequest = $"https://www.google.com/?q={item.Name}";
                string searchResult;
                try
                {
                    searchResult = GetSearchResults(searchRequest).Result;
                }
                catch (Exception ex)
                {
                    Consoul.Write("Failed to search for '" + item.Name + "'.");
                    continue;
                }

                string extractedImage;
                try
                {
                    extractedImage = ExtractImageLink(searchResult);
                }
                catch (Exception ex)
                {
                    Consoul.Write("Failed to extract image for '" + item.Name + "'.");
                    continue;
                }

                if (!string.IsNullOrEmpty(extractedImage))
                {
                    item.ImageUrl = extractedImage;
                    Consoul.Write("Image Found", ConsoleColor.Green);
                } else
                {
                    Consoul.Write("Image not found", ConsoleColor.Red);
                }
            }

            if (Consoul.Ask("Would you like to overwrite this JSON file?"))
            {
                Consoul.Write("Overwriting file...", ConsoleColor.Yellow, false);
                File.WriteAllText(filepath, JsonConvert.SerializeObject(dinosaurs));
                Consoul.Write("Finished!", ConsoleColor.Green);
            }

            Consoul.Write("Done", ConsoleColor.Green);
            Consoul.Wait();
        }

        public static string ExtractImageLink(string html)
        {
            const string id = "VisualDigestFirstImageResult";
            var firstImageResultIndex = html.IndexOf(id);
            if (firstImageResultIndex == -1)
                throw new IndexOutOfRangeException("Couldn't find '" + id + "'");
            var firstImageResult = html.Substring(firstImageResultIndex);

            var imgIndex = firstImageResult.IndexOf("img");
            if (imgIndex == -1)
                throw new IndexOutOfRangeException("Couldn't find 'img'");
            var img = firstImageResult.Substring(imgIndex);

            var srcIndex = img.IndexOf("src=\"");
            if (srcIndex == -1)
                throw new IndexOutOfRangeException("Couldn't find 'src'");
            var src = img.Substring(srcIndex);

            var contents = src.Substring(src.IndexOf("\""), src.LastIndexOf("\"") - 1);
            return contents;
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(html);
            //XmlNode node = doc.SelectSingleNode("//*[@data-attrid='VisualDigestFirstImageResult']//img");
            //if (node != null)
            //    return (node as XmlElement)?.GetAttribute("src");
            //return null;
        }
        public static async Task<string> GetSearchResults(string query)
        {
            //string apiKey = "YOUR_API_KEY";
            //string cx = "YOUR_CUSTOM_SEARCH_ENGINE_ID";
            //string url = $"https://www.googleapis.com/customsearch/v1?key={apiKey}&cx={cx}&q={query}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(query);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }

    public class Dinosaur
    {
        public int Id { get; set; }

        /// <summary>
        /// Name of the dinosaur
        /// </summary>
        public string Name { get; set; }

        public string Pronunciation { get; set; }

        public string MeaningOfName { get; set; }

        public string Diet { get; set; }

        public string Length { get; set; }

        public string Period { get; set; }

        public string Mya { get; set; }

        public string Info { get; set; }

        /// <summary>
        /// Image link
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
