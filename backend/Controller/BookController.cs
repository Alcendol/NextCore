using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using NextCore.backend.Dtos;
using System.Text.RegularExpressions;
using NextCore.backend.Repositories;
using NextCore.backend.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using NextCore.backend.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;
[Route("api/book")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IBookRepository _bookRepo;
    private readonly ILogger<BookController> _logger;
    private const string UploadsFolder = "uploads";
    public List<BookResponseDTO> BooksList { get; set; } = new List<BookResponseDTO>();

    // Constructor with ILogger dependency injection
    public BookController(IConfiguration configuration, ILogger<BookController> logger, IBookRepository bookRepo)
    {
        _configuration = configuration;
        _logger = logger;
        _bookRepo = bookRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookResponseDTO>>> Index(){
        var books = await _bookRepo.GetAll();
        if (books == null){
            return NotFound();
        }
        return Ok(books);
    }

    [HttpGet("by-id/{bookId}")]
    public async Task<ActionResult<BookResponseDTO>> getBookById(string bookId){
        var book = await _bookRepo.GetBookById(bookId);
        if (book == null){
            return NotFound();
        }
        return Ok(book);
    }
    

    [HttpGet("by-author/{authorId}")]
    public async Task<ActionResult<List<BookResponseDTO>>> getBooksByAuthorId(string authorId){
        var books = await _bookRepo.GetBooksByAuthorId(authorId);
        if (books == null){
            return NotFound();
        }
        return Ok(books);
    }


    [HttpGet("by-publisher/{publisherId}")]
    public async Task<ActionResult<List<BookResponseDTO>>> getBooksByPublisherId(string publisherId){
        var books = await _bookRepo.GetBooksByPublisherId(publisherId);
        if (books == null){
            return NotFound();
        }
        return Ok(books);
    }

    [HttpGet("by-genre/{genreId}")]
    public async Task<ActionResult<List<BookResponseDTO>>> getBooksByGenreId(string genreId){
        var books = await _bookRepo.GetBooksByPublisherId(genreId);
        if (books == null){
            return NotFound();
        }
        return Ok(books);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddBook([FromForm] BookRequestDTO book){
        int row = await _bookRepo.AddBook(book);
        if (row == 0){
            return BadRequest("Failed while creating book");
        }
        return CreatedAtAction(nameof(AddBook), new {id = book.bookId});
    }   


    // [HttpPost("multiple")]
    // public IActionResult AddMultipleBooks(List<Book> bookList)
    // {
    //     _logger.LogDebug("Adding multiple books to the library.");

    //     foreach (var book in bookList)
    //     {
    //         book.country ??= "";
    //         book.language ??= "";
    //     }

    //     try
    //     {
    //         var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
    //         using (var connection = new MySqlConnection(connectionString))
    //         {
    //             connection.Open();

    //             string query = @"INSERT INTO books 
    //                             (bookId, title, datePublished, totalPage, country, language, genre, description) 
    //                             VALUES 
    //                             (@bookId, @title, @datePublished, @totalPage, @country, @language, @genre, @desc)";
                                
    //             using (var command = new MySqlCommand(query, connection))
    //             {
    //                 foreach (var book in bookList)
    //                 {
    //                     command.Parameters.Clear();
    //                     command.Parameters.AddWithValue("@bookId", book.bookId);
    //                     command.Parameters.AddWithValue("@title", book.title);
    //                     command.Parameters.AddWithValue("@datePublished", book.datePublished);
    //                     command.Parameters.AddWithValue("@totalPage", book.totalPage);
    //                     command.Parameters.AddWithValue("@country", book.country);
    //                     command.Parameters.AddWithValue("@language", book.language);
    //                     command.Parameters.AddWithValue("@genre", book.genre);
    //                     command.Parameters.AddWithValue("@desc", book.desc);

    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //         }

    //         _logger.LogDebug("Books successfully added.");
    //         return Ok(bookList);
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, "Error occurred while adding multiple books.");
    //         return StatusCode(500, "Internal server error.");
    //     }
    // }

}
