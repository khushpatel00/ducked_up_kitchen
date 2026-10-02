using UnityEngine;

public class CuttingCounter : BaseCounter
{
	[SerializeField] private CuttingRecipeSO[] CuttingRecipeSOs;
	public override void Interact(CharacterScript player)
	{
		if (!HasKitchenObject())
		{
			// no KitchenObject here
			if (player.HasKitchenObject())
			{
				player.GetKitchenObject().SetKitchenObjectParent(this);
				player.ClearKitchenObjectRefrence();
			} // else { //  player doesnt have anything  }
		}
		else
		{
			// theres KitchenObject here
			if (!player.HasKitchenObject())
			{
				kitchenObject.SetKitchenObjectParent(player);
				SetKitchenObject(null);
			} // else { // player already has KitchenObject }
		}
	}

	public override void InteractAlternate(CharacterScript player)
	{
		if (HasKitchenObject())
		{
			KitchenObjectSO outputKitchenObject = GetKitchenObjectForCuttingRecipe(GetKitchenObject().GetKitchenObjectSO());
			GetKitchenObject().DestroySelf();
			// Debug.Log("Generating KitchenObject" + kitchenObject);
			
			
			KitchenObject.Instance.SpawnKitchenObject(outputKitchenObject, this);
			
			// Transform kitchenObjectTransform = Instantiate(outputKitchenObject.prefab);
			// kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
		}
	}
	
	public KitchenObjectSO GetKitchenObjectForCuttingRecipe(KitchenObjectSO kitchenObjectSO)
	{
		foreach (CuttingRecipeSO cuttingRecipe in CuttingRecipeSOs)
		{
			if (cuttingRecipe.input == kitchenObjectSO) 
				return cuttingRecipe.output;
		}
		return null;
	}

}
