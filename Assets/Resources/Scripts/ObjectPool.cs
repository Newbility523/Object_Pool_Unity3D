using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public enum ObjectType
//{
//    Ball,
//    Cube
//}

public class ObjectPool {

    public string configPath = "prefabPath";

    private Dictionary<string, Queue<GameObject>> queueDir;
    private Dictionary<string, GameObject> prefabDir;

    public static ObjectPool GetObjectPool
    {
        get
        {
            if (objectPool == null)
            {
                objectPool = new ObjectPool();
            }
            return objectPool;
        }
    }

    private static ObjectPool objectPool;

    private ObjectPool()
    {
        queueDir = new Dictionary<string, Queue<GameObject>>();
        prefabDir = new Dictionary<string, GameObject>();
        LoadPrefab();
    }

    private void LoadPrefab()
    {
        TextAsset prefabPathTxt = Resources.Load(configPath) as TextAsset;
        
        string[] paths = prefabPathTxt.text.Split('\n');
        // // Test
        // foreach(var item in paths){
        //     Debug.Log(item);
        // }
        foreach(string path in paths) {
            // Make it better : How to cut string more compactly?
            string temp = path.Replace("\n","");
            temp = temp.Replace("\r","");
            // Make it better
            GameObject prefab = Resources.Load(temp) as GameObject;
            if(prefab!=null){
                string name = path.Replace("Prefabs/","");
                name = name.Replace("\n", "");
                name = name.Replace("\r", "");
                Debug.Log(name);
                AddItem(name, prefab);
            }
        }
    }

    private void AddItem(string name, GameObject go){
        if(!go.GetComponent<prefabDestroy>())
            go.AddComponent<prefabDestroy>();
        prefabDir.Add(name, go);
        Queue<GameObject> tempPrefabQueue = new Queue<GameObject>();
        queueDir.Add(name, tempPrefabQueue);
    }

    public GameObject GetGameObject(string name)
    {
        if (prefabDir.ContainsKey(name))
        {
            Queue<GameObject> goq = queueDir[name];
            if (goq.Count != 0)
            {
                GameObject result = goq.Dequeue();
                result.SetActive(true);
                return result;
            }
            else
            {
                GameObject prefab = prefabDir[name];
                GameObject prefabClone = GameObject.Instantiate(prefab);
                return prefabClone;
            }
        }
        //不存在该素材
        Debug.Log("No this prefab !");
        return null;
    }

    public void SaveGameObject(string name, GameObject go) {
        if (prefabDir.ContainsKey(name))
        {
            queueDir[name].Enqueue(go);
            go.SetActive(false);
        }
        else// Add new item in prefabDir
        {
            AddItem(name, go);
            SaveGameObject(name, go);
        }
    }
}
