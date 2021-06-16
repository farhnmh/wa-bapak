using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerAction
{
    public string command;
    public int counter;
    public int id;

    public PlayerAction(string cmd, int count, int id)
    {
        command = cmd;
        counter = count;
        this.id = id;
    }
}
