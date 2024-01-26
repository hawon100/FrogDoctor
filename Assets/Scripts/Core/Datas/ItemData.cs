using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string ItemName;
    public Sprite ItemIcon;
    [TextArea] public string ItemLog;
}

[CreateAssetMenu(fileName = "New ItemData", menuName = "Data/ItemData", order = int.MinValue)]
public class ItemData : BaseData
{
    public Item[] items;
}