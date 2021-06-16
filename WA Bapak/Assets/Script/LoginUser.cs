using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LoginUser
{
    public string uname;
    public int id;

    public LoginUser(string _uname, int _id)
    {
        uname = _uname;
        id = _id;
    }

}

