using UnityEngine;
using UnityEngine.UI;

public class HumanReactionControl : MonoBehaviour
{
    [SerializeField] private Image imageHumanReaction;
    [SerializeField] private Sprite spriteTransparent;
    [SerializeField] private Sprite spriteSmileHappy;
    [SerializeField] private Sprite spriteSmileMiddle;
    [SerializeField] private Sprite spriteSmileSad;
    [SerializeField] private Sprite spriteCrystal;
    
    private HumanControls humanControls;
    
    void Start()
    {
        humanControls = GetComponent<HumanControls>();
    }
    
    private void SetNoFindObjectSprite()
    {
        imageHumanReaction.sprite = BuildingManager.Instance.objectsForBuilding[humanControls.targetsArray[humanControls.indexInTargetsArray]].sprite;
    }

    public void ClearHumanReactionSprite()
    {
        imageHumanReaction.sprite = spriteTransparent;
    }

    public void SetCrystalAboveHuman()
    {
        imageHumanReaction.sprite = spriteCrystal;
    }

    public void SetSmileAboveHuman()
    {
        int mood = humanControls.GetMood();
        
        if (mood <= 25) imageHumanReaction.sprite = spriteSmileSad;
        else if (mood > 25 && mood < 75) imageHumanReaction.sprite = spriteSmileMiddle;
        else imageHumanReaction.sprite = spriteSmileHappy;
    }
}
