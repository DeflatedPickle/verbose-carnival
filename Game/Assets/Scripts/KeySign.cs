using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySign : MonoBehaviour {
    public float MovementSpeed;
    
    public Vector3 PositionOriginal;
    public Vector3 PositionIncrease;

    public int MoveUp;

    public Vector3 Velocity;

    private void Update() {
        PositionOriginal = transform.parent.position;
        PositionIncrease = PositionOriginal + Vector3.up * 1.5f;
    }

    private void FixedUpdate() {
        if (MoveUp == 1) {
            transform.position = Vector3.SmoothDamp(transform.position,
                PositionIncrease,
                ref Velocity,
                MovementSpeed * Time.deltaTime);
        }
        else if (MoveUp == 2) {
            transform.position = Vector3.SmoothDamp(transform.position,
                PositionOriginal,
                ref Velocity,
                MovementSpeed * Time.deltaTime);
        }
    }
}