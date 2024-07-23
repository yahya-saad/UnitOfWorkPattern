namespace UnitOfWork.API.Controllers.V1
{
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _unitOfWork.Books.GetAll();
            return Ok(books);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _unitOfWork.Books.GetById(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpGet("GetByTitle")]
        public IActionResult GetByTitle(string title)
        {
            var result = _unitOfWork.Books.Find(x => x.Title == title, [nameof(Book.Author)]);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Search")]
        public IActionResult SearchBooks(string title)
        {
            var result = _unitOfWork.Books.FindAll(x => x.Title.Contains(title), includes: [nameof(Book.Author)]);
            return Ok(result);
        }

        [HttpGet("OrderedByTitle")]
        public IActionResult SearchBooksWithOrdering(string title, bool? ascending)
        {
            var result = _unitOfWork.Books.FindAllWithOrdering(
                x => x.Title.Contains(title),
                orderBy: x => x.Title,
                ascending: ascending ?? true,
                includes: [nameof(Book.Author)]);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            var existingAuthor = _unitOfWork.Authors.GetById(book.AuthorId);
            if (existingAuthor == null)
                return NotFound(new { message = $"Author with id {book.AuthorId} not found" });

            var result = _unitOfWork.Books.Add(book);
            _unitOfWork.SaveChages();
            return Ok(result);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, Book book)
        {
            var existing = _unitOfWork.Books.GetById(id);
            if (existing == null)
                return NotFound();

            existing.Title = book.Title;
            existing.AuthorId = book.AuthorId;
            existing.Description = book.Description;
            _unitOfWork.Books.Update(existing);

            _unitOfWork.SaveChages();

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _unitOfWork.Books.GetById(id);
            if (existing == null)
                return NotFound();

            _unitOfWork.Books.Delete(existing);
            _unitOfWork.SaveChages();

            return NoContent();
        }


    }
}
