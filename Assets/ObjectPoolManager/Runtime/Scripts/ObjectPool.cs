using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolManager
{
	public class ObjectPool : PoolableComponent
	{
		private readonly Queue<PoolableComponent> _poolObjects = new Queue<PoolableComponent>();
        
        private PoolableComponent GetObjectFromPool()
        {
            while (true)
            {
                if (_poolObjects.Count <= 0)
                    return null;
                var poolableComponent = _poolObjects.Dequeue();
                if (poolableComponent.transform.parent != null)
                {
                    return poolableComponent;
                }
            }
        }

        private void Deactivate(PoolableComponent obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform);
            obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            obj.ResetPoolObject();
        }

        private void Activate(PoolableComponent obj)
        {
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
        }

        private void Activate(PoolableComponent objectToSpawn, Transform parent)
        {
            objectToSpawn.transform.SetParent(parent);
            Activate(objectToSpawn);
        }
        
        private void Activate(PoolableComponent objectToSpawn, Transform parent, bool worldPositionStays)
        {
            objectToSpawn.transform.SetParent(parent);
            if (!worldPositionStays)
                objectToSpawn.transform.position = Vector3.zero;
            Activate(objectToSpawn);
        }

        private void Activate(PoolableComponent objectToSpawn, Vector3 position, Quaternion rotation)
        {
            objectToSpawn.transform.SetParent(null);
            objectToSpawn.transform.SetPositionAndRotation(position, rotation);
            Activate(objectToSpawn);
        }

        private void Activate(PoolableComponent objectToSpawn, Vector3 position, Quaternion rotation, Transform parent)
        {
            objectToSpawn.transform.SetParent(parent);
            Activate(objectToSpawn, position, rotation);
        }
        
        public T Spawn<T>(T obj) where T : PoolableComponent
        {
            var poolObject = GetObjectFromPool();
            if (poolObject == null)
                return Instantiate(obj);
            Activate(poolObject);
            return poolObject.GetComponent<T>();
        }

        public T Spawn<T>(T obj, Transform parent) where T : PoolableComponent
        {
            var poolObject = GetObjectFromPool();
            if (poolObject == null)
                return Instantiate(obj, parent);
            Activate(poolObject, parent);
            return poolObject.GetComponent<T>();
        }

        public T Spawn<T>(T obj, Transform parent, bool worldPositionStays) where T : PoolableComponent
        {
            var poolObject = GetObjectFromPool();
            if (poolObject == null)
                return Instantiate(obj, parent, worldPositionStays);
            Activate(poolObject, parent, worldPositionStays);
            return poolObject.GetComponent<T>();
        }

        public T Spawn<T>(T obj, Vector3 position, Quaternion rotation) where T : PoolableComponent
        {
            var poolObject = GetObjectFromPool();
            if (poolObject == null)
                return Instantiate(obj, position, rotation);
            Activate(poolObject, position, rotation);
            return poolObject.GetComponent<T>();
        }

        public T Spawn<T>(T obj, Vector3 position, Quaternion rotation, Transform parent) where T : PoolableComponent
        {
            var poolObject = GetObjectFromPool();
            if (poolObject == null)
                return Instantiate(obj, position, rotation, parent);
            Activate(poolObject, position, rotation, parent);
            return poolObject.GetComponent<T>();
        }

        public void Despawn(PoolableComponent objectToDespawn)
        {
            Deactivate(objectToDespawn);
            _poolObjects.Enqueue(objectToDespawn);
        }
	}
}