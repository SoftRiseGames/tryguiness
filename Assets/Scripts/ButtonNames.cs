using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="buttonNames",menuName = "buttonName")]
public class ButtonNames : ScriptableObject
{
    public string buttonName1;
    public string buttonName2;
    public string buttonName3;
    public string question;
    public bool buttonName1Control;
    public bool buttonName2Control;
    public bool buttonName3Control;
}
