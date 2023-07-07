namespace HTML_to_JSON;

using HtmlAgilityPack;

using Newtonsoft.Json;

internal class Program
{
    static void Main(string[] args)
    {
        // Specify the folder name relative to the current directory
        string folderName = "HTML_in";
        string folderPath = Path.Combine("../../../", folderName);

        string[] htmlFiles = Directory.GetFiles(folderPath, "*.html");

        var outputData = new List<Dictionary<string, string>>();

        foreach (var htmlFile in htmlFiles)
        {
            string htmlContent = File.ReadAllText(htmlFile);

            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            var entryNodes = doc.DocumentNode.SelectNodes("//table[@width='100%']");

            if (entryNodes != null)
            {
                foreach (var entryNode in entryNodes)
                {
                    var classNameAttribute = entryNode.SelectSingleNode("tr/td/span")?.GetAttributeValue("class", "");

                    if (classNameAttribute == "bheadline_avherald")
                    {
                        var currentEntry = new Dictionary<string, string>();
                        currentEntry["Occurrence"] = entryNode.InnerText.Trim();
                        outputData.Add(currentEntry);
                    }
                    else if (outputData.Count > 0)
                    {
                        var imgElement = entryNode.SelectSingleNode(".//img[@class='frame']");
                        var headlineElement = entryNode.SelectSingleNode(".//span[@class='headline_avherald']");

                        if (imgElement != null)
                            outputData[outputData.Count - 1]["title"] = imgElement.GetAttributeValue("title", "Unknown");

                        if (headlineElement != null)
                            outputData[outputData.Count - 1]["headline_avherald"] = headlineElement.InnerText.Trim();
                    }
                }
            }
        }

        var json = JsonConvert.SerializeObject(outputData, Formatting.Indented);

        // Specify the output folder path
        string outputFolderPath = Path.Combine("../../../", "JSON_out");

        // Create the output folder if it doesn't exist
        Directory.CreateDirectory(outputFolderPath);

        // Specify the output file path
        string outputFile = Path.Combine(outputFolderPath, "combined_data_avherald.json");

        // Save the combined JSON data to a file
        File.WriteAllText(outputFile, json);


        string jsonUpdate = File.ReadAllText(Path.Combine(outputFolderPath, "combined_data_avherald.json"));
        List<HeraldCombined> items = JsonConvert.DeserializeObject<List<HeraldCombined>>(jsonUpdate);

        foreach (var item in items)
        {
            Guid guid = Guid.NewGuid();
            item.guid = guid.ToString();
        }

        string modifiedJson = JsonConvert.SerializeObject(items, Formatting.Indented);
        File.WriteAllText(Path.Combine(outputFolderPath, "modified_avherald.json"), modifiedJson);
    }
}