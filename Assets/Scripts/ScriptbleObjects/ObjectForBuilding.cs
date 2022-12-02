using UnityEngine;

[CreateAssetMenu(fileName = "ObjectForBuilding", menuName = "New ObjectForBuilding")]
public class ObjectForBuilding : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int price;
    [SerializeField] private string description;
    [SerializeField] private float damageInPersent;
}
