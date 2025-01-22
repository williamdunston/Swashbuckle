using BookstoreAPI.Data;
using BookstoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BooksController : ControllerBase
    {
        private readonly BookRepository _bookRepository;

        public BooksController()
        {
            _bookRepository = new BookRepository();
        }

        // GET api/books
        /// <summary>
        /// Retrieve all books from the repository.
        /// </summary>
        /// <returns>Returns a list of all books</returns>
        /// <response code="200">Returns all the books in the repository</response>
        /// <response code="404">No books found in the repository</response>
        /// <response code="500">Internal Server Error reached</response>
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                             nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(_bookRepository.GetAllBooks());
        }

        // GET api/books/1
        /// <summary>
        /// Retrieve a book from the repository based on the book id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/books
        ///     {
        ///         "id": 1
        ///     }
        ///     
        /// </remarks>
        /// <returns>Returns a single books</returns>
        /// <response code="200">Returns the book in the repository base on the book id</response>
        /// <response code="404">No book with the specified id found in the repository</response>
        /// <response code="500">Internal Server Error reached</response>
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                             nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/books
        /// <summary>
        /// Add a book to the repository
        /// </summary>
        /// <param name="book">Book object</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/books
        ///     {
        ///         "Id" = 1,
        ///         "Stock" = 10, 
        ///         "Title" = "Book 1", 
        ///         "Author" = "Author 1", 
        ///         "Price" = $9.99
        ///     }
        ///     
        /// </remarks>
        /// <returns></returns>
        /// <response code="201">Created a new book</response>
        /// <response code="404">Could not create book with given parameters</response>
        /// <response code="500">Internal Server Error reached</response>
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                                     nameof(DefaultApiConventions.Post))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult AddBook([FromBody] Book book)
        {
            _bookRepository.AddBook(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        // PUT api/books/1
        /// <summary>
        /// Retrieve all books
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/books
        ///     {
        ///         "Id" = 1,
        ///         {
        ///             "Stock" = 12, 
        ///             "Title" = "Book 1", 
        ///             "Author" = "Author 1", 
        ///             "Price" = $9.99
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <returns></returns>
        /// <response code="204">Updated existing book</response>
        /// <response code="404">Could not update book with given parameters</response>
        /// <response code="500">Internal Server Error reached</response>
        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                             nameof(DefaultApiConventions.Put))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            var existingBook = _bookRepository.GetBookById(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            _bookRepository.UpdateBook(book);
            return NoContent();
        }

        // DELETE api/books/1
        /// <summary>
        /// Deletes a book from the repository based on the book id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELTE api/books
        ///     {
        ///         "Id" = 1
        ///     }
        ///     
        /// </remarks>
        /// <returns></returns>
        /// <response code="204">Deleted existing book</response>
        /// <response code="404">Could not delete book with given parameters"</response>
        /// <response code="500">Internal Server Error reached</response>
        [HttpDelete("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                             nameof(DefaultApiConventions.Delete))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteBook(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            _bookRepository.DeleteBook(id);
            return NoContent();
        }
    }
}