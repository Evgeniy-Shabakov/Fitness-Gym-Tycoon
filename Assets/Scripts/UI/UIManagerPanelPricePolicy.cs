using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerPanelPricePolicy : MonoBehaviour
{
    public static UIManagerPanelPricePolicy Instance;

    public GameObject panel;
    
    [SerializeField] private Slider sliderPricePerVisit;
    [SerializeField] private TextMeshProUGUI textPricePerVisit;
    
    
    private void Awake()
    {
        Instance = this;
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    public void Open()
    {
        panel.SetActive(true);

        textPricePerVisit.text = LevelManager.pricePerVisit + "";
        sliderPricePerVisit.value = LevelManager.pricePerVisit;
    }

    public void SetPricePerVisit()
    {
        LevelManager.pricePerVisit = (int)sliderPricePerVisit.value;
        textPricePerVisit.text = LevelManager.pricePerVisit + "";
    }
}
