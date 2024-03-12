using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ObjectPoolManager.Samples.Scripts
{
	public class BallSpawner : MonoBehaviour
	{
		[Inject] private ObjectPoolManager _objectPoolManager;
		[SerializeField] private Ball _ball;
		private Queue<Ball> _spawnedBalls = new Queue<Ball>();

		private void Start()
		{
			StartCoroutine(SpawnBalls());
		}

		private IEnumerator SpawnBalls()
		{
			while (true)
			{
				_objectPoolManager.Spawn(_ball, Vector3.zero, Quaternion.identity);
				yield return new WaitForSeconds(1f);
			}
		}
	}
}