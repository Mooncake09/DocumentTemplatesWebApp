using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DocumentTemplatesWebApp.Services;
using DocumentTemplatesWebApp.Models;

namespace DocumentTemplatesWebApp.Controllers;

[Route("api/doc")]
[ApiController]
public class DocumentController : ControllerBase {
    private readonly WordFileHandler wordFileHandler;
    private readonly Settings settings;

    public DocumentController(Settings settings)
    {
        this.settings = settings;
        wordFileHandler = new WordFileHandler(settings);
    }

    [HttpGet("test")]
    public IActionResult Test() 
    {
        var result = wordFileHandler.GetFileText("Test.docx");
        return Ok(result);
    }

    [HttpPost("word")]
    public IActionResult GenerateDocument([FromBody] object request) 
    {
        if (request == null)
            return BadRequest("request body is null");

        var data = JsonConvert.DeserializeObject<RequestModel>(request.ToString());
        // wordFileHandler.GenerateDocument("Test.docx", "some content");
        return Ok();
    }
}