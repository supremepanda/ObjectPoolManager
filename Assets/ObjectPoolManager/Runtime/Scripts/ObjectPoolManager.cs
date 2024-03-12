using UnityEngine;

namespace ObjectPoolManager
{
	public abstract class ObjectPoolManager
	{
		private readonly ObjectPool _poolPrefab;
		
		protected ObjectPoolManager(ObjectPool prefab)
		{
			_poolPrefab = prefab;
		}
		
		protected ObjectPool CreateNewPool(string name)
		{
			var pool = Object.Instantiate(_poolPrefab);
			pool.name = $"Pool_{name}";
			return pool;
		}
		
		public abstract T Spawn<T>(T prefab) where T : PoolableComponent;
		public abstract T Spawn<T>(T prefab, Transform parent) where T : PoolableComponent;
		public abstract T Spawn<T>(T prefab, Transform parent, bool instantiateInWorldSpace) where T : PoolableComponent;
		public abstract T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : PoolableComponent;
		public abstract T Spawn<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : PoolableComponent;
		public abstract void Despawn<T>(T instance) where T : PoolableComponent;
		public abstract void ResetPoolManager();
	}
}