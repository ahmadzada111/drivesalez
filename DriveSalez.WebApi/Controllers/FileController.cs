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
    public ActionResult Upload()
    {
        var response = _fileService.UploadFilesAsync();
        return response != null ? Ok() : BadRequest();
    }
}