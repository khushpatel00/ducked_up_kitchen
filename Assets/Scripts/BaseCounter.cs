using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
	[SerializeField] protected Transform CounterTop;
	protected KitchenObject kitchenObject; 
	public virtual void Interact(CharacterScript player)
	{
		Debug.LogError("Invalid Call: BaseCounter.Interact()");
	}
	public virtual void InteractAlternate(CharacterScript player)
	{
		Debug.LogError("Invalid Call: BaseCounter.InteractAlternate()");
	}
	public void ClearKitchenObject(GameObject gameObject)
	{
		if (kitchenObject.gameObject != gameObject)
			Debug.Log("Invalid Call" + kitchenObject.gameObject + " " + gameObject);
		else kitchenObject = null;
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
	public bool HasKitchenObject()
	{
		return kitchenObject != null;
	}
	public void ClearSelfKitchenObject()
    {
        kitchenObject = null;
    }
	
}