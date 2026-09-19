using System;
using UnityEngine;

// TODO: interaction can be hidden, but cant be enabled back
// the condition is always failing in this file @line17

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    private void Start() {
        CharacterScript.Instance.OnSelectedCounterChanged += CharacterScript_OnSelectedCounterChanged;
    }

    private void CharacterScript_OnSelectedCounterChanged(object sender, CharacterScript.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            Debug.Log("Showing selection");
            Show();
        }
        else
        {
            Hide();
        }
        
        // visualGameObject.SetActive(e.selectedCounter == clearCounter);
    }
    void Show()
    {
        visualGameObject.SetActive(true);
    }
    void Hide()
    {
        visualGameObject.SetActive(false);
    }
}
