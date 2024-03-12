using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolManager
{
	public class DefaultObjectPoolManager : ObjectPoolManager
	{
        private readonly Dictionary<int, ObjectPool> _prefabToPoolDictionary = new Dictionary<int, ObjectPool>();
        private readonly Dictionary<int, int> _instanceToPrefabsDictionary = new Dictionary<int, int>();

        public DefaultObjectPoolManager(ObjectPool prefab) : base(prefab)
        {
        }
        
        private ObjectPool FindPoolFromPrefab<T>(T prefab) where T : PoolableComponent
        {
            _prefabToPoolDictionary.TryGetValue(prefab.gameObject.GetHashCode(), out var pool);
            if (pool != null)
                return pool;
            pool = CreateNewPool(prefab.name);
            AddNewPool(prefab, pool);
            return pool;
        }
        
        private void AddNewPool(PoolableComponent prefab, ObjectPool pool)
        {
            if (pool == null)
                return;
            if (prefab == null)
                return;
            _prefabToPoolDictionary.TryAdd(prefab.gameObject.GetHashCode(), pool);
        }
        
        private ObjectPool FindPoolFromInstance<T>(T instance) where T : PoolableComponent
        {
            _instanceToPrefabsDictionary.TryGetValue(instance.gameObject.GetHashCode(), out var prefabHash);
            if (prefabHash == 0)
            {
                Debug.LogError("Hash can not be found!");
                return null;
            }
            _prefabToPoolDictionary.TryGetValue(prefabHash, out var pool);
            if (pool != null) 
                return pool;
            Debug.LogError("Pool can not be found!");
            return null;
        }
        
        public override T Spawn<T>(T prefab)
        {
            var pool = FindPoolFromPrefab(prefab);
            var obj = pool.Spawn(prefab);
            _instanceToPrefabsDictionary.TryAdd(obj.gameObject.GetHashCode(), prefab.gameObject.GetHashCode());
            return obj;
        }
        
        public override T Spawn<T>(T prefab, Transform parent)
        {
            var pool = FindPoolFromPrefab(prefab);
            var obj = pool.Spawn(prefab, parent);
            _instanceToPrefabsDictionary.TryAdd(obj.gameObject.GetHashCode(), prefab.gameObject.GetHashCode());
            return obj;
        }
        
        public override T Spawn<T>(T prefab, Transform parent, bool instantiateInWorldSpace)
        {
            var pool = FindPoolFromPrefab(prefab);
            var obj = pool.Spawn(prefab, parent, instantiateInWorldSpace);
            _instanceToPrefabsDictionary.TryAdd(obj.gameObject.GetHashCode(), prefab.gameObject.GetHashCode());
            return obj;
        }

        public override T Spawn<T>(T prefab, Vector3 position, Quaternion rotation)
        {
            var pool = FindPoolFromPrefab(prefab);
            var obj = pool.Spawn(prefab, position, rotation);
            _instanceToPrefabsDictionary.TryAdd(obj.gameObject.GetHashCode(), prefab.gameObject.GetHashCode());
            return obj;
        }
        
        public override T Spawn<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            var pool = FindPoolFromPrefab(prefab);
            var obj = pool.Spawn(prefab, position, rotation, parent);
            _instanceToPrefabsDictionary.TryAdd(obj.gameObject.GetHashCode(), prefab.gameObject.GetHashCode());
            return obj;
        }

        public override void Despawn<T>(T instance)
        {
            var pool = FindPoolFromInstance(instance);
            pool.Despawn(instance);
        }
        
        public override void ResetPoolManager()
        {
            _prefabToPoolDictionary.Clear();
            _instanceToPrefabsDictionary.Clear();
        }
    }
}