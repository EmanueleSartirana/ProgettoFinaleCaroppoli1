using Microsoft.AspNetCore.Mvc;
using ProgettoFinaleCaroppoli1.Models;
using ProgettoFinaleCaroppoli1.Services;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly FileStorageService _fileStorage;
    private readonly CsvParserService _csvParser;

    public UploadController(FileStorageService fileStorage, CsvParserService csvParser)
    {
        _fileStorage = fileStorage;
        _csvParser = csvParser;
    }

    [HttpPost("csv")]
    public async Task<IActionResult> UploadCsv(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new UploadResult
            {
                NomeFile = file?.FileName ?? string.Empty,
                Dimensione = file?.Length ?? 0,
                Messaggio = "File non valido",
                Corso = new List<Corso>()
            });
        }

        try
        {
            var path = await _fileStorage.SaveFileAsync(file);
            var corsi = await _csvParser.ParseAsync(file.OpenReadStream());

            var result = new UploadResult
            {
                NomeFile = file.FileName,
                Dimensione = file.Length,
                Messaggio = "OK",
                Corso = corsi
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new UploadResult
            {
                NomeFile = file?.FileName ?? string.Empty,
                Dimensione = file?.Length ?? 0,
                Messaggio = ex.Message,
                Corso = new List<Corso>()
            });
        }
    }
}