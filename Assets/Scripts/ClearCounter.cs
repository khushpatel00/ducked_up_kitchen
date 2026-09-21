using UnityEngine;

public class ClearCounter : MonoBehaviour
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
			kitchenObject.SetClearCounter(secondClearCounter);
		}
	}
	public void Interact()
	{
		if (kitchenObject == null)
		{
			Transform objectTransform = Instantiate(kitchenObjectSO.prefab, CounterTop);
			objectTransform.localPosition = Vector3.zero;

			kitchenObject = objectTransform.GetComponent<KitchenObject>();
			kitchenObject.SetClearCounter(this);
		}
		else
		{
			Debug.Log(kitchenObject.GetClearCounter());
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
