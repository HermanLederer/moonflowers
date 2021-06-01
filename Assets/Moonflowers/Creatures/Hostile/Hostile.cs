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
		[SerializeField] ParticleSystem hitParticles;

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
			if (Vector3.Distance(transform.position, GameManager.instance.Player.transform.position) <= attackRadius)
			{
				GameManager.instance.Player.TakeDamage(attackDamage);
				nextAttackTime = Time.time + attackGap;
				hitParticles.Play();
				hitParticles.transform.position = Vector3.Lerp(
					transform.position,
					GameManager.instance.Player.transform.position,
					0.9f
				) + Vector3.up;
			}
		}
	}
}
