using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DocumentTemplatesWebApp.Services;
using DocumentTemplatesWebApp.Models;

namespace DocumentTemplatesWebApp.Controllers;

[Route("api/doc")]
[ApiController]
public class DocumentController : ControllerBase {
    private readonly MSWordService _wordService;
    private readonly Settings _settings;
    public DocumentController(Settings settings, MSWordService msWordService)
    {
        _settings = settings;
        _wordService = msWordService;
    }

    [HttpGet("test")]
    public IActionResult Test() 
    {
        var result = _wordService.GetFileText("Test.docx");
        return Ok(result);
    }

    [HttpPost("word")]
    public async Task<IActionResult> GenerateDocument([FromBody] object request) 
    {
        if (request == null)
            return BadRequest("request body is null");

        try 
        {
            var data = JsonConvert.DeserializeObject<Request>(request.ToString());
            var filePath = await _wordService.GenerateDocument(data.Template, data.Content);
            
            var content = await System.IO.File.ReadAllBytesAsync(filePath);
            var contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            return File(content, contentType, Path.GetFileNameWithoutExtension(filePath));
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("templates")]
    public IActionResult GetTemplatesList() 
    {
        return Ok(_settings.Documents);
    }
}