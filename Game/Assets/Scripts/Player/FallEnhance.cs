using UnityEngine;

/*
 * Credit: Board To Bits Games
 * Link: https://www.youtube.com/watch?v=7KiK0Aqtmzc
 */
public class FallEnhance : MonoBehaviour {
	[Range(0, 10)]
	public float FallMultiplier = 3f;
	[Range(0, 5)]
	public float LowJumpMultiplier = 2f;

	private Rigidbody2D _rigidbody2D;

	private void Awake() {
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update () {
		if (_rigidbody2D.velocity.y < 0) {
			_rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
		} 
		else if (_rigidbody2D.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
			_rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
		}
	}
}
