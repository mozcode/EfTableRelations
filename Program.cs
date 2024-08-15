using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EfTableRelations.Data;
using EfTableRelations.Services;

class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("YourConnectionStringHere"));
        serviceCollection.AddTransient<TableRelationService>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var tableRelationService = serviceProvider.GetService<TableRelationService>();
        var relations = tableRelationService.GetTableRelations();

        foreach (var relation in relations)
        {
            Console.WriteLine($"Table: {relation.TableName}, Related Table: {relation.RelatedTableName}, Relation Type: {relation.RelationType}");
        }
    }
}
