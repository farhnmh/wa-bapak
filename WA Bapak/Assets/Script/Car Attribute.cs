using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Car
{
    public int model;
    public float speed;

    public Car(int _model , float _speed)
    {
        model = _model;
        speed = _speed;
    }
}
