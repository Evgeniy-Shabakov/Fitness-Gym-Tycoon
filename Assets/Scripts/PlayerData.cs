using UI;

public static class PlayerData
{
    private static int _money;

    public static int GetMoney()
    {
        return _money;
    }
    
    public static void SetMoney(int value)
    {
        _money = value;
        UIManagerMain.Instance.SetTextMoney(_money);
    }
    
    public static void AddMoney(int amount)
    {
        _money += amount;
        
        UIManagerMain.Instance.SetTextMoney(_money);
        SaveLoadManager.Instance.Save();
    }

    public static void SpendMoney(int amount)
    {
        _money -= amount;
        
        UIManagerMain.Instance.SetTextMoney(_money);
        SaveLoadManager.Instance.Save();
    }
}
