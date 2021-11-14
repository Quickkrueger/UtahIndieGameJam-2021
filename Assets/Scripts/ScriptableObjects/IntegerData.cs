using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntegerData", menuName = "ScriptableObjects/Data/IntegerData", order = 1)]
public class IntegerData : ScriptableObject
{
    public int value;
    
    public delegate void OnValueChangedDelegate();
    public event OnValueChangedDelegate OnValueChanged;
    
    public void AddToValue(int changeToInt)
    {
        value += changeToInt;
        if (OnValueChanged != null) OnValueChanged();
    }

    public void ResetValue()
    {
        value = 0;
        if (OnValueChanged != null) OnValueChanged();
    }

    public void SetValue(int newValue)
    {
        value = newValue;
        if (OnValueChanged != null) OnValueChanged();
    }

    public void SaveValue()
    {
        PlayerPrefs.SetInt(this.name, value);
        PlayerPrefs.Save();
    }

    public void LoadValue()
    {
        value = PlayerPrefs.GetInt(this.name);
    }
    
    
    
    
}
