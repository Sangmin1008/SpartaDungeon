using UnityEngine;
using UnityEngine.Events;

public class GenericEventChannelSO<T> : ScriptableObject
{
    public event UnityAction<T> OnEventRaised;
    public void Raise(T data) => OnEventRaised?.Invoke(data);
}