namespace ApProject.Models
{
    public class Table
    {
        public Table(string name)
        {
            Id = 1;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Column> Columns = new List<Column>();
        public static List<Table> AllTables=new List<Table>();
    }
}
