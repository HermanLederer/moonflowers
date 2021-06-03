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
		protected bool m_IsDead = false;

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
			if (m_IsDead) return;

			health -= damage;
			if (health <= 0)
			{
				health = 0;
				Kill();
			}
		}

		public void Kill()
		{
			m_IsDead = true;
			onDeath.Invoke();
			if (destroyOnDeath) Destroy(gameObject);
		}

		private void OnDestroy()
		{
			onDeath.RemoveAllListeners();
		}
	}
}

