using System.DataStructures;

namespace KioskApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            static void ShowHeading(string heading, char ch)
            {
                Console.WriteLine(new string(ch, heading.Length));
                Console.WriteLine(heading);
                Console.WriteLine(new string(ch, heading.Length));
            }
            
            string path = @"/Users/vicke/Desktop/LibraryKioskSystem/KioskApp/books.csv";
            using (StreamReader reader = new StreamReader(path))
            {
                AvlTree<Book> avlTree = new AvlTree<Book>();
                // Add the newest item split by the comma alone to a new string
                // array.
                string bookLine;
                List<string> cleanedLine;
                while ((bookLine = reader.ReadLine()) != null)
                {
                    cleanedLine = ProcessCSVLine(bookLine);
                    Book book = new Book();
                    book.Title = cleanedLine[0];
                    book.Author = cleanedLine[1];
                    book.Pages = cleanedLine[2];
                    book.Publisher = cleanedLine[3];
                    avlTree.Add(book);
                };
                
                foreach (var book in avlTree)
                {
                    Console.WriteLine(book.Print());
                }

                List<string> ProcessCSVLine(string lineFromCSV)
                {
                    // Split it based on a comma
                    string[] rawBookParts = lineFromCSV.Split(",");
                    // Create a list of book parts that represent the columns in the CSV
                    // We can treat anything that goes into this list as sanitized data.
                    List<string> sanitizedBookParts = new List<string>();
                    // Iterate through each book part found by naively spliting on the comma alone.
                    string cleanString = string.Empty;
                    for (int i = 0; i < rawBookParts.Length; i++)
                    {
                        // Add the newest item split by the comma alone to a new string
                        cleanString += rawBookParts[i];
                        // If the string starts with a quote, but does not end with a quote,
                        // then we know that the full text from the string hasn't been
                        // read in yet, and that the rest of the text be in a future
                        // element of rawBookParts.
                        if (cleanString.StartsWith("\"") && !cleanString.EndsWith("\""))
                        {
                            continue;
                        }
                        // Once it is verified that the clean string contains the entire
                        // text of the column, we can add it to our list of sanitized
                        // book parts. This is also a good time to remove the quotes
                        // at the beginning and end of the string if they exist.
                        sanitizedBookParts.Add(cleanString.Replace("\"", String.Empty));
                        // Reset the clean string for the next iteration.
                        cleanString = String.Empty;
                    }
                    return sanitizedBookParts;
                }
            }
        }
    }
}