using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moonflowers.Combat
{
	public class Projectile : MonoBehaviour
	{
		//
		//
		// Other compoennets

		//
		//
		// Editor
		[Header("Particle systems")]
		[SerializeField] ParticleSystem m_SpawnPS;
		[SerializeField] ParticleSystem m_ConstantPS;
		[SerializeField] ParticleSystem m_HitPS;
		[SerializeField] Light m_Light;
		[SerializeField] GameObject m_Model;

		//
		//
		// Properties
		public float speed = 60f;
		public float damage = 25f;

		//
		//
		// Privates
		private bool isAlive = true;
		private float deathTime = 0f;

		//
		//
		// Methods
		private void Start()
		{
			deathTime = Time.time + 4f;

			if (m_SpawnPS) m_SpawnPS.Play(true);
			if (m_ConstantPS) m_ConstantPS.Play(true);
		}

		private void Update()
		{
			if (Time.time >= deathTime) StartCoroutine(DestroyCorutine());

			if (isAlive) transform.Translate(Vector3.forward * (speed * Time.deltaTime));
		}

		private void OnTriggerEnter(Collider other)
		{
			Creatures.Creature creature;
			if (other.transform.parent && other.transform.parent.TryGetComponent(out creature))
			{
				creature.TakeDamage(damage);
			}

			StartCoroutine(DestroyCorutine());
		}

		private IEnumerator DestroyCorutine()
		{
			m_Light.enabled = false;
			Destroy(m_Model);
			if (m_ConstantPS)m_ConstantPS.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			if (m_HitPS) m_HitPS.Play(true);
			isAlive = false;

			yield return new WaitForSeconds(m_HitPS.main.duration);

			Destroy(gameObject);
		}
	}
}
