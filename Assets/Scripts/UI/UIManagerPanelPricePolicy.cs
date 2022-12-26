using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerPanelPricePolicy : MonoBehaviour
{
    public static UIManagerPanelPricePolicy Instance;

    public GameObject panel;
    
    [SerializeField] private TextMeshProUGUI textPricePerVisit;
    [SerializeField] private TextMeshProUGUI textPricePerMonth;
    [SerializeField] private TextMeshProUGUI textPricePerSixMonth;
    [SerializeField] private TextMeshProUGUI textPricePerYear;
    
    
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
        textPricePerMonth.text = LevelManager.Instance.GetPricePerMonth() + "";
        textPricePerSixMonth.text = LevelManager.Instance.GetPricePerSixMonth() + "";
        textPricePerYear.text = LevelManager.Instance.GetPricePerYear() + "";
    }

    public void BtMinusPricePerVisit()
    {
        var price = LevelManager.Instance.GetPricePerVisit() - LevelManager.StepPricePerVisit;
        LevelManager.Instance.SetPricePerVisit(price);
        textPricePerVisit.text = LevelManager.Instance.GetPricePerVisit() + "";
    }
    
    public void BtPlusPricePerVisit()
    {
        var price = LevelManager.Instance.GetPricePerVisit() + LevelManager.StepPricePerVisit;
        LevelManager.Instance.SetPricePerVisit(price);
        textPricePerVisit.text = LevelManager.Instance.GetPricePerVisit() + "";
    }
    
    public void BtMinusPricePerMonth()
    {
        var price = LevelManager.Instance.GetPricePerMonth() - LevelManager.StepPricePerMonth;
        LevelManager.Instance.SetPricePerMonth(price);
        textPricePerMonth.text = LevelManager.Instance.GetPricePerMonth() + "";
    }
    
    public void BtPlusPricePerMonth()
    {
        var price = LevelManager.Instance.GetPricePerMonth() + LevelManager.StepPricePerMonth;
        LevelManager.Instance.SetPricePerMonth(price);
        textPricePerMonth.text = LevelManager.Instance.GetPricePerMonth() + "";
    }
    
    public void BtMinusPricePerSixMonth()
    {
        var price = LevelManager.Instance.GetPricePerSixMonth() - LevelManager.StepPricePerSixMonth;
        LevelManager.Instance.SetPricePerSixMonth(price);
        textPricePerSixMonth.text = LevelManager.Instance.GetPricePerSixMonth() + "";
    }
    
    public void BtPlusPricePerSixMonth()
    {
        var price = LevelManager.Instance.GetPricePerSixMonth() + LevelManager.StepPricePerSixMonth;
        LevelManager.Instance.SetPricePerSixMonth(price);
        textPricePerSixMonth.text = LevelManager.Instance.GetPricePerSixMonth() + "";
    }

    public void BtMinusPricePerYear()
    {
        var price = LevelManager.Instance.GetPricePerYear() - LevelManager.StepPricePerYear;
        LevelManager.Instance.SetPricePerYear(price);
        textPricePerYear.text = LevelManager.Instance.GetPricePerYear() + "";
    }
    
    public void BtPlusPricePerYear()
    {
        var price = LevelManager.Instance.GetPricePerYear() + LevelManager.StepPricePerYear;
        LevelManager.Instance.SetPricePerYear(price);
        textPricePerYear.text = LevelManager.Instance.GetPricePerYear() + "";
    }
}
