using HumanClient;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HumanReactionControl : MonoBehaviour
{
    [SerializeField] private Image imageHumanReaction;
    [SerializeField] private Sprite spriteTransparent;
    [SerializeField] private Sprite spriteSmileHappy;
    [SerializeField] private Sprite spriteSmileMiddle;
    [SerializeField] private Sprite spriteSmileSad;
    [SerializeField] private Sprite spriteCrystal;
    [SerializeField] private Sprite spriteMoney;

    [SerializeField] private TextMeshProUGUI textPay;

    private HumanClientData _humanClientData;
    private HumanControls _humanControls;
    
    void Start()
    {
        _humanClientData = GetComponent<HumanClientData>();
        _humanControls = GetComponent<HumanControls>();
    }
    
    public void SetNoFindObjectSprite()
    {
        imageHumanReaction.sprite = 
            BuildingManager.Instance.objectsForBuilding[_humanClientData.targetsArray[_humanClientData.indexInTargetsArray]].sprite;
    }

    public void ClearHumanReactionSprite()
    {
        if (gameObject != UIManagerPanelHumanClient.Instance.currentGameObjectForPanelHumanClient)
        {
            imageHumanReaction.sprite = spriteTransparent;
        }
        else SetCrystalAboveHuman();
    }

    public void SetCrystalAboveHuman()
    {
        imageHumanReaction.sprite = spriteCrystal;
    }

    public void SetSmileAboveHuman()
    {
        if (gameObject == UIManagerPanelHumanClient.Instance.currentGameObjectForPanelHumanClient)
        {
            ClearHumanReactionSprite();
        }

        else
        {
            int mood = _humanControls.GetMood();
        
            if (mood <= LevelManager.MoodSad) imageHumanReaction.sprite = spriteSmileSad;
            else if (mood > LevelManager.MoodSad && mood < LevelManager.MoodHappy) imageHumanReaction.sprite = spriteSmileMiddle;
            else imageHumanReaction.sprite = spriteSmileHappy;
        }
    }

    public void SetMoneyAboveHuman()
    {
        imageHumanReaction.sprite = spriteMoney;
    }

    public void SetTextAboveHuman(string s)
    {
        textPay.text = s;
    }
    
}
