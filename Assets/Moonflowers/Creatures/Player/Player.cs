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
		// editor
		[SerializeField] LayerMask navigationLayers;
		[SerializeField] LayerMask interactionLayers;

		//
		//
		// Properties
		public float attackDamage = 25f;

		//
		//
		// Privates
		private Camera m_Cam;

		private bool m_IsNavigating = false;
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
				RaycastHit navHit;
				if (NavigationRaycast(out navHit))
				{
					m_NavAgent.SetDestination(navHit.point);
				}
			}
		}

		private void OnInteract(InputValue value)
		{
			// Mouse down
			if (value.Get<float>() > 0f)
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
				else m_IsNavigating = true;
			}
			// Mosue up
			else
			{
				m_IsNavigating = false;
			}
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

		private void Jump()
		{
			Debug.Log("Jump");
		}
	}
}

