using System;
using UnityEngine;
using Zenject;

namespace ObjectPoolManager
{
	[RequireComponent(typeof(GameObjectContext))]
	[RequireComponent(typeof(ZenAutoInjecter))]
	public class PoolableComponent : MonoBehaviour
	{
		public event Action<PoolableComponent> ResettedPoolObject;
        
		protected virtual void Reset()
		{
			GetComponent<ZenAutoInjecter>().ContainerSource = ZenAutoInjecter.ContainerSources.SceneContext;
		}

		public virtual void ResetPoolObject()
		{
			ResettedPoolObject?.Invoke(this);
		}
	}
}