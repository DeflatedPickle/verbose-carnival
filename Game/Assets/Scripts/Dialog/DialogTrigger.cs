using UnityEngine;

public class DialogTrigger : MonoBehaviour {
    public float MovementSpeed;
    public Dialog Dialog;
    
    private Transform _keySign;
    private Vector3 _keySignPositionOriginal;
    private Vector3 _keySignPositionIncrease;

    private Vector3 _velocity;

    private int _moveUp;

    private GameObject _canvas;
    private DialogManager _dialogManager;

    private void Awake() {
        _keySign = transform.Find("KeySign").transform;
        _canvas = GameObject.Find("Canvas");
        _dialogManager = FindObjectOfType<DialogManager>();
    }

    private void Update() {
        if (_moveUp == 1) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (!_dialogManager.IsDialogOpen) {
                    _dialogManager.OpenDialog(Dialog);
                }
            }
        }
        
        _keySignPositionOriginal = transform.position;
        _keySignPositionIncrease = _keySignPositionOriginal + Vector3.up * 1.5f;
    }

    private void FixedUpdate() {
        if (_moveUp == 1) {
            _keySign.position = Vector3.SmoothDamp(_keySign.position,
                _keySignPositionIncrease,
                ref _velocity,
                MovementSpeed * Time.deltaTime);
        }
        else if (_moveUp == 2) {
            _keySign.position = Vector3.SmoothDamp(_keySign.position,
                _keySignPositionOriginal,
                ref _velocity,
                MovementSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            _moveUp = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            _moveUp = 2;

            if (_dialogManager.IsDialogOpen) {
                if (_dialogManager.CurrentSentence < _dialogManager.CurrentDialog.Sentences.Length) {
                    _dialogManager.LeaveLine();
                }
                else {
                    _dialogManager.CloseDialog();
                }
            }
        }
    }
}