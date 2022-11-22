/*     
 *---------------------------------------------------------------------------------
 * 	   File name: Program.cs
 * 	Project name: LibraryKioskSystem
 *---------------------------------------------------------------------------------
 * Author’s name and email:	 Brycen Dunn; DUNNBE@ETSU.EDU		
 *          Course-Section:  CSCI 2210 - 001
 *           Creation Date:	 11.17.22	
 * --------------------------------------------------------------------------------
 */
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

            ShowHeading("Welcome to the Library Kiosk App", '-');
            
            BookMenu();

            void BookMenu()
            {
                Console.WriteLine("1 - Add a book to the library");
                Console.WriteLine("2 - Remove a book from the library");
                Console.WriteLine("3 - Change key (sort method)");
                Console.WriteLine("4 - Display all books");
                Console.WriteLine("5 - Exit the kiosk system");
                Console.Write("\nPlease make a choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());


                string path = @"/Users/vicke/Desktop/LibraryKioskSystem/KioskApp/books.csv";
                StreamReader reader = new StreamReader(path);
                AvlTree<Book> avlTree = new AvlTree<Book>();
                List<Book> books = avlTree.GetInorderEnumerator().ToList();
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
                }
                
                if (choice == 1)
                {
                    Console.WriteLine("Please enter the title of your book: ");
                    Book book = new Book();
                    book.Title = Console.ReadLine();
                    Console.WriteLine("Please enter the author of your book: ");
                    book.Author = Console.ReadLine();
                    Console.WriteLine("Please enter the number of pages in your book: ");
                    book.Pages = Console.ReadLine();
                    Console.WriteLine("Please enter the publisher of your book: ");
                    book.Publisher = Console.ReadLine();
                    avlTree.Add(book);
                    BookMenu();
                }

                if (choice == 2)
                {
                    Console.WriteLine("Please enter the name of the book you wish to remove:");
                    string search = Console.ReadLine();
                    
                    foreach(Book book in books)
                    {
                        if (book.Title == search)
                        {
                            avlTree.Remove(book);
                        }
                    }
                    BookMenu();
                }

                if (choice == 3)
                {
                    Console.WriteLine("Please enter the key you wish to sort by: ");
                    string key = Console.ReadLine();
                    if (key == "title")
                    {
                        books = avlTree.GetInorderEnumerator().ToList();
                        BookMenu();
                    }
                    if (key == "author")
                    {
                        books = avlTree.GetPostorderEnumerator().ToList();
                        BookMenu();
                    }
                    if (key == "publisher")
                    {
                        books = avlTree.GetPreorderEnumerator().ToList();
                        BookMenu();
                    }
                }

                if (choice == 4)
                {
                    Console.WriteLine("Displaying all books.");
                    foreach (var book in avlTree)
                    {
                        Console.WriteLine(book.Print());
                    }
                    BookMenu();
                }

                if (choice == 5)
                {
                    Console.WriteLine("Thank you for using the library kiosk system.");
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                }

                else if (choice > 5)
                {
                    Console.WriteLine("Invalid input!");
                    Thread.Sleep(1000);
                    Console.Clear();
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