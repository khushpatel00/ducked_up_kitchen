using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
	[SerializeField] private KitchenObjectSO kitchenObjectSO;
	[SerializeField] private Transform CounterTop;
	public ClearCounter secondClearCounter;
	[SerializeField] private bool testing = false;

	private KitchenObject kitchenObject;

	public void Update()
	{
		if (kitchenObject != null & testing && Input.GetKeyDown(KeyCode.T))
		{
			kitchenObject.SetKitchenObjectParent(secondClearCounter);
		}
	}
	public void Interact(CharacterScript player)
	{
		if (kitchenObject == null)
		{
			Transform objectTransform = Instantiate(kitchenObjectSO.prefab, CounterTop);
			objectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
		}
		else
		{
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
	public void ClearKitchenObject()
	{
		kitchenObject = null;
	}
	public bool HasKitchenObject()
	{
		return kitchenObject != null;
	}
}
