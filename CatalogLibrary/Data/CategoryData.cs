using CatalogLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLibrary.Data;

internal class CategoryData
{
    private static readonly string[] Names = new[]
    {
        "Musical", "Adventure", "Documentary", "Science", "Fiction", "Western", "Nature"
    };

    private static readonly string[] Descriptions = new[]
    {
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin volutpat velit sit amet scelerisque ornare. ",
        "Vestibulum tincidunt dapibus diam quis imperdiet. Suspendisse porta orci quis velit mollis, pretium tempus risus semper. ",
        "Ut vehicula felis nec neque auctor tempus. Pellentesque magna turpis, venenatis vitae varius non, posuere sit amet risus. ",
        "Morbi mollis mi in lectus luctus rutrum. Sed nisi neque, sodales eu tellus at, vestibulum blandit sapien. Sed pellentesque est non aliquet malesuada. ",
        "Duis venenatis, sapien ac volutpat molestie, lectus orci maximus turpis, et vulputate odio augue eu libero. ",
        "Nullam nunc nisi, accumsan quis justo at, convallis mollis mi. ",
        "Nam fringilla, neque et blandit commodo, quam enim tempus diam, at efficitur lorem erat nec dui.",
        "Vivamus ultrices vestibulum aliquet. Proin erat enim, sollicitudin vel nulla quis, egestas eleifend leo. ",
        "Duis viverra urna ligula, nec tincidunt augue condimentum quis. Mauris at lobortis nisl. Duis bibendum ultricies consectetur. ",
        "Ut tristique sollicitudin dui vehicula rutrum. Etiam ex leo, mattis eu mollis non, dignissim faucibus nisl. Nulla vel orci vitae sapien maximus porttitor sed sed magna. "
    };

    public IEnumerable<Category> Get()
    {
        // Here, we are creating and returning some dummy list.
        return Enumerable.Range(1, 5).Select(index => new Category
        {
            Id = Random.Shared.Next(1, 100),
            Name = Names[Random.Shared.Next(Names.Length)],
            Date = DateTime.Now.AddDays(index),
            Description = Descriptions[Random.Shared.Next(Descriptions.Length)]
        })
        .OrderBy(x => x.Id)
        .ToArray();
    }

    public Category Get(int Id)
    {
        // Here, we are creating and returning some dummy category. Actually not searching in the names array.

        var someRandomNumber = Random.Shared.Next(1, 100);

        var category = new Category
        {
            Id = Id,
            Name = Names[Random.Shared.Next(Names.Length)],
            Date = DateTime.Now.AddDays(someRandomNumber),
            Description = Descriptions[Random.Shared.Next(Descriptions.Length)]
        };

        return category;
    }

    public Category Get(string Name)
    {
        // Here, we are creating and returning some dummy category. Actually not searching in the names array.

        var someRandomNumber = Random.Shared.Next(1, 100);

        var category = new Category
        {
            Id = someRandomNumber,
            Name = Name,
            Date = DateTime.Now.AddDays(someRandomNumber),
            Description = Descriptions[Random.Shared.Next(Descriptions.Length)]
        };

        return category;
    }

    public bool Add(Category Category)
    {
        // Here, we are acting like we added category.
        return true;
    }

    public bool Update(Category Category)
    {
        // Here, we are acting like we updated category.
        return true;
    }

    public bool Delete(int Id)
    {
        // Here, we are acting like we deleted category.
        return true;
    }
}
