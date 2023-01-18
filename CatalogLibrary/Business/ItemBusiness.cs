using CatalogLibrary.Data;
using CatalogLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLibrary.Business;

public class ItemBusiness
{
    ItemData itemData;

    public ItemBusiness()
    {
        itemData = new ItemData();
    }

    public List<Item> Get()
    {
        List<Item> foundItems = itemData.GetList().OrderBy(x => x.Id).ToList();
        return foundItems;
    }

    public List<Item> Get(int CurrentPage, int PageSize, out int TotalCount)
    {
        List<Item> foundItems = itemData.GetList().OrderBy(x => x.Id).ToList();
        TotalCount = foundItems.Count;
        return foundItems.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
    }

    public Item Get(string Name)
    {
        return itemData.Get(Name);
    }

    public Item Get(int Id)
    {
        return itemData.Get(Id);
    }


    public bool Add(Item Item)
    {
        return itemData.Add(Item);
    }

    public bool Update(Item Item)
    {
        return itemData.Update(Item);
    }

    public bool Delete(int Id)
    {
        return itemData.Delete(Id);
    }
}
