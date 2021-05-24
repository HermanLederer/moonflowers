using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Moonflowers
{
	public class GameManager : MonoBehaviour
	{
		//
		//
		// Other compoennets

		//
		//
		// Editor
		[SerializeField] CinemachineTargetGroup cmTartgets;

		//
		//
		// Properties

		//
		//
		// Privates

		//
		//
		// Singleton
		public static GameManager instance;

		//
		//
		// Methods
		public void Awake()
		{
			if (instance != null)
			{
				Destroy(gameObject);
				Debug.LogError("Cannot have more than one instance of " + name);
				return;
			}

			instance = this;
		}

		public void SetCameraTarget(Transform pos, float radius)
		{
			var target = new CinemachineTargetGroup.Target();
			target.target = pos;
			target.radius = radius;
			cmTartgets.m_Targets[1] = target;
			StartCoroutine(FadeCameraTarget());
		}

		public void RemoveCameraTarget()
		{
			StartCoroutine(FadeCameraTarget(false));
		}

		private IEnumerator FadeCameraTarget(bool fadeIn = true)
		{
			const float weight = 3f;
			const float speed = 3f;

			if (fadeIn)
				while (cmTartgets.m_Targets[1].weight < weight)
				{
					cmTartgets.m_Targets[1].weight += Time.deltaTime * speed;
					if (cmTartgets.m_Targets[1].weight > 3) cmTartgets.m_Targets[1].weight = weight;
					yield return null;
				}
			else
				while (cmTartgets.m_Targets[1].weight > 0)
				{
					cmTartgets.m_Targets[1].weight -= Time.deltaTime * speed;
					if (cmTartgets.m_Targets[1].weight < 0) cmTartgets.m_Targets[1].weight = 0;
					yield return null;
				}
		}
	}
}

