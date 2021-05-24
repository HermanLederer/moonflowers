using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Moonflowers.Creatures
{
	public class Player : Creature
	{
		//
		//
		// Other compoennets

		//
		//
		// Properties

		//
		//
		// Privates
		private Camera m_Cam;

		private bool m_IsNavigating = false;
		private Vector3 m_NavPoint;
		private Vector2 m_MousePos;

		//
		//
		// Methods
		protected override void Awake()
		{
			base.Awake();

			m_Cam = Camera.main;
		}

		private void Update()
		{
			if (m_IsNavigating)
			{
				Ray navRay = m_Cam.ScreenPointToRay(m_MousePos);
				RaycastHit navHit;
				if (Physics.Raycast(navRay, out navHit))
				{
					Debug.Log(m_NavAgent.SetDestination(navHit.point));
				}
			}
		}

		private void OnNavigate(InputValue value)
		{
			m_IsNavigating = value.Get<float>() > 0f;
		}

		private void OnNavigation(InputValue value)
		{
			m_MousePos = value.Get<Vector2>();
		}
	}
}
