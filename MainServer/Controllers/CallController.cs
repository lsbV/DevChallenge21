using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CallComponent;
using Core;
using MainServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MainServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CallController(ICallService service, IMapper mapper, HttpClient httpClient) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(CreateCallRequestDto request)
    {
        var audio = await httpClient.GetByteArrayAsync(request.Audio_Url);
        var callId = await service.ProcessCallAsync(new Audio([.. audio]));
        return Ok(new CreateCallResponseDto() { Id = callId.Value });
    }

    [HttpPost("FromLocalFile")]
    public async Task<IActionResult> FromLocalFile(CreateCallFromLocalFileRequestDto request)
    {
        var audio = await System.IO.File.ReadAllBytesAsync(request.LocalPath);
        var callId = await service.ProcessCallAsync(new Audio([.. audio]));
        return Ok(new CreateCallResponseDto() { Id = callId.Value });
    }

    [HttpPost("AudioInBody")]
    public async Task<IActionResult> AudioInBody([FromForm] IFormFile audioFile)
    {
        if (audioFile.Length == 0)
        {
            return BadRequest("Audio file is not provided or is empty.");
        }

        using var memoryStream = new MemoryStream();
        await audioFile.CopyToAsync(memoryStream);
        var audioBytes = memoryStream.ToArray();
        var callId = await service.ProcessCallAsync(new Audio([.. audioBytes]));
        return Ok(new CreateCallResponseDto() { Id = callId.Value });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var callId = new CallId(id);
        var call = await service.GetCallByIdAsync(callId);
        var callDto = mapper.Map<CallResponseDto>(call);
        return Ok(call);
    }

}