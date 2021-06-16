using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectSerialization
{
    public float exp;
    public float pos;

    public ObjectSerialization(float _exp, float _pos)
    {
        exp = _exp;
        pos = _pos;
    }
}
