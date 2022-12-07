using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private GameObject ParentAllDynamicObjects;

    private string savePath;
    private string saveFileName = "data.json";
    
    private List<GameObject> listGameObjectsForSave = new List<GameObject>();

    private void Awake()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            savePath = Path.Combine(Application.persistentDataPath, saveFileName);
        #else
            savePath = Path.Combine(Application.dataPath, saveFileName);
        #endif
    }

    private void Start()
    {
        BuildingManager.ObjectInstalledOrDeleted.AddListener(Save);
        
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
        public List<ObjectDataSave> listObjectsDataSave = new List<ObjectDataSave>();
    }
    
    [Serializable]
    private class AllDataSave
    {
        public List<LevelDataSave> listLevelsDataSave = new List<LevelDataSave>();
    }

    public void Save()
    {
        FillListGameObjectsForSave();

        LevelDataSave levelDataSave = new LevelDataSave();
        levelDataSave.level = 1;
        levelDataSave.listObjectsDataSave = FormListObjectsDataSave();
        
        AllDataSave allDataSave = new AllDataSave();
        allDataSave.listLevelsDataSave.Add(levelDataSave);
        
        File.WriteAllText(savePath, JsonUtility.ToJson(allDataSave, true));
    }

    public void Load()
    {
        if (File.Exists(savePath) == false) return;
        
        AllDataSave allDataSave = new AllDataSave();

        allDataSave =
            JsonUtility.FromJson<AllDataSave>(File.ReadAllText(savePath));
        
        LevelDataSave levelDataSave = allDataSave.listLevelsDataSave[0];

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
