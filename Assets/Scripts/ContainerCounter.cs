using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

	public event EventHandler OnPlayerGrabbed;
	[SerializeField] private KitchenObjectSO kitchenObjectSO;

	public override void Interact(CharacterScript player)
	{
		if (player.HasKitchenObject())
			player.ClearSelfKitchenObject();
		Transform objectTransform = Instantiate(kitchenObjectSO.prefab, CounterTop);
		objectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
		Debug.Log("Interacting");
		OnPlayerGrabbed?.Invoke(this, EventArgs.Empty);
	}
}
