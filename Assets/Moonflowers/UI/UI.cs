using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Moonflowers.UI
{
	public class UI : MonoBehaviour
	{
		//
		//
		// Other compoennets

		//
		//
		// Editor
		[SerializeField] Volume m_Volume;
		[SerializeField] GameObject m_WelcomeScreen;
		[SerializeField] GameObject m_DeathScreen;
		[SerializeField] GameObject m_VictoryScreen;

		//
		//
		// Properties
		public float fadeDuration;

		//
		//
		// Privates
		private float m_TargetWeight;
		private GameObject m_CurrentScreen;

		//
		//
		// Methods
		private void Start()
		{
			m_CurrentScreen = m_WelcomeScreen;
			m_WelcomeScreen.SetActive(true);
			m_Volume.weight = 1f;
			m_TargetWeight = 1f;
		}

		private void Update()
		{
			UpdateVolumeFade(m_Volume, m_TargetWeight);
		}

		private void UpdateVolumeFade(Volume volume, float targetWeight)
		{
			if (volume.weight < targetWeight)
			{
				volume.weight += Time.deltaTime / fadeDuration;
				if (volume.weight > targetWeight) volume.weight = targetWeight;
			}
			else if (volume.weight > targetWeight)
			{
				volume.weight -= Time.deltaTime / fadeDuration;
				if (volume.weight < targetWeight) volume.weight = targetWeight;
			}
		}

		public void NoScreen()
		{
			m_TargetWeight = 0f;
			m_CurrentScreen.SetActive(false);
			m_CurrentScreen = null;
		}

		public void WelcomeScreen() => StartCoroutine(ScreenCorutine(m_WelcomeScreen));
		public void DeathScreen() => StartCoroutine(ScreenCorutine(m_DeathScreen));
		public void VictoryScreen() => StartCoroutine(ScreenCorutine(m_VictoryScreen));

		private IEnumerator ScreenCorutine(GameObject screen)
		{
			if (m_CurrentScreen != null)
			{
				m_CurrentScreen.SetActive(false);
				yield return null;
			}
			else
			{
				m_TargetWeight = 1f;
				yield return new WaitForSeconds(fadeDuration);
			}
			
			screen.SetActive(true);
			m_CurrentScreen = screen;
		}
	}
}
