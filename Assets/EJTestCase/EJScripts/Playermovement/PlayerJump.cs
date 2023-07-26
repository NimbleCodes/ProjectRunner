using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] Rigidbody _rigPlayer;
    int _jumpCount = 0;
    bool _isGround = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (_jumpCount < 1 || _isGround == true))

        {
            GetComponent<Animator>().Play("Jumping");
            _jumpCount++; 
            _rigPlayer.AddForce(Vector2.up * 5, ForceMode.Impulse);
            _isGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Jumping"))
        {
            _isGround = true;
            _jumpCount = 0;
        }
    }

}