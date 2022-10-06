using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentTemplatesWebApp.Services;

namespace DocumentTemplatesWebApp.Controllers;

[Route("api/doc")]
[ApiController]
public class DocumentController : ControllerBase {
    private readonly WordFileHandler wordFileHandler;
    private readonly Settings settings;

    public DocumentController(Settings settings) {
        this.settings = settings;
        wordFileHandler = new WordFileHandler(settings);
    }

    [HttpGet("test")]
    public IActionResult Test() {
        var result = wordFileHandler.GetFileContent("Test.docx");
        return Ok(result);
    }
}