using Azure;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.WebApi.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
public class FileController : Controller
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("file/upload")]
    public async Task<ActionResult> Upload([FromBody] List<string> files)
    {
        try
        {
            var response = await _fileService.UploadFilesAsync(files);
            return response != null ? Ok() : BadRequest();
        }
        catch (RequestFailedException e)
        {
            return Problem(e.Message);
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }
}