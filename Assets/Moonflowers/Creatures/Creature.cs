using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
		// Editor

		//
		//
		// Getters

		//
		//
		// Properties
		public float health = 100f;
		public float maxHealth = 100f;
		public float attackDamage = 25f;

		public bool destroyOnDeath = true;
		public UnityEvent onDeath;

		//
		//
		// Privates

		//
		//
		// Methods
		protected virtual void Awake()
		{
			m_NavAgent = GetComponent<NavMeshAgent>();
			onDeath = new UnityEvent();
		}

		public void TakeDamage(float damage)
		{
			health -= damage;
			if (health <= 0)
			{
				health = 0;
				Kill();
			}
		}

		public void Kill()
		{
			onDeath.Invoke();
			
			if (destroyOnDeath) Destroy(gameObject);
		}

		private void OnDestroy()
		{
			onDeath.RemoveAllListeners();
		}
	}
}

