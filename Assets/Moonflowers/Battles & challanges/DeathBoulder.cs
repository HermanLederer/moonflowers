using System.Collections;
using UnityEngine;

namespace Moonflowers.Challanges
{
	public class DeathBoulder : MonoBehaviour
	{
		//
		//
		// Other compoennets
		Rigidbody rb;

		//
		//
		// Editor

		//
		//
		// Properties
		public float respanwDelay = 6f;

		//
		//
		// Privates
		private Vector3 startPosition;
		private float nextRespawnTime = float.MaxValue;

		//
		//
		// Methods
		private void Awake()
		{
			rb = GetComponent<Rigidbody>();

			rb.isKinematic = true;
			StartCoroutine(UnfreezeCorutine());
		}

		private void Start()
		{
			startPosition = transform.position;
		}

		private void Update()
		{
			if (Time.time >= nextRespawnTime) Respawn();
		}

		public void OnTriggerEnter(Collider other)
		{
			Creatures.Creature creature;
			if (other.transform.parent && other.transform.parent.TryGetComponent(out creature))
			{
				creature.TakeDamage(12000);
			}
		}

		public void Respawn()
		{
			nextRespawnTime = Time.time + respanwDelay;
			transform.position = startPosition;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}

		private IEnumerator UnfreezeCorutine()
		{
			yield return new WaitForSeconds(Random.Range(0f, respanwDelay));

			rb.isKinematic = false;
			Respawn();
		}
	}
}
