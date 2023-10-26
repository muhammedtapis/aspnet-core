using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.ActionResults;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using System;

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
            try
            {
                var newPublisher = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher); // created methodu oluşturulan objeyi ve method ismini alıyor.
                                                                    // return ok yerine return created yapmamızın sebebi yeni bir resource oluştuğu için.
            }
            catch (PublisherNameException ex) //yeni oluşturduğumuz exception classını çağırdık.
            {
                return BadRequest($"{ex.Message}, Publisher name :{ex.PublisherName}"); //bu parametreler publishernameexception sınıfındaki constructorlardan en alttakinden geliyor.
                //servisteki exception methodunun ilk parametresi  , methodun ikinci parametresi   !!!publisher serviste throw edildi bu hata burada yakalayıp kullanıcıya gösteriyoruz.
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           

        }

        [HttpGet("get-publisher-books-with-authors/{id}")]

        public IActionResult GetPublisherData(int id) 
        {
            var _response = _publishersService.GetPublisherData(id);
            return  Ok(_response);
        }


        [HttpGet("get-publisher-by-id/{id}")]

        public IActionResult GetPublisherById(int id) //changing return type to Publisher and change back again.
                                                                //return type ActionResult<T> olursa hem http status code hem de publisher aynı anda gönderiliyor.
                                                                //iki return typeta da sıkıntı çıkmıyor
                                                                //en son CustomActionREsult yaptık
        {
           
            var _response = _publishersService.GetPublisherById(id);

            if(_response != null)
            {
                return Ok(_response); //return type IActionResult olduğu için değiştirdik tekrar
              //  return _response; //return type Publisher olduğu için _response olarak değişti. Return type generic ActionResult<T> ise de çalışıyor.
                //var _responseObj = new CustomActionResultVM()
                //{
                //    Publisher = _response
                //};

                //return new CustomActionResult(_responseObj);
            }
            else
            {
                 return NotFound(); //return type ActionResult<T> olduğu için iki türlü de oluo.
                // return null;

                //var _responseObj = new CustomActionResultVM()
                //{
                //    Exception = new Exception("this is coming from publishers controller")
                //};

                //return new CustomActionResult(_responseObj);
            }
            
        }

        [HttpDelete("delete-publisher-by-id/{id}")]

        public IActionResult DeletePublisherById(int id) 
        {
            try
            {
                _publishersService.DeletePublisherById(id);

                return Ok();
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
