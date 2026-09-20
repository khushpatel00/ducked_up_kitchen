using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform ObjectPrefab;
    [SerializeField] private Transform CounterTop;
    public void Interact()
    {
        Transform objectTransform = Instantiate(ObjectPrefab, CounterTop);
        objectTransform.localPosition = Vector3.zero;
    }
}
