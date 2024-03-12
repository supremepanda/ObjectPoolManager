using UnityEngine;
using Zenject;

namespace ObjectPoolManager.Samples.Scripts
{
	public class Ball : PoolableComponent
	{
		private const float INITIAL_SPEED = 2f;
		private const float MAX_SPEED = 10f;
		private const float DESTROY_TIME = 5f;
		[Inject] private ObjectPoolManager _objectPoolManager;
		private float _currentSpeed;
		private float _timeToDestroy;

		private void Awake()
		{
			_currentSpeed = INITIAL_SPEED;
			_timeToDestroy = DESTROY_TIME;
		}

		private void Update()
		{
			_currentSpeed = Mathf.Clamp(_currentSpeed + Time.deltaTime * 5f, INITIAL_SPEED, MAX_SPEED);
			transform.position += Vector3.forward * Time.deltaTime;
			_timeToDestroy -= Time.deltaTime;
			if (_timeToDestroy <= 0)
			{
				_objectPoolManager.Despawn(this);
			}
		}
		

		public override void ResetPoolObject()
		{
			base.ResetPoolObject();
			_currentSpeed = INITIAL_SPEED;
			_timeToDestroy = DESTROY_TIME;
		}
	}
}