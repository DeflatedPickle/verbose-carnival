using UnityEngine;

public class SitTrigger : MonoBehaviour {
    public Vector3 SitPosition;
    public Vector3 StandPosition;
    public GameObject Player;
    
    private KeySign _keySign;

    private GeneralAttributes _playerGeneralAttributes;
    private Rigidbody2D _playerRigidBody2D;

    private void Awake() {
        _keySign = transform.Find("KeySign").GetComponent<KeySign>();

        _playerGeneralAttributes = Player.GetComponent<GeneralAttributes>();
        _playerRigidBody2D = Player.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (_keySign.MoveUp == 1) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (_playerGeneralAttributes.IsSitting) {
                    _playerGeneralAttributes.IsSitting = false;
                    Player.transform.position = transform.position + StandPosition;
                    _playerRigidBody2D.velocity = Vector2.zero;
                }
                else {
                    _playerGeneralAttributes.IsSitting = true;
                    Player.transform.position = transform.position + SitPosition;
                    _playerRigidBody2D.velocity = Vector2.zero;
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
        }
    }
}