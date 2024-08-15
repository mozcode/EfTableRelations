
namespace EfTableRelations.Entities;

    public class TableRelation
    {
        public string TableName { get; set; }
        public string RelatedTableName { get; set; }
        public string RelationType { get; set; }
        public string TableKey { get; set; }
        public string ForeignKey { get; set; }
        public string RelatedTableKeyInTableForeign { get; set; }
    }

