using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.WebApi.Controllers;

[AllowAnonymous]
public class FileController : Controller
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("file/upload")]
    public ActionResult Index()
    {
        var response = _fileService.UploadFilesAsync();
        return response != null ? Ok() : BadRequest();
    }
}