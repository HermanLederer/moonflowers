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
		// Editor
		[SerializeField] LayerMask navigationLayers;
		[SerializeField] LayerMask interactionLayers;

		//
		//
		// Properties

		//
		//
		// Privates
		private Camera m_Cam;

		private bool m_IsNavigating = false;
		private bool m_IsLocked = false;
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
			if (!m_IsLocked && m_IsNavigating)
			{
				RaycastHit navHit;
				if (NavigationRaycast(out navHit))
				{
					m_NavAgent.SetDestination(navHit.point);
				}
			}
		}

		private void OnInteract()
		{
			RaycastHit navHit;
			if (InteactionRaycast(out navHit))
			{
				var parent = navHit.transform.parent.gameObject;
				Hostile hostile;
				if (parent.TryGetComponent(out hostile))
				{
					//
					// Attack
					hostile.TakeDamage(attackDamage);
				}
				else if (parent.TryGetComponent<Player>(out _))
				{
					//
					// Area attack / jump
					Jump();
				}
			}
		}

		private void OnNavigate(InputValue value)
		{
			m_IsNavigating = value.Get<float>() > 0f;
		}

		private void OnHover(InputValue value)
		{
			m_MousePos = value.Get<Vector2>();
		}

		private bool NavigationRaycast(out RaycastHit navHit)
		{
			Ray navRay = m_Cam.ScreenPointToRay(m_MousePos);
			return Physics.Raycast(navRay, out navHit, 128f, navigationLayers);
		}

		private bool InteactionRaycast(out RaycastHit navHit)
		{
			Ray navRay = m_Cam.ScreenPointToRay(m_MousePos);
			return Physics.Raycast(navRay, out navHit, 128f, interactionLayers);
		}

		public void Navigate(Vector3 destination, float freezeTime)
		{
			StartCoroutine(NavigateCorutine(destination, freezeTime));
		}

		private IEnumerator NavigateCorutine(Vector3 destination, float freezeTime)
		{
			Lock();
			m_NavAgent.SetDestination(destination);
			yield return new WaitForSeconds(freezeTime);
			Unock();
		}

		private void Lock()
		{
			m_IsLocked = true;
		}

		private void Unock()
		{
			m_IsLocked = false;
		}

		private void Jump()
		{
			Debug.Log("Jump");
		}
	}
}

