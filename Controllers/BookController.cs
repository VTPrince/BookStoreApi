using capp.Models;
using capp.Services;
using Microsoft.AspNetCore.Mvc;

namespace capp.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase {
    public BookController(){

    }

    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetBooks([FromQuery] string query) {
        return await BookService.GetBooks(query);
    }


}