using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class User
{
    public string uname;
    public string password;

    public User(string _uname, string _pw)
    {
        uname = _uname;
        password = _pw;
    }

}

