using UnityEngine;
using UnityEngine.UI;

namespace Moonflowers.Creatures
{
	public class PlayerUI : CreatureUI
	{
		//
		//
		// Other compoennets

		//
		//
		// Editor
		[SerializeField] protected Slider magickBar;

		//
		//
		// Properties

		//
		//
		// Privates

		//
		//
		// *Parent creature*
		protected Player parentPlayer;

		//
		//
		// Methods
		protected override void Awake()
		{
			base.Awake();
			parentPlayer = transform.GetComponentInParent<Player>();
		}

		protected override void Update()
		{
			base.Update();
			magickBar.value = parentPlayer.magick / parentPlayer.maxMagick;
		}
	}
}

