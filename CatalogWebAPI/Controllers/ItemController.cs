using AutoMapper;
using CatalogLibrary.Business;
using CatalogLibrary.Entity;
using CatalogLibrary.ViewModel;
using Microsoft.AspNetCore.Mvc;
using static CatalogWebAPI.Controllers.CategoryController;

namespace CatalogWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly ILogger<ItemController> _logger;
    private readonly IMapper _mapper;
    ItemBusiness itemBusiness;

    public ItemController(ILogger<ItemController> logger, IMapper Mapper)
    {
        _logger = logger;
        _mapper = Mapper;
        itemBusiness = new ItemBusiness();
    }

    #region Request & Response

    public class ItemRequest
    {
        public ItemRequest()
        {
            this.Name = String.Empty;
            this.Description = String.Empty;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }

    #endregion


    [HttpGet(Name = "GetItems")]
    public IEnumerable<ItemViewModel> Get()
    {
        IEnumerable<Item> items = itemBusiness.Get();
        IEnumerable<ItemViewModel> itemViewModel = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(items);

        return itemViewModel;
    }

    // TODO: When I uncomment this method swagger fails to display. I will return this problem later 
    //[HttpGet(Name = "GetItemsPaged")]
    //public IEnumerable<ItemViewModel> GetPaged(int PageNumber = 1, int PageSize = 20)
    //{
    //    int TotalCount = 0;
    //    IEnumerable<Item> items = itemBusiness.Get(PageNumber, PageSize, out TotalCount);
    //    IEnumerable<ItemViewModel> itemViewModel = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(items);

    //    return itemViewModel;
    //}

    [HttpGet("{id}")]
    public ItemViewModel Get(int id)
    {
        Item item = itemBusiness.Get(id);
        ItemViewModel itemViewModel = _mapper.Map<Item, ItemViewModel>(item);

        return itemViewModel;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ItemViewModel> Post([FromBody] string name)
    {
        if (String.IsNullOrEmpty(name))
        {
            return BadRequest("Name value is required.");
        }

        Item item = itemBusiness.Get(name);

        if (item == null)
        {
            item = new Item
            {
                Name = name,
                Date = DateTime.Now,
                Description = "Some description."
            };
        }

        itemBusiness.Add(item);
        ItemViewModel itemViewModel = _mapper.Map<Item, ItemViewModel>(item);

        return CreatedAtAction(nameof(Post), itemViewModel);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Put(int id, [FromBody] ItemRequest ItemRequest)
    {
        if (String.IsNullOrEmpty(ItemRequest.Name))
        {
            return BadRequest("Name value is required.");
        }

        Item item = itemBusiness.Get(id);

        if (item == null)
        {
            return NotFound($"Item Id '{id}' not found.");
        }

        item.Name = ItemRequest.Name;
        item.Description = ItemRequest.Description;
        itemBusiness.Update(item);

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

        Item item = itemBusiness.Get(id);

        if (item == null)
        {
            return NotFound($"Item Id '{id}' not found.");
        }

        itemBusiness.Delete(id);

        return Ok();
    }
}