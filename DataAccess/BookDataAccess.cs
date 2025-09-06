using BookManagement.DB_Tables;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DataAccess;

public class BookDataAccess
{
    private (string Title, string Author, string Genre, string ISBN, int PublishedYear, int CopiesAvailable) GetBookDetails()
    {
        Console.Write("Enter the book Title: ");
        var Title = Console.ReadLine();
        Console.Write("Enter the book Author: ");
        var author = Console.ReadLine();
        Console.Write("Enter the book Genre: ");
        var genre = Console.ReadLine();
        Console.Write("Enter the book ISBN: ");
        var isbn = Console.ReadLine();
        Console.Write("Enter the book Published Year: ");
        var publishedYear = int.Parse(Console.ReadLine());
        Console.Write("Enter the book Copies Available: ");
        var copiesAvailable = int.Parse(Console.ReadLine());

        return (Title, author, genre, isbn, publishedYear, copiesAvailable);
    }

    public void AddBook()
    {
        var bookDetails = GetBookDetails();

        var query = "INSERT INTO Books (Title, Author, ISBN, Genre, PublishedYear, CopiesAvailable) " +
                    "VALUES (@Title, @Author, @ISBN, @Genre, @PublishedYear, @CopiesAvailable)";

        using var connection = new DatabaseHelper().ConnectionToSQL();
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Title", bookDetails.Title);
        command.Parameters.AddWithValue("@Author", bookDetails.Author);
        command.Parameters.AddWithValue("@ISBN", bookDetails.ISBN);
        command.Parameters.AddWithValue("@Genre", bookDetails.Genre);
        command.Parameters.AddWithValue("@PublishedYear", bookDetails.PublishedYear);
        command.Parameters.AddWithValue("@CopiesAvailable", bookDetails.CopiesAvailable);

        try
        {
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine(rowsAffected > 0 ? "The new book was added successfully." : "Failed to add the new book.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void UpdateBook()
    {
        Console.Write("Enter the ID for the book you want to update: ");
        int id = int.Parse(Console.ReadLine());
        var bookDetails = GetBookDetails();

        var query = "UPDATE Books SET Title = @Title, Author = @Author, ISBN = @ISBN, Genre = @Genre, " +
                    "PublishedYear = @PublishedYear, CopiesAvailable = @CopiesAvailable WHERE BookID = @ID";

        using var connection = new DatabaseHelper().ConnectionToSQL();
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Title", bookDetails.Title);
        command.Parameters.AddWithValue("@Author", bookDetails.Author);
        command.Parameters.AddWithValue("@ISBN", bookDetails.ISBN);
        command.Parameters.AddWithValue("@Genre", bookDetails.Genre);
        command.Parameters.AddWithValue("@PublishedYear", bookDetails.PublishedYear);
        command.Parameters.AddWithValue("@CopiesAvailable", bookDetails.CopiesAvailable);
        command.Parameters.AddWithValue("@ID", id);

        try
        {
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine(rowsAffected > 0 ? "The book was updated successfully." : "Failed to update the book.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void ListBooks()
    {
        var query = "SELECT * FROM Books";

        using var connection = new DatabaseHelper().ConnectionToSQL();
        using var command = new SqlCommand(query, connection);

        try
        {
            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["BookID"]}, Title: {reader["Title"]}, Author: {reader["Author"]}, " +
                                  $"Genre: {reader["Genre"]}, ISBN: {reader["ISBN"]}, Year: {reader["PublishedYear"]}, Copies Available: {reader["CopiesAvailable"]}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void SearchBook()
    {
        Console.Write("Enter the ID for the book: ");
        int id = int.Parse(Console.ReadLine());

        var query = "SELECT * FROM Books WHERE BookID = @ID";

        using var connection = new DatabaseHelper().ConnectionToSQL();
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ID", id);

        try
        {
            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine($"ID: {reader["BookID"]}, Title: {reader["Title"]}, Author: {reader["Author"]}, " +
                                  $"Genre: {reader["Genre"]}, ISBN: {reader["ISBN"]}, Year: {reader["PublishedYear"]}, Copies Available: {reader["CopiesAvailable"]}");
            }
            else
            {
                Console.WriteLine("No book found with the provided ID.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void DeleteBook()
    {
        Console.Write("Enter the ID for the book you want to delete: ");
        int id = int.Parse(Console.ReadLine());

        var query = "DELETE FROM Books WHERE BookID = @ID";

        using var connection = new DatabaseHelper().ConnectionToSQL();
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ID", id);

        try
        {
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine(rowsAffected > 0 ? "The book was deleted successfully." : "Failed to delete the book.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
