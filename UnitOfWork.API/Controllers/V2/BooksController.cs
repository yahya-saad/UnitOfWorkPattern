namespace UnitOfWork.API.Controllers.V2
{
    [ApiController]
    [ApiVersion(2)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            return Ok(books);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpGet("GetByTitle")]
        public async Task<IActionResult> GetByTitleAsync(string title)
        {
            var result = await _unitOfWork.Books.FindAsync(x => x.Title == title, [nameof(Book.Author)]);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchBooksAsync(string title, int? skip, int? take)
        {
            var result = await _unitOfWork.Books.FindAllAsync(
                x => x.Title.Contains(title),
                skip,
                take,
                [nameof(Book.Author)]);
            return Ok(result);
        }

        [HttpGet("OrderedByTitle")]
        public async Task<IActionResult> SearchBooksWithOrderingAsync(string title, bool? ascending)
        {
            var result = await _unitOfWork.Books.FindAllWithOrderingAsync(
                x => x.Title.Contains(title),
                orderBy: x => x.Title,
                ascending: ascending ?? true,
                includes: [nameof(Book.Author)]);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Book book)
        {
            var existingAuthor = await _unitOfWork.Authors.GetByIdAsync(book.AuthorId);
            if (existingAuthor == null)
                return NotFound(new { message = $"Author with id {book.AuthorId} not found" });

            var result = await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.SaveChagesAsync();
            return Ok(result);
        }
    }
}
