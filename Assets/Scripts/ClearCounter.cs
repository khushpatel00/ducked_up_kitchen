using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObject;
    [SerializeField] private Transform CounterTop;
    public void Interact()
    {
        Transform objectTransform = Instantiate(kitchenObject.prefab, CounterTop);
        objectTransform.localPosition = Vector3.zero;
    }
}
