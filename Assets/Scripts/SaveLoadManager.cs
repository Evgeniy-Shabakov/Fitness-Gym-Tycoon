using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;
    
    [SerializeField] private GameObject ParentAllDynamicObjects;

    [HideInInspector] public string savePath;
    private string saveFileName = "data.json";
    
    private List<GameObject> listGameObjectsForSave = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        
        #if UNITY_ANDROID && !UNITY_EDITOR
            savePath = Path.Combine(Application.persistentDataPath, saveFileName);
        #else
            savePath = Path.Combine(Application.dataPath, saveFileName);
        #endif
    }

    private void Start()
    {
        Load();
    }

    [Serializable]
    private class ObjectDataSave
    {
        public Vector3 position;
        public Quaternion rotation;
        public int indexInBuildingManagerList;
    }

    [Serializable]
    private class LevelDataSave
    {
        public int level;
        public int rating;
        public int pricePerVisit;
        
        public List<ObjectDataSave> listObjectsDataSave = new List<ObjectDataSave>();
    }
    
    [Serializable]
    private class AllDataSave
    {
        public int money;
        public int day;
        public int month;
        public int year;
        
        public List<LevelDataSave> listLevelsDataSave = new List<LevelDataSave>();
    }

    public void Save()
    {
        FillListGameObjectsForSave();

        LevelDataSave levelDataSave = new LevelDataSave();
        
        levelDataSave.level = 1;
        levelDataSave.rating = LevelManager.Instance.GetRating();
        levelDataSave.pricePerVisit = LevelManager.Instance.GetPricePerVisit();
        levelDataSave.listObjectsDataSave = FormListObjectsDataSave();
        
        AllDataSave allDataSave = new AllDataSave();
        
        allDataSave.money = PlayerData.GetMoney();
        
        allDataSave.day = DateSimulation.Instance.GetDay();
        allDataSave.month = DateSimulation.Instance.GetMonth();
        allDataSave.year = DateSimulation.Instance.GetYear();
        
        allDataSave.listLevelsDataSave.Add(levelDataSave);
        
        File.WriteAllText(savePath, JsonUtility.ToJson(allDataSave, true));
    }

    public void Load()
    {
        if (File.Exists(savePath) == false)
        {
            PlayerData.SetMoney(LevelManager.MoneyStartGame);
            
            LevelManager.Instance.SetRating(LevelManager.RatingOnStart);
            LevelManager.Instance.SetPricePerVisit(LevelManager.PricePerVisitOnStart);
            
            return;
        }
        
        AllDataSave allDataSave = new AllDataSave();
        allDataSave = JsonUtility.FromJson<AllDataSave>(File.ReadAllText(savePath));
        
        PlayerData.SetMoney(allDataSave.money);
        DateSimulation.Instance.SetDate(allDataSave.day, allDataSave.month, allDataSave.year);
        
        LevelDataSave levelDataSave = allDataSave.listLevelsDataSave[0];
        
        LevelManager.Instance.SetRating(levelDataSave.rating);
        LevelManager.Instance.SetPricePerVisit(levelDataSave.pricePerVisit);
        
        foreach (var objDataSave in levelDataSave.listObjectsDataSave)
        {
            BuildingManager.Instance.CreateAndSetObjectForLoad(objDataSave.indexInBuildingManagerList, 
                objDataSave.position, objDataSave.rotation);
        }
    }

    private List<ObjectDataSave> FormListObjectsDataSave()
    {
        List<ObjectDataSave> list = new List<ObjectDataSave>();
        
        foreach (var gameObj in listGameObjectsForSave)
        {
            ObjectDataSave obj = new ObjectDataSave();
            
            obj.position = gameObj.transform.position;
            obj.rotation = gameObj.transform.rotation;
            obj.indexInBuildingManagerList = gameObj.GetComponentInChildren<ObjectData>().indexInBuildingManagerList;
            
            list.Add(obj);
        }
        
        return list;
    }
    
    private void FillListGameObjectsForSave()
    {
        if (listGameObjectsForSave.Count != 0) listGameObjectsForSave.Clear();
        
        foreach(Transform child in ParentAllDynamicObjects.transform)
        {
            listGameObjectsForSave.Add(child.gameObject);
        }
    }
}
