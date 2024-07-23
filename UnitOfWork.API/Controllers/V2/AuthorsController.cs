namespace UnitOfWork.API.Controllers.V2
{
    [ApiController]
    [ApiVersion(2)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var autohr = await _unitOfWork.Authors.GetByIdAsync(id);
            if (autohr == null)
                return NotFound();

            return Ok(autohr);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var result = await _unitOfWork.Authors.FindAsync(x => x.Name == name);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchAuthorsAsync(string name, int? take, int? skip)
        {
            var result = await _unitOfWork.Authors.FindAllAsync(
                x => x.Name.Contains(name),
                skip,
                take
                );
            return Ok(result);
        }

        [HttpGet("OrderedByName")]
        public async Task<IActionResult> SearchAuthorsWithOrderingAsync(string name, bool? ascending)
        {
            var result = await _unitOfWork.Authors.FindAllWithOrderingAsync(
                x => x.Name.Contains(name),
                orderBy: x => x.Name,
                ascending: ascending ?? true);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Author author)
        {
            var result = await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.SaveChagesAsync();
            return Ok(result);
        }
    }
}
