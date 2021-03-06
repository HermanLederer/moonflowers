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
		[SerializeField] Transform m_StartPosition;
		[SerializeField] Transform m_SpawnPositions;
		[SerializeField] GameObject m_DarkSpiritPrefab;
		[SerializeField] GameObject m_FastSpiritPrefab;
		[SerializeField] GameObject m_FatSpiritPrefab;
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
			if (NextWave(1f))
			{
				GameManager.instance.NavigatePlayer(m_StartPosition.position, 1f);
				GameManager.instance.SetCameraTarget(m_CameraTarget, m_CameraTargetRadius);
			}
		}

		private bool NextWave(float delay = 0f)
		{
			if (nextWave < m_Waves.Length)
			{
				StartCoroutine(NextWaveCorutine(delay, 0.1f));
				return true;
			}

			return false;
		}

		private IEnumerator NextWaveCorutine(float delay, float gap)
		{
			yield return new WaitForSeconds(delay);

			for (int i = 0; i < m_Waves[nextWave]; ++i)
			{
				var spirit = m_DarkSpiritPrefab;
				var random = Random.Range(0f, 1f);
				if (random > 0.8f) spirit = m_FastSpiritPrefab;
				if (random < 0.1f) spirit = m_FatSpiritPrefab;

				var pos = m_SpawnPositions.GetChild(Mathf.FloorToInt(Random.Range(0, m_SpawnPositions.childCount - 1)));
				
				Creature c = Instantiate(spirit, pos.position, pos.rotation, transform).GetComponent<Creature>();
				c.onDeath.AddListener(HostileDead);
				++hostilesAlive;

				yield return new WaitForSeconds(gap);
			}

			++nextWave;
		}
		
		public void EndBattle()
		{
			GameManager.instance.RemoveCameraTarget();
		}

		private void HostileDead()
		{
			--hostilesAlive;
			if (hostilesAlive == 0)
			{
				if (!NextWave()) EndBattle();
			}
		}
	}
}

