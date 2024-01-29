// See https://aka.ms/new-console-template for more information
using ApProject.Models;
using ApProject;

string createuser = "create user";
string createtable = "create table";
string addcolumn = "add column";
string addrow = "add row";
string changerow = "change";
string printtable = "print";
string searchintable = "search";
string deletetable = "delete table";
string removecolumn = "remove column";
string removerow = "remove row";
string mystring = Console.ReadLine();
while (mystring != "done")
{
    if (mystring.Substring(0, createuser.Length) == createuser)
    {
        bool isvalid = true;
        bool isadmin = true;
        var y = mystring.Substring(12);
        int index = y.IndexOf(' ');
        string username = y.Substring(0, index);
        y = y.Substring(index + 1);
        if (y == "editor")
            isadmin = true;
        else if (y == "viewer")
            isadmin = false;
        else
        {
            Console.WriteLine("please write a correct query(usernames can not contain space)");
            isvalid = false;
        }
        if (isvalid)
        {
            var repeatedname = User.Users.SingleOrDefault(x => x.Name == username);
            if (repeatedname == null)
            {
                User user = new User(username, isadmin);
                User.Users.Add(user);
            }
            else
                Console.WriteLine("this user name was exist");
        }
    }
    if (mystring.Substring(0, createtable.Length) == createtable)
    {
        var y = mystring.Substring(13);
        if (y.Access())
        {
            int index = y.IndexOf(' ');
            string tablename = y.Substring(0, index);
            Table table = new Table(tablename);
            Table.AllTables.Add(table);
        }
        else
            Console.WriteLine("access denied");
    }
    if (mystring.Substring(0, deletetable.Length) == deletetable)
    {
        var y = mystring.Substring(13);
        if (y.Access())
        {
            int index = y.IndexOf(' ');
            string tablename = y.Substring(0, index);
            var tabletodelete = Table.AllTables.SingleOrDefault(x => x.Name == tablename);
            if (tabletodelete == null)
                Console.WriteLine("tablename does not exist");
            else
                Table.AllTables.Remove(tabletodelete);
        }
        else
            Console.WriteLine("access denied");

    }
    if (mystring.Substring(0, addcolumn.Length) == addcolumn)
    {
        var y = mystring.Substring(11);
        if (y.Access())
        {
            int index = y.IndexOf(' ');
            string tablename = y.Substring(0, index);
            string columnnamewithusername = y.Substring(index + 1);
            index = columnnamewithusername.IndexOf(" ");
            string columnname = columnnamewithusername.Substring(0, index);
            string usernamewithtype = columnnamewithusername.Substring(index + 1);
            index = usernamewithtype.IndexOf(" ");
            string type = usernamewithtype.Substring(0, index); ;
            var tableindex = Table.AllTables.FindIndex(x => x.Name == tablename);
            var whichtable = Table.AllTables.SingleOrDefault(x => x.Name == tablename);
            Column column = new Column(columnname, type);
            Table.AllTables[tableindex].Columns.Add(column);
            int rowscount = Table.AllTables[tableindex].Columns[0].AllColumnData.Count;
            if (rowscount > 0)
            {
                if (type == "int")
                    for (int i = 0; i < rowscount; i++)
                    {
                        Table.AllTables[tableindex].Columns.SingleOrDefault(x => x.Name == columnname).
                            AllColumnData.Add(new ColumnData() { intvalue = 0, RowNumber = i + 1 });
                    }
                else if (type == "string")
                    for (int i = 0; i < rowscount; i++)
                    {
                        Table.AllTables[tableindex].Columns.SingleOrDefault(x => x.Name == columnname).
                            AllColumnData.Add(new ColumnData() { stringvalue = "null", RowNumber = i + 1 });
                    }
            }
        }
        else
            Console.WriteLine("access denied");
    }
    if (mystring.Substring(0, removecolumn.Length) == removecolumn)
    {
        //without cascade
        var y = mystring.Substring(14);
        if (y.Access())
        {
            int index = y.IndexOf(' ');
            string tablename = y.Substring(0, index);
            string columnnamewithusername = y.Substring(index + 1);
            index = columnnamewithusername.IndexOf(" ");
            string columnname = columnnamewithusername.Substring(0, index);
            var tableindex = Table.AllTables.FindIndex(x => x.Name == tablename);
            var whichtable = Table.AllTables.SingleOrDefault(x => x.Name == tablename);
            var columnwanttoremove = Table.AllTables[tableindex].Columns.SingleOrDefault(x => x.Name == columnname);
            Table.AllTables[tableindex].Columns.Remove(columnwanttoremove);
        }
        else
            Console.WriteLine("access denied");
    }
    if (mystring.Substring(0, addrow.Length) == addrow)
    {
        var y = mystring.Substring(8);
        if (y.Access())
        {
            int index = y.IndexOf(' ');
            string tablename = y.Substring(0, index);
            var tableindex = Table.AllTables.FindIndex(x => x.Name == tablename);
            var whichtable = Table.AllTables.SingleOrDefault(x => x.Name == tablename);
            ColumnData forDebbug;
            foreach (var c in Table.AllTables[tableindex].Columns)
            {
                forDebbug = new ColumnData();
                if (c.Type == "int")
                    forDebbug.intvalue = 0;
                else if (c.Type == "string")
                    forDebbug.stringvalue = "null";
                forDebbug.RowNumber = c.AllColumnData.Count + 1;
                c.AllColumnData.Add(forDebbug);
            }
        }
        else
            Console.WriteLine("access denied");
    }
    if (mystring.Substring(0, removerow.Length) == removerow)
    {
        var y = mystring.Substring(11);
        if (y.Access())
        {
            int index = y.IndexOf(' ');
            string tablename = y.Substring(0, index);
            string rownumberwithusername = y.Substring(index + 1);
            index = rownumberwithusername.IndexOf(" ");
            int numberofrow = Convert.ToInt32(rownumberwithusername.Substring(0, index));
            foreach (var c in Table.AllTables.SingleOrDefault(x => x.Name == tablename).Columns)
            {
                var rowtoremove = c.AllColumnData.SingleOrDefault(x => x.RowNumber == numberofrow);
                c.AllColumnData.Remove(rowtoremove);
                foreach (var r in c.AllColumnData)
                {
                    if (r.RowNumber > numberofrow)
                        r.RowNumber -= 1;
                }
            }
        }
        else
            Console.WriteLine("access denied");
    }
    if (mystring.Substring(0, changerow.Length) == changerow)
    {
        var y = mystring.Substring(7);
        if (y.Access())
        {
            int index = y.IndexOf(' ');
            string tablename = y.Substring(0, index);
            string rowwithcolumnwithvaluewithuser = y.Substring(index + 1);
            index = rowwithcolumnwithvaluewithuser.IndexOf(' ');
            string rowname = rowwithcolumnwithvaluewithuser.Substring(0, index);
            string columnwithvaluewithuser = rowwithcolumnwithvaluewithuser.Substring(index + 1);
            index = columnwithvaluewithuser.IndexOf(' ');
            string columnname = columnwithvaluewithuser.Substring(0, index);
            string valuewithuser = columnwithvaluewithuser.Substring(index + 1);
            index = valuewithuser.IndexOf(' ');
            string value = valuewithuser.Substring(0, index);
            int rownumber = Convert.ToInt32(rowname);
            if (Table.AllTables.SingleOrDefault(x => x.Name == tablename).
                Columns.SingleOrDefault(c => c.Name == columnname).Type == "int")
            {
                Table.AllTables.SingleOrDefault(x => x.Name == tablename).
                    Columns.SingleOrDefault(c => c.Name == columnname).
                        AllColumnData.SingleOrDefault(r => r.RowNumber == rownumber).intvalue = Convert.ToInt32(value);
            }
            else if (Table.AllTables.SingleOrDefault(x => x.Name == tablename).
                Columns.SingleOrDefault(c => c.Name == columnname).Type == "string")
            {
                Table.AllTables.SingleOrDefault(t => t.Name == tablename).Columns.SingleOrDefault(c => c.Name == columnname).
                    AllColumnData.SingleOrDefault(r => r.RowNumber == rownumber).stringvalue = value;
            }
        }
        else
            Console.WriteLine("access denied");
    }
    if (mystring.Substring(0, printtable.Length) == printtable)
    {
        var y = mystring.Substring(6);
        int index = y.IndexOf(' ');
        string tablename = y.Substring(0, index);
        string withouttablename = y.Substring(index + 1);
        if (withouttablename[0] == '*')
        {
            int columnscount = Table.AllTables.SingleOrDefault(x => x.Name == tablename).Columns.Count;
            int rowscount = Table.AllTables.SingleOrDefault(x => x.Name == tablename).Columns[0].AllColumnData.Count;
            for (int i = 0; i < rowscount; i++)
            {
                for (int j = 0; j < columnscount; j++)
                {
                    if (Table.AllTables.SingleOrDefault(x => x.Name == tablename).Columns[j].Type == "int")
                    {
                        Console.Write(Table.AllTables.SingleOrDefault(x => x.Name == tablename).Columns[j].
                            AllColumnData[i].intvalue + " ");
                    }
                    else if (Table.AllTables.SingleOrDefault(x => x.Name == tablename).Columns[j].Type == "string")
                    {
                        Console.Write(Table.AllTables.SingleOrDefault(x => x.Name == tablename).Columns[j].
                            AllColumnData[i].stringvalue + " ");
                    }
                }
                Console.Write("\n");
            }
        }
        else
        {
            int columncountswanttocopare = withouttablename.Count(x => x == ' ');//space count equals to columns count
            int allrowscount = Table.AllTables.SingleOrDefault(t => t.Name == tablename).Columns[0].AllColumnData.Count;
            List<List<RowForCompare>> allrowscontaincomparecolumns = new List<List<RowForCompare>>();
            List<RowForCompare> q;
            for (int j = 0; j < allrowscount; j++)
            {
                q = new List<RowForCompare>();
                for (int i = 0; i <= columncountswanttocopare; i++)
                    q.Add(new RowForCompare() { StringValue = "" });
                allrowscontaincomparecolumns.Add(q);
            }

            for (int i = 0; i <= columncountswanttocopare; i++)
            {
                if (i == columncountswanttocopare)
                {
                    for (int j = 0; j < allrowscount; j++)
                        allrowscontaincomparecolumns[j][i].IntValue = j + 1;
                    break;
                }
                index = withouttablename.IndexOf(' ');
                string columnnamewantoget = withouttablename.Substring(0, index);
                var g = Table.AllTables.SingleOrDefault(t => t.Name == tablename).
                    Columns.SingleOrDefault(x => x.Name == columnnamewantoget);
                if (g.Type == "string")
                    for (int j = 0; j < allrowscount; j++)
                        allrowscontaincomparecolumns[j][i].StringValue = g.AllColumnData[j].stringvalue;
                else if (g.Type == "int")
                    for (int j = 0; j < allrowscount; j++)
                        allrowscontaincomparecolumns[j][i].IntValue = g.AllColumnData[j].intvalue;
                withouttablename = withouttablename.Substring(index + 1);
            }
            int o = 0;
            for (int i = 0; i <= columncountswanttocopare; i++)
            {
                for (int j = 0; j < allrowscount - 1; j++)
                {
                    if (i == 0)
                    {
                        if (allrowscontaincomparecolumns[j][i].StringValue == "")
                        {
                            for (int k = j + 1; k < allrowscount; k++)
                                if (allrowscontaincomparecolumns[j][i].IntValue > allrowscontaincomparecolumns[k][i].IntValue)
                                    for (int l = 0; l <= columncountswanttocopare; l++)
                                    {
                                        var temp = allrowscontaincomparecolumns[j][l];
                                        allrowscontaincomparecolumns[j][l] = allrowscontaincomparecolumns[k][l];
                                        allrowscontaincomparecolumns[k][l] = temp;
                                    }
                            var mydebug = allrowscontaincomparecolumns;
                        }
                        else
                        {
                            for (int k = j + 1; k < allrowscount; k++)
                            {
                                if (allrowscontaincomparecolumns[j][i].StringValue.CompareTo(allrowscontaincomparecolumns[k][i].StringValue) > 0)
                                {
                                    for (int l = 0; l <= columncountswanttocopare; l++)
                                    {
                                        var temp = allrowscontaincomparecolumns[j][l];
                                        allrowscontaincomparecolumns[j][l] = allrowscontaincomparecolumns[k + 1][l];
                                        allrowscontaincomparecolumns[k][l] = temp;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (allrowscontaincomparecolumns[j][i - 1].StringValue == "")
                        {
                            for (int p = j + 1; p < allrowscount; p++)
                            {
                                if (allrowscontaincomparecolumns[j][i - 1].IntValue == allrowscontaincomparecolumns[p][i - 1].IntValue)
                                    o++;
                                else
                                    break;
                            }
                        }
                        else
                            for (int p = j + 1; p < allrowscount; p++)
                            {
                                if (allrowscontaincomparecolumns[j][i - 1].StringValue == allrowscontaincomparecolumns[p][i - 1].StringValue)
                                    o++;
                                else
                                    break;
                            }
                        if (o != 0)
                        {
                            if (allrowscontaincomparecolumns[j][i].StringValue == "")
                                for (int k = 1; k <= o; k++)
                                {
                                    if (allrowscontaincomparecolumns[j][i].IntValue > allrowscontaincomparecolumns[j + k][i].IntValue)
                                        for (int l = 0; l <= columncountswanttocopare; l++)
                                        {
                                            var temp = allrowscontaincomparecolumns[j][l];
                                            allrowscontaincomparecolumns[j][l] = allrowscontaincomparecolumns[j + k][l];
                                            allrowscontaincomparecolumns[j + k][l] = temp;
                                        }
                                }
                            else
                            {
                                for (int k = 1; k <= o; k++)
                                {
                                    if (allrowscontaincomparecolumns[j][i].StringValue.CompareTo(allrowscontaincomparecolumns[j + k][i].StringValue) > 0)
                                    {
                                        for (int l = 0; l <= columncountswanttocopare; l++)
                                        {
                                            var temp = allrowscontaincomparecolumns[j][l];
                                            allrowscontaincomparecolumns[j][l] = allrowscontaincomparecolumns[j + k][l];
                                            allrowscontaincomparecolumns[j + k][l] = temp;
                                        }
                                    }
                                }
                            }

                        }
                        j += o;
                        o = 0;
                    }
                }
            }
            List<int> rowsnumberssorted = new List<int>();
            foreach (var x in allrowscontaincomparecolumns)
            {
                rowsnumberssorted.Add(x[columncountswanttocopare].IntValue);
                var t = rowsnumberssorted;
            }
            foreach (var x in rowsnumberssorted)
            {
                foreach (var h in Table.AllTables.SingleOrDefault(c => c.Name == tablename).Columns)
                {
                    if (h.Type == "int")
                        Console.Write(h.AllColumnData.FirstOrDefault(c => c.RowNumber == x).intvalue + " ");
                    else if (h.Type == "string")
                        Console.Write(h.AllColumnData.FirstOrDefault(c => c.RowNumber == x).stringvalue + " ");
                }
                Console.Write("\n");
            }
        }
    }
    if (mystring.Substring(0, searchintable.Length) == searchintable)
    {
        var y = mystring.Substring(7);
        int index = y.IndexOf(" ");
        var tablename = y.Substring(0, index);
        var columnwithvaluewithusername = y.Substring(index + 1);
        index = columnwithvaluewithusername.IndexOf(" ");
        var columnname = columnwithvaluewithusername.Substring(0, index);
        var valuewithusername = columnwithvaluewithusername.Substring(index + 1);
        index = valuewithusername.IndexOf(" ");
        var value = valuewithusername.Substring(0, index);
        var mytable = Table.AllTables.SingleOrDefault(x => x.Name == tablename);
        var mycolumn = mytable.Columns.SingleOrDefault(x => x.Name == columnname);
        int newvalue;
        //var k = value;
        List<ColumnData> columnDatas = new List<ColumnData>();
        List<int> rowsnumbers = new List<int>();
        if (mycolumn.Type == "int")
        {
            newvalue = Convert.ToInt32(value);
            columnDatas = mycolumn.AllColumnData.Where(x => x.intvalue == newvalue).ToList();
        }
        else if (mycolumn.Type == "string")
            columnDatas = mycolumn.AllColumnData.Where(x => x.stringvalue == value).ToList();
        rowsnumbers = columnDatas.Select(x => x.RowNumber).ToList();
        for (int i = 0; i < rowsnumbers.Count; i++)
        {
            for (int j = 0; j < mytable.Columns.Count; j++)
            {
                if (mytable.Columns[j].Type == "int")
                    Console.Write(mytable.Columns[j].AllColumnData[rowsnumbers[i] - 1].intvalue + " ");
                else if (mytable.Columns[j].Type == "string")
                    Console.Write(mytable.Columns[j].AllColumnData[rowsnumbers[i] - 1].stringvalue + " ");

            }
            Console.Write("\n");
        }
    }
    mystring = Console.ReadLine();
}
Console.ReadKey();