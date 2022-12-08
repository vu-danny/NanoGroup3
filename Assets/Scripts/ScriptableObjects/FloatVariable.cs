using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[CreateAssetMenu(fileName = "New Float", menuName = "Variables/Float")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] private float _value;
    public float Value
    {
        get { return _value; }
        set
        {
            if (valueChange != null)
                valueChange(_value, value);
            _value = value;
        }
    }

    public event Action<float, float> valueChange;

    public static implicit operator float(FloatVariable var)
    {
        return var.Value;
    }
}
