using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Moonflowers.Creatures;

namespace Moonflowers.Battles
{
	public class Battle : MonoBehaviour
	{
		//
		//
		// Other compoennets

		//
		//
		// Editor
		[SerializeField] List<Transform> m_SpawnPositions;
		[SerializeField] GameObject m_EnemyPrefab;
		[SerializeField] Transform m_CameraTarget;
		[SerializeField] float m_CameraTargetRadius;

		//
		//
		// Properties

		//
		//
		// Privates
		private int hostilesAlive = 0;

		//
		//
		// Methods
		public void Engage() {
			if (m_CameraTarget != null)
				GameManager.instance.SetCameraTarget(m_CameraTarget, m_CameraTargetRadius);

			m_SpawnPositions.ForEach((Transform pos) => {
				++hostilesAlive;
				Creature c = Instantiate(m_EnemyPrefab, pos.position, pos.rotation).GetComponent<Creature>();
				c.onDeath.AddListener(hostileDead);
			});
		}

		public void EndBattle()
		{
			GameManager.instance.RemoveCameraTarget();
		}

		private void hostileDead()
		{
			--hostilesAlive;
			if (hostilesAlive == 0) EndBattle();
		}
	}
}

