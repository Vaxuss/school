using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 PlayerMoveInput;

    [SerializeField] Rigidbody PlayerBody;
    [SerializeField] float Speed = 9f;

    void FixedUpdate()
    {
        PlayerMoveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMoveInput);
        PlayerBody.linearVelocity = new Vector3(MoveVector.x, PlayerBody.linearVelocity.y, MoveVector.z).normalized * Speed;
    }
}
