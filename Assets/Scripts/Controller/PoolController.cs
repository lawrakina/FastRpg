using System.Collections.Generic;
using Interface;
using Model;
using Model.Weapons;
using UnityEngine;


namespace Controller
{
    public class PoolController 
    {
        #region Fields

        private Dictionary<string, LinkedList<BaseObjectScene>> _poolsBaseObjectSceneDictionary;
        // private Dictionary<string, LinkedList<BaseHit>> _poolsBaseHitDictionary;
        private Transform _deactivatedObjectsParent;

        #endregion


        #region Initial

        public void Init(Transform pooledObjectsContainer)
        {
            // Debug.Log($"PoolController.Init; _poolsDictionary Create new Dictionary<string, LinkedList<BaseObjectScene>>()");
            _deactivatedObjectsParent = pooledObjectsContainer;
            _poolsBaseObjectSceneDictionary = new Dictionary<string, LinkedList<BaseObjectScene>>();
            // _poolsBaseHitDictionary = new Dictionary<string, LinkedList<BaseHit>>();
        }
        
        #endregion

        
        #region BaseObjectScene

        public BaseObjectScene GetFromPool(BaseObjectScene prefab)
        {
            // Debug.Log($"_poolsDictionary: {_poolsDictionary.Count}");
            if (!_poolsBaseObjectSceneDictionary.ContainsKey(prefab.name))
            {
                // Debug.Log($"Create new LinkedList<BaseObjectScene>");
                _poolsBaseObjectSceneDictionary[prefab.name] = new LinkedList<BaseObjectScene>();
            }

            BaseObjectScene result;

            if (_poolsBaseObjectSceneDictionary[prefab.name].Count > 0)
            {
                // Debug.Log($"_poolsDictionary[{prefab.name}].Count = {_poolsDictionary.Count}");
                result = _poolsBaseObjectSceneDictionary[prefab.name].First.Value;
                _poolsBaseObjectSceneDictionary[prefab.name].RemoveFirst();
                result.SetDefault();
                result.SetActive(true);
                // Debug.Log($"return {result.name}");
                return result;
            }

            // Debug.Log($"Create new BaseObjectScene.Instantiate(prefab): {prefab.name}");
            result = BaseObjectScene.Instantiate(prefab);
            result.name = prefab.name;

            return result;
        }

        public void PutToPool(BaseObjectScene target)
        {
            _poolsBaseObjectSceneDictionary[target.name].AddFirst(target);
            target.transform.parent = _deactivatedObjectsParent;
            target.SetActive(false);
            // Debug.Log($"PoolController.PutToPool; pool.Count: {_poolsDictionary[target.name].Count}");
        }

        #endregion

        //
        // #region BaseHit
        //
        // public BaseHit GetFromPool(BaseHit prefab)
        // {
        //     // Debug.Log($"_poolsDictionary: {_poolsDictionary.Count}");
        //     if (!_poolsBaseHitDictionary.ContainsKey(prefab.name))
        //     {
        //         // Debug.Log($"Create new LinkedList<BaseObjectScene>");
        //         _poolsBaseHitDictionary[prefab.name] = new LinkedList<BaseHit>();
        //     }
        //
        //     BaseHit result;
        //
        //     if (_poolsBaseHitDictionary[prefab.name].Count > 0)
        //     {
        //         // Debug.Log($"_poolsDictionary[{prefab.name}].Count = {_poolsDictionary.Count}");
        //         result = _poolsBaseHitDictionary[prefab.name].First.Value;
        //         _poolsBaseHitDictionary[prefab.name].RemoveFirst();
        //         result.SetDefault();
        //         result.SetActive(true);
        //         // Debug.Log($"return {result.name}");
        //         return result;
        //     }
        //
        //     // Debug.Log($"Create new BaseObjectScene.Instantiate(prefab): {prefab.name}");
        //     result = BaseHit.Instantiate(prefab);
        //     result.name = prefab.name;
        //
        //     return result;
        // }
        //
        // public void PutToPool(BaseHit target)
        // {
        //     _poolsBaseHitDictionary[target.name].AddFirst(target);
        //     target.transform.parent = _deactivatedObjectsParent;
        //     target.SetActive(false);
        //     // Debug.Log($"PoolController.PutToPool; pool.Count: {_poolsDictionary[target.name].Count}");
        // }
        //
        // #endregion
    }
}