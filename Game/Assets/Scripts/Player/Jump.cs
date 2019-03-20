using UnityEngine;

/*
 * Credit: Board To Bits Games
 * Link: https://www.youtube.com/watch?v=7KiK0Aqtmzc
 */
public class Jump : MonoBehaviour {
    [Range(0, 10)] public float JumpVelocity = 5;

    public bool IsGrounded;

    private Rigidbody2D _rigidbody2D;
    private GeneralAttributes _generalAttributes;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _generalAttributes = GetComponent<GeneralAttributes>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded) {
            _rigidbody2D.velocity = Vector2.up * JumpVelocity;
            _generalAttributes.IsSitting = false;
        }

        // IsGrounded = Physics2D.OverlapCircle(Ground.position, 0.15f, GroundLayerMask);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            IsGrounded = false;
        }
    }
}