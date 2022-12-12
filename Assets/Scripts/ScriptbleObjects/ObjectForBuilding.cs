using UnityEngine;

[CreateAssetMenu(fileName = "ObjectForBuilding", menuName = "New ObjectForBuilding")]
public class ObjectForBuilding : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public AnimatorOverrideController animatorOverrideController;
    public int price;
    public string description;
    public float damageInPersent;
}
