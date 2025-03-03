﻿// This code is taken from the following GitHub repository:
// Author: Peter Cardwell-Gardner  (thefuntastic)
// Repository: [https://github.com/thefuntastic/unity-object-pool]
// License: MIT License
//
// MIT License
// 
// Copyright (c)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Pool
{
    public class PoolManager : Singleton<PoolManager>
    {
        public bool logStatus;
        public Transform root;

        private Dictionary<GameObject, ObjectPool<GameObject>> prefabLookup;
        private Dictionary<GameObject, ObjectPool<GameObject>> instanceLookup;

        private bool dirty = false;

        void Awake()
        {
            prefabLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
            instanceLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
        }

        void Update()
        {
            if (logStatus && dirty)
            {
                PrintStatus();
                dirty = false;
            }
        }

        public void warmPool(GameObject prefab, int size)
        {
            if (prefabLookup.ContainsKey(prefab))
            {
                throw new Exception("Pool for prefab " + prefab.name + " has already been created");
            }
            var pool = new ObjectPool<GameObject>(() => { return InstantiatePrefab(prefab); }, size);
            prefabLookup[prefab] = pool;

            dirty = true;
        }

        public GameObject spawnObject(GameObject prefab)
        {
            return spawnObject(prefab, Vector3.zero, Quaternion.identity);
        }

        public GameObject spawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!prefabLookup.ContainsKey(prefab))
            {
                WarmPool(prefab, 1);
            }

            var pool = prefabLookup[prefab];

            var clone = pool.GetItem();
            clone.transform.SetPositionAndRotation(position, rotation);
            clone.SetActive(true);

            instanceLookup.Add(clone, pool);
            dirty = true;
            return clone;
        }

        public void releaseObject(GameObject clone)
        {
            clone.SetActive(false);

            if (instanceLookup.ContainsKey(clone))
            {
                instanceLookup[clone].ReleaseItem(clone);
                instanceLookup.Remove(clone);
                dirty = true;
            }
            else
            {
                Debug.LogWarning("No pool contains the object: " + clone.name);
            }
        }


        private GameObject InstantiatePrefab(GameObject prefab)
        {
            var go = Instantiate(prefab) as GameObject;
            if (root != null) go.transform.parent = root;
            return go;
        }

        public void PrintStatus()
        {
            foreach (KeyValuePair<GameObject, ObjectPool<GameObject>> keyVal in prefabLookup)
            {
                Debug.Log(string.Format("Object Pool for Prefab: {0} In Use: {1} Total {2}", keyVal.Key.name, keyVal.Value.CountUsedItems, keyVal.Value.Count));
            }
        }

        #region Static API

        public static void WarmPool(GameObject prefab, int size)
        {
            Instance.warmPool(prefab, size);
        }

        public static GameObject SpawnObject(GameObject prefab)
        {
            return Instance.spawnObject(prefab);
        }

        public static GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return Instance.spawnObject(prefab, position, rotation);
        }

        public static void ReleaseObject(GameObject clone)
        {
            Instance.releaseObject(clone);
        }

        public static void SetRoot(Transform transform)
        {
            Instance.root = transform;
        }

        #endregion
    }
}
