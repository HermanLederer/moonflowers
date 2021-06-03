using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moonflowers
{
	public class Finish : MonoBehaviour
	{
		//
		//
		// Other compoennets

		//
		//
		// Editor
		[SerializeField] Transform m_ExitPosition;
		[SerializeField] Transform m_CameraTarget;

		//
		//
		// Properties

		//
		//
		// Privates

		//
		//
		// Methods
		private void OnTriggerEnter(Collider other)
		{
			GameManager.instance.NavigatePlayer(m_ExitPosition.position, 1f);
			GameManager.instance.SetCameraTarget(m_CameraTarget, 8f);
			GameManager.instance.Win();
		}
	}
}
