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
		[SerializeField] GameObject projectilePrefab;
		[SerializeField] LayerMask navigationLayers;
		[SerializeField] LayerMask interactionLayers;

		//
		//
		// Properties
		public float magick = 0f;
		public float maxMagick = 100f;
		public float magickRegeneration = 120f;
		public float magickCost = 48f;

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
			magick += magickRegeneration * Time.deltaTime;
			if (magick > maxMagick) magick = maxMagick;

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
				Hostile hostile;
				//if (parent.TryGetComponent(out hostile))
				//{
				//	//
				//	// Hostile clicked
				//}
				if (navHit.transform.parent && navHit.transform.parent.TryGetComponent<Player>(out _))
				{
					//
					// Area attack / jump
					Jump();
				}
				else
				{
					//
					// Attack
					if (magick > magickCost)
					{
						var target = navHit.point;
						var origin = transform.position + Vector3.up * 1.2f;
						var direction = (target - origin).normalized;

						var pO = Instantiate(projectilePrefab, origin + direction, Quaternion.identity);
						// var p = pO.GetComponent<Combat.Projectile>();
						pO.transform.LookAt(navHit.point);
						magick -= magickCost;
					}
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
			return Physics.Raycast(navRay, out navHit, 128f);
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

