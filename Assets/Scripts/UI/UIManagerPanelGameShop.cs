using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerPanelGameShop : MonoBehaviour
{
    public static UIManagerPanelGameShop Instance;
    
    public GameObject panelGameShop;
    
    [SerializeField] private GameObject contentScrollViewGameShop;
    [SerializeField] private GameObject prefabBtGameShop;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        for (int i = 0; i < BuildingManager.Instance.objectsForBuilding.Count; i++)
        {
            GameObject bt = Instantiate(prefabBtGameShop, contentScrollViewGameShop.transform);
            bt.transform.GetChild(0).GetComponent<Image>().sprite = BuildingManager.Instance.objectsForBuilding[i].sprite;
            bt.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = BuildingManager.Instance.objectsForBuilding[i].price + "";
        }
    }
    
    public void Open()
    {
        UIManagerMain.Instance.CloseAllPanels();
        panelGameShop.SetActive(true);
    }

    public void Close()
    {
        panelGameShop.SetActive(false);
    }
}
