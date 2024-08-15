namespace EfTableRelations.Entities;

    public class EntityRelationDto
    {
        public string TableName { get; set; }
        public string RelatedTableName { get; set; }
        public string RelationType { get; set; }
        public string TableKey { get; set; }
        public string ForeignKey { get; set; }
        public string RelatedTableKeyInTableForeign { get; set; }
        public List<TableRelation> SubJoins { get; set; }

        public EntityRelationDto()
        {
            SubJoins = new List<TableRelation>();
        }
}
