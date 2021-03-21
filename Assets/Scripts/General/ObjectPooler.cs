using System.Collections.Generic;
using UnityEngine;

/*! /class ObjectPooler
 *  /brief Manages a pool of objects to reuse instead of instantiate new ones.
 */
public class ObjectPooler : MonoBehaviour {

    /// Sub class container of each object type.
    [System.Serializable]
    class PoolTypes
    {
        [SerializeField]private GameObject objectToPool;                            //!< Object to create in the list.
        [SerializeField]private int amountToPool;                                   //!< Amount of copies to create.
        [SerializeField]private bool shouldExpand;                                  //!< Check if the spawned amount can grow if necessary.

        public GameObject ObjectToPool { get { return objectToPool; } }
        public int AmountToPool { get { return amountToPool; } }
        public bool ShouldExpand { get { return shouldExpand; } }
    }

    public static ObjectPooler Instance;                                            //!< Class singleton.

    [SerializeField]private PoolTypes[] poolTypes;                                  //!< Objects to create in the pool.
    
    private List<GameObject> poolList;                                              //!< List of created GameObjects.

	// Use this for initialization
	void Awake () {
        Instance = this;
	
        poolList = new List<GameObject>();

        for (int i = 0; i < poolTypes.Length; i++)
        {
            CreatePooledObjects(poolTypes[i].AmountToPool, poolTypes[i].ObjectToPool);
        }        
    }	
	
    /// Creates the objects to the pool list.
    private void CreatePooledObjects (int amountToCreate, GameObject objectToCreate)
    {        
        Transform container = transform;

        for (int i = 0; i < amountToCreate; i++)
        {
            GameObject go = Instantiate(objectToCreate) as GameObject;
            go.SetActive(false);
            go.transform.SetParent(container);
            poolList.Add(go);
        }
    }

    /// <summary>
    /// Returns the first available GameObject in the pool list.
    /// </summary>    
    public GameObject GetPooledObject (string tag)
    {
        for (int i = 0; i < poolList.Count; i++)
        {
            if (!poolList[i].activeSelf && poolList[i].CompareTag(tag))
            {
                return poolList[i];
            }
        }

        foreach (PoolTypes item in poolTypes)
        {
            if (item.ObjectToPool.CompareTag(tag))
            {
                if (item.ShouldExpand)
                {
                    CreatePooledObjects(1, item.ObjectToPool);
                    return poolList[poolList.Count - 1];
                }
            }
        }           

        return null;
    }
}
