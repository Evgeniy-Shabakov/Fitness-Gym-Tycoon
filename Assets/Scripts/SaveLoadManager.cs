using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private GameObject ParentAllDynamicObjects;
    private List<GameObject> listGameObjectsForSave;

    private class ObjectDataSave
    {
        public Vector3 position;
        public Quaternion rotation;
        public int indexInBuildingManagerList;
    }

    private class LevelDataSave
    {
        public int level;
        public List<ObjectDataSave> listObjectsDataSave;
    }
    
    private class AllDataSave
    {
        public List<LevelDataSave> listLevelsDataSave;
    }

    public void Save()
    {
        FillListGameObjectsForSave();

        LevelDataSave levelDataSave = new LevelDataSave();
        levelDataSave.level = 1;
        levelDataSave.listObjectsDataSave = FormListObjectsDataSave();

        AllDataSave allDataSave = new AllDataSave();
        allDataSave.listLevelsDataSave.Add(levelDataSave);
    }

    public void Load()
    {
        AllDataSave allDataSave = new AllDataSave();
        
        LevelDataSave levelDataSave = allDataSave.listLevelsDataSave[0];

        foreach (var objDataSave in levelDataSave.listObjectsDataSave)
        {
            BuildingManager.Instance.CreateObjectForBuild(objDataSave.indexInBuildingManagerList);
            
            BuildingManager.Instance.objectForBuild.transform.position = objDataSave.position;
            BuildingManager.Instance.objectForBuild.transform.rotation = objDataSave.rotation;
            BuildingManager.Instance.objectForBuild.GetComponentInChildren<ObjectData>().isNew = false;
            
            BuildingManager.Instance.SetObject();
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
        listGameObjectsForSave.Clear();
        
        foreach(Transform child in ParentAllDynamicObjects.transform)
        {
            listGameObjectsForSave.Add(child.gameObject);
        }
    }
}
