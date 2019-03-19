using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogResponses : MonoBehaviour {
    private DialogManager _dialogManager;

    private void Awake() {
        _dialogManager = FindObjectOfType<DialogManager>();
    }

    public void NPC1_YoureDumb() {
        _dialogManager.ForceCloseDialog();
        _dialogManager.OpenDialog(new Dialog {
            Names = new[] {"NPC 1"},
            Sentences = new[] {"xdddddddddddddddddddddddd"},
            LeaveSentence = "",
            Options = new Dialog.Option[] { }
        });
        _dialogManager.NextLine();
    }

    public void NPC1_YoureABeautifulPerson() {
        _dialogManager.CloseDialog();
    }
}