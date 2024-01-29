namespace ApProject.Models
{
    public class User
    {
        public User(string name,bool isadmin)
        {
            Name = name;
            IsAdmin = isadmin;
        }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }//Access Without Token
        public static List<User> Users = new List<User>();
    }
}
