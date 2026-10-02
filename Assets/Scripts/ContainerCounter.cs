using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

	public event EventHandler OnPlayerGrabbed;
	[SerializeField] private KitchenObjectSO kitchenObjectSO;

	public override void Interact(CharacterScript player)
	{
		Debug.Log("Initializing Stages");
		if (player.HasKitchenObject())
			player.ClearSelfKitchenObject();
		Debug.Log("Stage 1 Cleared");
		Transform objectTransform = Instantiate(kitchenObjectSO.prefab, CounterTop);
		Debug.Log("Stage 2 Cleared");
		objectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
		Debug.Log("Stage 3 Cleared");
		OnPlayerGrabbed?.Invoke(this, EventArgs.Empty);
		Debug.Log("Stage 4 Cleared");
	}
}
