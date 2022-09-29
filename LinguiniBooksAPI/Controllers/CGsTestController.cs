using DBDataAccess;
using DBDataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LinguiniBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CGsTestController : ControllerBase
    {
        private IBookRepository _bookRepository;

        public CGsTestController()
        {
            _bookRepository = new BookRepository(new MongoClient()); // connstr?
        }

        public CGsTestController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IMongoCollection<BookModel> Get()
        {
            return _bookRepository.GetAll();
        }








        // GET api/<CGsTestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CGsTestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CGsTestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CGsTestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
