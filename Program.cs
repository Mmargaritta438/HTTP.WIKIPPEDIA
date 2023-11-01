using System.Xml.Linq;
using System.Data;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var urlWikia = "https://ru.wikipedia.org/wiki/.";

        var thisIsNotAPagesFile = "this_is_not_a_pages_file";

        var client = new HttpClient();

        var chars = Enumerable.Range('a', 'z' - 'a' + 1).Select(c => (char)c).ToArray();

        for (char messageAZ = 'a'; messageAZ <= 'z'; messageAZ++)
        {
            for (char messageAZS = 'a'; messageAZS <= 'z'; messageAZS++)
            {
                var formUrl = urlWikia + messageAZ + messageAZS;

                try
                {
                    HttpResponseMessage response = await client.GetAsync(formUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var document = await response.Content.ReadAsStringAsync();
                        var givenRoute = $".{messageAZ}{messageAZS}.\\..\\..\\HtmlFiles\\.html";
                        File.WriteAllText(givenRoute, document);
                        Console.WriteLine($"The HTML code of the page in this case {formUrl} is saved in the file {givenRoute}");

                        Console.Write($"{urlWikia}");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\t success");
                        Console.ForegroundColor = ConsoleColor.White;                                       
                        Console.WriteLine($"The page in this case {formUrl} does not exist.");

                        File.AppendAllText(thisIsNotAPagesFile, formUrl + Environment.NewLine);

                        File.Create(givenRoute).Close();
                    }
                    else
                    {
                        Console.Write($"{urlWikia}");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t not a success");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while processing this page {formUrl}: {ex.HResult}");
                }
            }
        }
    }
}