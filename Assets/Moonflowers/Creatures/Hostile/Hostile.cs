using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moonflowers.Creatures
{
	public class Hostile : Creature
	{
		//
		//
		// Editor
		[SerializeField] GameObject magickPickupPrefab;

		//
		//
		// Properties
		public float attackRadius = 1f;
		public float attackGap = 1f;

		//
		//
		// Privates
		private float nextAttackTime = 0f;

		private void Update()
		{
			m_NavAgent.SetDestination(GameManager.instance.Player.transform.position);
			if (Time.time >= nextAttackTime) AttackPlayer();
		}

		private void AttackPlayer()
		{
			if (Vector3.Distance(transform.position, GameManager.instance.Player.transform.position) <= attackRadius) {
				GameManager.instance.Player.TakeDamage(attackDamage);
				nextAttackTime = Time.time + attackGap;
			}
		}
	}
}
