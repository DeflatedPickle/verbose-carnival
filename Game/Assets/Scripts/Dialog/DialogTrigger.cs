using UnityEngine;

public class DialogTrigger : MonoBehaviour {
    public Dialog Dialog;
    
    private KeySign _keySign;

    private GameObject _canvas;
    private DialogManager _dialogManager;

    private void Awake() {
        _keySign = transform.Find("KeySign").GetComponent<KeySign>();
        _canvas = GameObject.Find("Canvas");
        _dialogManager = FindObjectOfType<DialogManager>();
    }

    private void Update() {
        if (_keySign.MoveUp == 1) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (!_dialogManager.IsDialogOpen) {
                    _dialogManager.OpenDialog(Dialog);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            _keySign.MoveUp = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            _keySign.MoveUp = 2;

            if (_dialogManager.IsDialogOpen) {
                if (_dialogManager.CurrentSentenceIndex < _dialogManager.CurrentDialog.Sentences.Length) {
                    _dialogManager.LeaveLine();
                }
                else {
                    _dialogManager.CloseDialog();
                }
            }
        }
    }
}