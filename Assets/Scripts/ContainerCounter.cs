using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{

	[SerializeField] private KitchenObjectSO kitchenObjectSO;
	[SerializeField] private Transform CounterTop;
	private KitchenObject kitchenObject;
	private GameObject currentGameObject;

	public override void Interact(CharacterScript player)
	{
		if (kitchenObject == null) // add object on Counter
		{
			Transform objectTransform = Instantiate(kitchenObjectSO.prefab, CounterTop);
			objectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
			currentGameObject = objectTransform.gameObject;
		}
		else // take from the Counter
		{
			if (player.HasKitchenObject())
			{
				player.ClearSelfKitchenObject();
			}
			player.SetKitchenObject(kitchenObject);
			kitchenObject.SetKitchenObjectParent(player);
		}
	}

	public Transform GetKitchenObjectFollowTransform()
	{
		return CounterTop;
	}
	public void SetKitchenObject(KitchenObject kitchenObject)
	{
		this.kitchenObject = kitchenObject;
	}
	public KitchenObject GetKitchenObject()
	{
		return kitchenObject;
	}
	public void ClearKitchenObject(GameObject gameObject)
	{
		kitchenObject.DestroyKitchenObject(gameObject);
		kitchenObject = null;
	}
	public bool HasKitchenObject()
	{
		return kitchenObject != null;
	}

}
