using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Moonflowers.Creatures
{
	[RequireComponent(typeof(NavMeshAgent))]
	public abstract class Creature : MonoBehaviour
	{
		//
		//
		// Other compoennets
		protected NavMeshAgent m_NavAgent;

		//
		//
		// Properties

		//
		//
		// Privates

		//
		//
		// Methods
		protected virtual void Awake()
		{
			m_NavAgent = GetComponent<NavMeshAgent>();
		}
	}
}
