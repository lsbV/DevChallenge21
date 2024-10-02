using System.Collections.Immutable;
using AutoMapper;
using CategoryComponent;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MainServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService service, IMapper mapper) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await service.GetCategoriesAsync();
        var categoryDtos = categories.Select(mapper.Map<CategoryResponseDTO>);
        return Ok(categoryDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var categoryId = new CategoryId(id);
        var category = await service.GetCategoryAsync(categoryId);
        var categoryDto = mapper.Map<CategoryResponseDTO>(category);
        return Ok(categoryDto);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateCategoryRequestDTO requestDto)
    {
        var category = mapper.Map<Category>(requestDto);
        var createdCategory = await service.CreateCategoryAsync(category);
        return CreatedAtAction(string.Empty, createdCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(UpdateCategoryRequestDTO requestDto, int id)
    {
        var categoryId = new CategoryId(id);
        var categoryFromService = await service.GetCategoryAsync(categoryId);
        var category = new Category(
            categoryId,
            string.IsNullOrEmpty(requestDto.Title) ? categoryFromService.Title : new Title(requestDto.Title),
            requestDto.Points.Length != 0 ? requestDto.Points.Select(p => new Point(p)).ToImmutableHashSet() : categoryFromService.Points
        );

        var updatedCategory = await service.UpdateCategoryAsync(category);
        var updatedCategoryDto = mapper.Map<CategoryResponseDTO>(updatedCategory);
        return Ok(updatedCategoryDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedCategory = await service.DeleteCategoryAsync(new CategoryId(id));
        var deletedCategoryDto = mapper.Map<CategoryResponseDTO>(deletedCategory);
        return Ok(deletedCategoryDto);
    }



}