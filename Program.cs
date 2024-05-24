using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;

class Program
{
    static void Main()
    {
        string inputFilePath = "input.xml";
        string sortedXmlFilePath = "sorted_books.xml";
        string textFilePath = "books.txt";

        
        List<Book> books = DeserializeBooks(inputFilePath);

        
        var sortedBooks = books.OrderBy(book => book.Year).ToList();

        
        SerializeBooks(sortedBooks, sortedXmlFilePath);

        
        WriteBooksToTextFile(sortedBooks, textFilePath);

        Console.WriteLine("Операція завершена успішно.");
    }

    static List<Book> DeserializeBooks(string filePath)
    {
        XElement booksXml = XElement.Load(filePath);
        var books = booksXml.Elements("Book")
                            .Select(book => new Book
                            {
                                Title = book.Element("Title")?.Value,
                                Author = book.Element("Author")?.Value,
                                Year = int.Parse(book.Element("Year")?.Value)
                            }).ToList();
        return books;
    }

    static void SerializeBooks(List<Book> books, string filePath)
    {
        XElement booksXml = new XElement("Books",
            books.Select(book => new XElement("Book",
                new XElement("Title", book.Title),
                new XElement("Author", book.Author),
                new XElement("Year", book.Year)
            ))
        );
        booksXml.Save(filePath);
    }

    static void WriteBooksToTextFile(List<Book> books, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var book in books)
            {
                writer.WriteLine($"Title: {book.Title}");
                writer.WriteLine($"Author: {book.Author}");
                writer.WriteLine($"Year: {book.Year}");
                writer.WriteLine();
            }
        }
    }
}

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
}
