using AutoMapper;
using CatalogLibrary.Business;
using CatalogLibrary.Entity;
using CatalogLibrary.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace CatalogWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly IMapper _mapper;
    CategoryBusiness categoryBusiness;

    public CategoryController(ILogger<CategoryController> logger, IMapper Mapper)
    {
        _logger = logger;
        _mapper = Mapper;
        categoryBusiness = new CategoryBusiness();
    }

    #region Request & Response

    public class CategoryRequest
    {
        public CategoryRequest()
        {
            this.Name = String.Empty;
            this.Description = String.Empty;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }

    #endregion

    [HttpGet(Name = "GetCategories")]
    public IEnumerable<CategoryViewModel> Get()
    {
        IEnumerable<Category> categories = categoryBusiness.Get();
        IEnumerable<CategoryViewModel> categoryViewModel = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);

        return categoryViewModel;
    }

    [HttpGet("{id}")]
    public CategoryViewModel Get(int id)
    {
        Category category = categoryBusiness.Get(id);
        CategoryViewModel categoryViewModel = _mapper.Map<Category, CategoryViewModel>(category);

        return categoryViewModel;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CategoryViewModel> Post([FromBody] string name)
    {
        if (String.IsNullOrEmpty(name))
        {
            return BadRequest("Name value is required.");
        }

        Category category = categoryBusiness.Get(name);

        if (category == null)
        {
            category = new Category
            {
                Name = name,
                Date = DateTime.Now,
                Description = "Some description."
            };
        }

        categoryBusiness.Add(category);
        CategoryViewModel categoryViewModel = _mapper.Map<Category, CategoryViewModel>(category);

        return CreatedAtAction(nameof(Post), categoryViewModel);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Put(int id, [FromBody] CategoryRequest CategoryRequest)
    {
        if (String.IsNullOrEmpty(CategoryRequest.Name))
        {
            return BadRequest("Name value is required.");
        }

        Category category = categoryBusiness.Get(id);

        if (category == null)
        {
            return NotFound($"Category Id '{id}' not found.");
        }

        category.Name = CategoryRequest.Name;
        category.Description = CategoryRequest.Description;
        categoryBusiness.Update(category);

        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id value is required.");
        }

        Category category = categoryBusiness.Get(id);

        if (category == null)
        {
            return NotFound($"Category id '{id}' not found.");
        }

        categoryBusiness.Delete(id);

        return Ok();
    }
}