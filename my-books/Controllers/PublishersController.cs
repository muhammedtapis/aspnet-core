using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _publishersService;
        //API endpoint oluşturman lazım data ile işlem yapabilmek için , bunun içinse önce SERVİSİ inject etmen gerekiyor constructor ile.
        //asıl ekle çıkarma methodları servis sınıfında.
        //eğer kullanıcıdan veri isteniyosa View Model oluşturman gerek.

        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        //ilk endpointi oluşturacağız 
        //HTTP POST API endpoint olacak çünkü database veri gönderiyoruz.
        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher) //FromBody html bodyden gelen publisher name entrysi için.
        {
            _publishersService.AddPublisher(publisher);
            return Ok();

        }

        [HttpGet("get-publisher-books-with-authors/{id}")]

        public IActionResult GetPublisherData(int id) 
        {
            var _response = _publishersService.GetPublisherData(id);
            return  Ok(_response);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]

        public IActionResult DeletePublisherById(int id) 
        {
            _publishersService.DeletePublisherById(id);

            return Ok();
        }
    }
}
