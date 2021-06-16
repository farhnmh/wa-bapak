using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerAttribute
{
    public int hp;
    public int kill;

    public PlayerAttribute(int _hp, int _kill)
    {
        hp = _hp;
        kill = _kill;
    }
}
