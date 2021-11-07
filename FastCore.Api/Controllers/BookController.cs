using FastCore.Application;
using FastCore.Application.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FastCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookApplication _bookApplication;

        public BookController(ILogger<BookController> logger, IBookApplication bookApplication)
        {
            _logger = logger;
            _bookApplication = bookApplication;
        }

        /// <summary>
        /// Adiciona um livro novo.
        /// </summary>
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] BookVm entity)
        {
            await _bookApplication.AddAsync(entity);
            return Ok();
        }

        /// <summary>
        /// Atualiza um livro existente.
        /// </summary>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] BookVm entity)
        {
            await _bookApplication.UpdateAsync(entity);
            return Ok();
        }

        /// <summary>
        /// Apaga um livro existente.
        /// </summary>
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] BookVm entity)
        {
            await _bookApplication.DeleteAsync(entity);
            return Ok();
        }

        /// <summary>
        /// Obtem todos os livros cadastrados.
        /// </summary>
        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _bookApplication.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtem um livro específico.
        /// </summary>
        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetItemAsyncAsync([FromBody] int bookId)
        {
            var result = await _bookApplication.GetItemAsync(bookId);
            return Ok(result);
        }
    }
}
