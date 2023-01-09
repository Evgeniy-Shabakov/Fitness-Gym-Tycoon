using System.Collections.Generic;
using BuildingSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectForBuilding", menuName = "New ObjectForBuilding")]
public class ObjectForBuilding : ScriptableObject
{
    public ObjectType type;
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
