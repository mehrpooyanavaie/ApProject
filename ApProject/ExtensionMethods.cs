using ApProject.Models;

namespace ApProject
{
    public static class ExtensionMethods
    {
        public static bool Access(this string query)
        {
            int lastspaceindex = query.LastIndexOf(' ');
            string usernamestring = query.Substring(lastspaceindex + 1);
            var userwanttoedit = User.Users.SingleOrDefault(x => x.Name == usernamestring);
            if (userwanttoedit.IsAdmin)
                return true;
            else
                return false;
        }
    }
}
