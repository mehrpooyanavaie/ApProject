namespace ApProject.Models
{
    public class Column
    {
        public Column(string name, string type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; set; }
        public List<ColumnData> AllColumnData = new List<ColumnData>();
        public string Type { get; set; }
    }
}
