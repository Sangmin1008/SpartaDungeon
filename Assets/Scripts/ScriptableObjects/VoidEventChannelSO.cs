using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidChannel", menuName = "Scriptable Objects/Events/Void Event Channel")]
public class VoidEventChannelSO : ScriptableObject
{
    public event UnityAction OnEventRaised;
    public void Raise() => OnEventRaised?.Invoke();
}