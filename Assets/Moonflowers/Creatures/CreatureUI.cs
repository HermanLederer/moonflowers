using UnityEngine;
using UnityEngine.UI;

namespace Moonflowers.Creatures
{
	public class CreatureUI : MonoBehaviour
	{
		//
		//
		// Other compoennets

		//
		//
		// Editor
		[SerializeField] protected Slider healthBar;

		//
		//
		// Properties

		//
		//
		// Privates

		//
		//
		// *Parent creature*
		protected Creature parentCreature;

		//
		//
		// Methods
		protected virtual void Awake()
		{
			parentCreature = transform.GetComponentInParent<Creature>();
		}

		protected virtual void Update()
		{
			healthBar.value = parentCreature.health / parentCreature.maxHealth;

			var euler = Camera.main.transform.rotation.eulerAngles;
			euler.x = 0;
			euler.z = 0;
			transform.rotation = Quaternion.Euler(euler);
		}
	}
}

