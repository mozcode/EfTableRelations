using System.Collections.Generic;
using System.Linq;
using EfTableRelations.Entities;
using Microsoft.EntityFrameworkCore;
using EfTableRelations.Data;

namespace EfTableRelations.Services;

public class TableRelationService
{
    private readonly AppDbContext _context;

    public TableRelationService(AppDbContext context)
    {
        _context = context;
    }

    public List<EntityRelationDto> GetTableRelations()
    {
        var relations = new List<EntityRelationDto>();

        var model = _context.Model;
        foreach (var entityType in model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();

            foreach (var foreignKey in entityType.GetForeignKeys())
            {
                var relatedTable = foreignKey.PrincipalEntityType.GetTableName();
                var relationType = foreignKey.IsUnique ? "OneToOne" : "OneToMany";
                var tableKey = string.Join(", ", foreignKey.Properties.Select(p => p.Name));
                var foreignKeyName = string.Join(", ", foreignKey.PrincipalKey.Properties.Select(p => p.Name));
                var relatedTableKey = string.Join(", ", foreignKey.PrincipalKey.Properties.Select(p => p.Name));

                var relation = new EntityRelationDto
                {
                    TableName = tableName,
                    RelatedTableName = relatedTable,
                    RelationType = relationType,
                    TableKey = tableKey,
                    ForeignKey = foreignKeyName,
                    RelatedTableKeyInTableForeign = relatedTableKey
                };

                // İlişkili tablonun diğer alt ilişkileri
                var subJoins = new List<TableRelation>();

                foreach (var subForeignKey in foreignKey.PrincipalEntityType.GetForeignKeys())
                {
                    var subJoin = new TableRelation
                    {
                        TableName = subForeignKey.DeclaringEntityType.GetTableName(),
                        RelatedTableName = subForeignKey.PrincipalEntityType.GetTableName(),
                        RelationType = subForeignKey.IsUnique ? "OneToOne" : "OneToMany",
                        TableKey = string.Join(", ", subForeignKey.Properties.Select(p => p.Name)),
                        ForeignKey = string.Join(", ", subForeignKey.PrincipalKey.Properties.Select(p => p.Name)),
                        RelatedTableKeyInTableForeign = string.Join(", ", subForeignKey.PrincipalKey.Properties.Select(p => p.Name))
                    };

                    subJoins.Add(subJoin);
                }

                relation.SubJoins = subJoins;

                relations.Add(relation);
            }
        }

        return relations;
    }
}
