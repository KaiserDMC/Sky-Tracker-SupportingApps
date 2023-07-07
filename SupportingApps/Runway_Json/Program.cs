using Newtonsoft.Json;

namespace Runway_Json
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            string json = File.ReadAllText("../../../runway_designators.json");
            List<RwyGuid> items = JsonConvert.DeserializeObject<List<RwyGuid>>(json);

            foreach (var item in items)
            {
                Guid guid = Guid.NewGuid();
                item.guid = guid.ToString();
            }

            string modifiedJson = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText("../../../modified_runway.json", modifiedJson);
        }
    }
}