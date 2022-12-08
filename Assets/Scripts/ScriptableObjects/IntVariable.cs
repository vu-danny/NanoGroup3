using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[CreateAssetMenu(fileName = "New Integer", menuName = "Variables/Integer")]
public class IntVariable : ScriptableObject
{
    [SerializeField] private int _value;
    [SerializeField] private bool unsigned;
    public int Value
    {
        get { return _value; }
        set
        {
            int previous = _value;
            int newValue = value;
            if (unsigned && newValue < 0)
                newValue = 0;
            _value = newValue;
            if (valueChange != null)
                valueChange(previous, newValue);
            
        }
    }

    public event Action<int, int> valueChange;

    public static implicit operator int(IntVariable var)
    {
        return var.Value;
    }
}
