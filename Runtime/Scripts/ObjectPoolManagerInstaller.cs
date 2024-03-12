using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ObjectPoolManager
{
	public class ObjectPoolManagerInstaller : MonoInstaller<ObjectPoolManagerInstaller>
	{
		[SerializeField] private ObjectPool _prefab;
		[SerializeField] private bool _resetOnSceneChange = true;
		private ObjectPoolManager _objectPoolManager;

		private void Awake()
		{
			if (!_resetOnSceneChange)
				return;
			SceneManager.activeSceneChanged += SceneManager_ActiveSceneChanged;
		}

		private void SceneManager_ActiveSceneChanged(Scene arg0, Scene arg1)
		{
			_objectPoolManager.ResetPoolManager();
		}

		private void OnDestroy()
		{
			if (!_resetOnSceneChange)
				return;
			SceneManager.activeSceneChanged -= SceneManager_ActiveSceneChanged;
		}

		public override void InstallBindings()
		{
			_objectPoolManager = new DefaultObjectPoolManager(_prefab);
			Container.Bind<ObjectPoolManager>().FromInstance(_objectPoolManager).AsSingle()
				.NonLazy();
		}
	}
}