using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    private void Start() {
        CharacterScript.Instance.OnSelectedCounterChanged += CharacterScript_OnSelectedCounterChanged;
    }

    private void CharacterScript_OnSelectedCounterChanged(object sender, CharacterScript.OnSelectedCounterChangedEventArgs e)
    {   
        visualGameObject.SetActive(e.selectedCounter == clearCounter);
    }
}
