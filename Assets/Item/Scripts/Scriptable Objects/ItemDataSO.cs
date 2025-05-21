using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/Data/Item Data")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
}
