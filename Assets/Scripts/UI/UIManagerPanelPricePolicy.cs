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
        SaveLoadManager.Instance.Save();
    }

    public void Open()
    {
        panel.SetActive(true);

        textPricePerVisit.text = LevelManager.Instance.GetPricePerVisit() + "";
        sliderPricePerVisit.value = LevelManager.Instance.GetPricePerVisit();
    }

    public void SetPricePerVisit()
    {
        LevelManager.Instance.SetPricePerVisitFromSlider(sliderPricePerVisit);
        textPricePerVisit.text = LevelManager.Instance.GetPricePerVisit() + "";
    }
}
