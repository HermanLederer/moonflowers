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
		[SerializeField] int[] m_Waves;
		[SerializeField] Transform m_SpawnPositions;
		[SerializeField] GameObject m_EnemyPrefab;
		[SerializeField] Transform m_CameraTarget;
		[SerializeField] float m_CameraTargetRadius;

		//
		//
		// Properties

		//
		//
		// Privates
		private int nextWave = 0;
		private int hostilesAlive = 0;

		//
		//
		// Methods
		public void Engage() {
			if (NextWave())
			{
				if (m_CameraTarget != null)
					GameManager.instance.SetCameraTarget(m_CameraTarget, m_CameraTargetRadius);
			}
		}

		private bool NextWave()
		{
			if (nextWave < m_Waves.Length)
			{
				for (int i = 0; i <= m_Waves[nextWave]; ++i)
				{
					var pos = m_SpawnPositions.GetChild(Mathf.FloorToInt(Random.Range(0, m_SpawnPositions.childCount - 1)));
					++hostilesAlive;
					Creature c = Instantiate(m_EnemyPrefab, pos.position, pos.rotation, transform).GetComponent<Creature>();
					c.onDeath.AddListener(hostileDead);
				}

				++nextWave;

				return true;
			}

			return false;
		}
		
		public void EndBattle()
		{
			GameManager.instance.RemoveCameraTarget();
		}

		private void hostileDead()
		{
			--hostilesAlive;
			if (hostilesAlive == 0)
			{
				if (!NextWave()) EndBattle();
			}
		}
	}
}

