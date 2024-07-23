namespace UnitOfWork.API.Controllers.V1
{
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var authors = _unitOfWork.Authors.GetAll();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var autohr = _unitOfWork.Authors.GetById(id);
            if (autohr == null)
                return NotFound();

            return Ok(autohr);
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var result = _unitOfWork.Authors.Find(x => x.Name == name);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Search")]
        public IActionResult SearchAuthors(string name)
        {
            var result = _unitOfWork.Authors.FindAll(x => x.Name.Contains(name));
            return Ok(result);
        }

        [HttpGet("OrderedByName")]
        public IActionResult SearchAuthorsWithOrdering(string name, bool? ascending)
        {
            var result = _unitOfWork.Authors.FindAllWithOrdering(
                x => x.Name.Contains(name),
                orderBy: x => x.Name,
                ascending: ascending ?? true);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            var result = _unitOfWork.Authors.Add(author);
            _unitOfWork.SaveChages();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Author author)
        {
            var existing = _unitOfWork.Authors.GetById(id);
            if (existing == null)
                return NotFound();

            existing.Name = author.Name;
            _unitOfWork.Authors.Update(existing);
            _unitOfWork.SaveChages();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _unitOfWork.Authors.GetById(id);
            if (existing == null)
                return NotFound();

            _unitOfWork.Authors.Delete(existing);

            _unitOfWork.SaveChages();
            return NoContent();
        }

    }
}
