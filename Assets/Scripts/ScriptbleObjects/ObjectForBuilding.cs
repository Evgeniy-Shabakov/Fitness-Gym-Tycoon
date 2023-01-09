using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectForBuilding", menuName = "New ObjectForBuilding")]
public class ObjectForBuilding : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public LayerMask layerFloor;

    public AnimatorOverrideController animatorOverrideController;
    public bool needBarbellInHands;
    public bool needDumbbellsInHands;
    
    public int price;
    public string description;
    public float damageInPercent;
    public int maxCountClients;
}
