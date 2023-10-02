using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        //API endpoint oluşturman lazım data ile işlem yapabilmek için , bunun içinse önce servisi inject etmen gerekiyor constructor ile.
        private AuthorsService _authorsService;

        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        //ilk endpointi oluşturacağız 
        //HTTP POST API endpoint olacak çünkü database veri gönderiyoruz.
        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            _authorsService.AddAuthor(author);
            return Ok();

        }
    }
}
