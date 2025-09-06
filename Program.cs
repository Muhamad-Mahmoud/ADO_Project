using BookManagement.DataAccess;

namespace BookManagement;

public class Program
{
  static void Main(string[] args)
  {
    var book = new BookDataAccess();
    while (true)
    {
      Console.WriteLine("\nLibrary Management System");
      Console.WriteLine("1. Add Book");
      Console.WriteLine("2. List Books");
      Console.WriteLine("3. Update Books");
      Console.WriteLine("4. Search Books");
      Console.WriteLine("5. Delete Books");
      Console.WriteLine("6. Exit");
      Console.Write("Enter your choice: ");

      int choice = int.Parse(Console.ReadLine());

      switch (choice)
      {
        case 1:
          book.AddBook();
          break;
        case 2:
          book.ListBooks();
          break;
        case 3:
          book.UpdateBook();
          break;
        case 4:
          book.SearchBook();
          break;
        case 5: book.DeleteBook(); break;
        case 6: return;
      }
    }
  }
}