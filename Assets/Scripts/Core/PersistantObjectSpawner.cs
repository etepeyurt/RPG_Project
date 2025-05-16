using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.Core{ 
public class PersistantObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject persistentGameObjectPrefab;
        static bool hasSpawned = false;
        private void Awake()
        {
            if (hasSpawned) return;
            spawnPersistentObject();
            hasSpawned = true;
        }
        void spawnPersistentObject()
        {
            GameObject persistantObject = Instantiate(persistentGameObjectPrefab);
            DontDestroyOnLoad(persistantObject);
        }
    }
}