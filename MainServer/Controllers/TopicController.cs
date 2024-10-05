using System.Collections.Immutable;
using AutoMapper;
using TopicComponent;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MainServer.Models;

namespace MainServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicController(ITopicService service, IMapper mapper) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await service.GetCategoriesAsync();
        var topicDtos = categories.Select(mapper.Map<TopicResponseDto>);
        return Ok(topicDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var topicId = new TopicId(id);
        var topic = await service.GetTopicAsync(topicId);
        var topicDto = mapper.Map<TopicResponseDto>(topic);
        return Ok(topicDto);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateTopicRequestDto requestDto)
    {
        var topic = mapper.Map<Topic>(requestDto);
        var createdTopic = await service.CreateTopicAsync(topic);
        return CreatedAtAction(string.Empty, createdTopic);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(UpdateTopicRequestDto requestDto, int id)
    {
        var topicId = new TopicId(id);
        var topicFromService = await service.GetTopicAsync(topicId);
        var topic = new Topic(
            topicId,
            string.IsNullOrEmpty(requestDto.Title) ? topicFromService.Title : new Title(requestDto.Title),
            requestDto.Points.Length != 0 ? requestDto.Points.Select(p => new Point(p)).ToImmutableHashSet() : topicFromService.Points,
            topicFromService.CallId
        );

        var updatedTopic = await service.UpdateTopicAsync(topic);
        var updatedTopicDto = mapper.Map<TopicResponseDto>(updatedTopic);
        return Ok(updatedTopicDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedTopic = await service.DeleteTopicAsync(new TopicId(id));
        var deletedTopicDto = mapper.Map<TopicResponseDto>(deletedTopic);
        return Ok(deletedTopicDto);
    }



}