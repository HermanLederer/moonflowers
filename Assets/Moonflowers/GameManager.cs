using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

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
		[SerializeField] Creatures.Player player;
		[SerializeField] UI.UI ui;

		//
		//
		// Getters
		public Creatures.Player Player { get => player; }
		public UI.UI UI { get => ui; }

		//
		//
		// Privates

		//
		//
		// *Singleton*
		public static GameManager instance;

		//
		//
		// Methods
		public void Awake()
		{
			if (instance != null)
			{
				Destroy(gameObject);
				Debug.LogError("Cannot have more than one instance of " + ToString());
				return;
			}

			instance = this;
		}

		public void Start()
		{
			player.onDeath.AddListener(Loose);
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
			const float speedIn = 2f;
			const float speedOut = 1f;

			if (fadeIn)
				while (cmTartgets.m_Targets[1].weight < weight)
				{
					cmTartgets.m_Targets[1].weight += Time.deltaTime * speedIn;
					if (cmTartgets.m_Targets[1].weight > 3) cmTartgets.m_Targets[1].weight = weight;
					yield return null;
				}
			else
				while (cmTartgets.m_Targets[1].weight > 0)
				{
					cmTartgets.m_Targets[1].weight -= Time.deltaTime * speedOut;
					if (cmTartgets.m_Targets[1].weight < 0) cmTartgets.m_Targets[1].weight = 0;
					yield return null;
				}
		}

		public void NavigatePlayer(Vector3 destination, float freezeTime)
		{
			player.Navigate(destination, freezeTime);
		}

		public void Loose()
		{
			ui.DeathScreen();
		}

		public void Win()
		{
			ui.VictoryScreen();
		}

		public void Restart()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void Exit()
		{
			Application.Quit();
		}
	}
}

