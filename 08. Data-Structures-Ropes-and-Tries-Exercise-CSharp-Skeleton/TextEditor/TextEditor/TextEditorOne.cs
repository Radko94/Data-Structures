using System;
using System.Collections.Generic;
using System.Text;

class TextEditorOne : ITextEditor
{
    Dictionary<string, string> users = new Dictionary<string, string>();
    List<string> logOut = new List<string>();

    public void Clear(string username)
    {
        if (users.ContainsKey(username))
        {
            users[username] = "";
        }
    }

    public void Delete(string username, int startIndex, int length)
    {
        if (users.ContainsKey(username))
        {
            if (startIndex + length < users[username].Length)
            {
                string temp = "";

                for (int i = 0; i < users[username].Length; i++)
                {
                    if (i < startIndex || i > startIndex + length)
                        temp += users[username][i];
                }

                users[username] = temp;
            }
        }
    }

    public void Insert(string username, int index, string str)
    {
        if (users.ContainsKey(username))
        {
            if (!logOut.Contains(users[username]))
            {
                if (index < users[username].Length)
                {
                    users[username].Insert(index, str);
                }
            }
        }
    }

    public int Length(string username)
    {
        if (users.ContainsKey(username))
        {
            return users[username].Length;
        }
        else
        {
            return 0;
        }
    }

    public void Login(string username)
    {
        users.Add(username, "");
    }

    public void Logout(string username)
    {
        if (users.ContainsKey(username))
            logOut.Add(username);
    }

    public void Prepend(string username, string str)
    {
        if (users.ContainsKey(username))
        {
            users[username].Insert(0, str);
        }
    }

    public string Print(string username)
    {
        if (users.ContainsKey(username))
        {
            return users[username];
        }
        else
            return null;
    }

    public void Substring(string username, int startIndex, int length)
    {
        if (users.ContainsKey(username))
        {
            string well  = users[username].Substring(startIndex, length);
            users[username] = well;
        }
    }

    public void Undo(string username)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> Users(string prefix = "")
    {
        List<string> usersOutput = new List<string>();

        foreach (var item in users)
        {
            usersOutput.Add(item.Key);
        }

        return usersOutput;
    }
}
