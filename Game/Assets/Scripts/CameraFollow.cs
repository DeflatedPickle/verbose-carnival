using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform Target;

	public float MovementSpeed;
	public Vector3 OffsetVector;

	private Vector3 _velocity;
	
	private void LateUpdate() {
		transform.position = Vector3.SmoothDamp(transform.position, Target.position + OffsetVector, ref _velocity, MovementSpeed);
	}
}
