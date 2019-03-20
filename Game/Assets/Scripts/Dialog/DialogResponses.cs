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
            Sentences = new[] {"Oh, well that's unfortunate.", "I don't think the same, but that's okay."},
            LeaveSentence = "Have a nice day...",
            Options = new Dialog.Option[] { }
        });
        _dialogManager.NextLine();
    }

    public void NPC1_YoureABeautifulPerson() {
        _dialogManager.ForceCloseDialog();
        _dialogManager.OpenDialog(new Dialog {
            Names = new[] {"NPC 1"},
            Sentences = new[] {"Awww, thank you! I wouldn't go as far as saying the same about you, but awww!"},
            LeaveSentence = "You made my day!",
            Options = new Dialog.Option[] { }
        });
        _dialogManager.NextLine();
    }
}