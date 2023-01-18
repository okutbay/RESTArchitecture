using CatalogLibrary.Data;
using CatalogLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLibrary.Business;

public class CategoryBusiness
{
    CategoryData categoryData;

    public CategoryBusiness()
    {
        categoryData = new CategoryData();
    }

    public IEnumerable<Category> Get()
    {
        return categoryData.Get();
    }

    public Category Get(string Name)
    {
        return categoryData.Get(Name);
    }

    public Category Get(int Id)
    {
        return categoryData.Get(Id);
    }

    public bool Add(Category Category)
    {
        return categoryData.Add(Category);
    }

    public bool Update(Category Category)
    {
        return categoryData.Update(Category);
    }

    public bool Delete(int Id)
    {
        return categoryData.Delete(Id);
    }
}
