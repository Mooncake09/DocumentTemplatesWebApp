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
    private readonly string _fileContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
    public DocumentController(Settings settings, MSWordService msWordService)
    {
        _settings = settings;
        _wordService = msWordService;
    }
    /// <summary>
    /// Запрос на генерацию нового документа
    /// </summary>
    /// <param name="request">json, содержащий название шаблона и значения для генерации нового документа</param>
    /// <returns>возвращает полученный в ходе генерации файл</returns>
    [HttpPost("word")]
    public async Task<IActionResult> GenerateDocument([FromBody] object request) 
    {
        if (request == null)
            return BadRequest("request body is null");

        try 
        {
            var data = JsonConvert.DeserializeObject<Request>(request.ToString());
            var fileName = await _wordService.GenerateDocument(data.Template, data.Content);
            
            var content = await GetFileAsync(fileName);
            return File(content, _fileContentType, fileName);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// Получить список всех доступных шаблнов
    /// </summary>
    /// <returns></returns>
    [HttpGet("templates")]
    public IActionResult GetTemplatesList() 
    {
        return Ok(_settings.Documents);
    }

    /// <summary>
    /// Получить список всех сохраненных файлов
    /// </summary>
    /// <returns></returns>
    [HttpGet("savedFiles")]
    public IActionResult GetSavedFilesList() 
    {
        return Ok(_wordService.GetSavedFilesList());
    }
    /// <summary>
    /// Загрузить файл
    /// </summary>
    /// <param name="fileName">имя файла</param>
    /// <returns>файл fileName</returns>
    [HttpGet("savedFile/{fileName}")]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        try 
        {
            var content = await GetFileAsync(fileName);
            return File(content, _fileContentType, fileName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, e.Message);
        }
        
    }

    private async Task<byte[]> GetFileAsync(string fileName) 
    {
        var filePath = Path.Combine(_settings.SavedFilesDirPath, $"{fileName}.docx");
        if (System.IO.File.Exists(filePath)) {
            var content = await System.IO.File.ReadAllBytesAsync(filePath);
            return content;
        }
        throw new FileNotFoundException($"file with name {fileName} does not exsist");
    } 

}