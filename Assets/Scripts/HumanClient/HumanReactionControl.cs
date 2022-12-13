using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanReactionControl : MonoBehaviour
{
    [SerializeField] private Image imageHumanReaction;
    [SerializeField] private Sprite spriteTransparent;
    private HumanControls humanControls;
    
    void Start()
    {
        humanControls = GetComponent<HumanControls>();
        humanControls.NeededAndFreeObjectNoFinded.AddListener(SetHumanReactionSprite);
        humanControls.NeededAndFreeObjectFinded.AddListener(ClearHumanReactionSprite);
    }
    
    private void SetHumanReactionSprite()
    {
        imageHumanReaction.sprite = BuildingManager.Instance.objectsForBuilding[humanControls.index].sprite;
    }

    private void ClearHumanReactionSprite()
    {
        imageHumanReaction.sprite = spriteTransparent;
    }
}
