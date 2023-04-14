using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObj/Events/stringChannel")]
public class stringChannelSO : ScriptableObject
{
    public event UnityAction<string> OnEventTriggered;
    public void Raise(string str) => OnEventTriggered?.Invoke(str);
}
