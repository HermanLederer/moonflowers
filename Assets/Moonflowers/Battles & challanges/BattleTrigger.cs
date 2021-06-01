using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moonflowers.Battles
{
	[RequireComponent(typeof(Collider))]
	public class BattleTrigger : MonoBehaviour
	{
		//
		//
		// Other compoennets
		private Collider m_Collider;

		//
		//
		// Editor

		//
		//
		// Properties

		//
		//
		// Privates

		//
		//
		// Methods
		private void Awake()
		{
			m_Collider = GetComponent<Collider>();
		}

		private void OnTriggerEnter(Collider other)
		{
			gameObject.GetComponentInParent<Battle>().Engage();
			Destroy(gameObject);
		}
	}
}
