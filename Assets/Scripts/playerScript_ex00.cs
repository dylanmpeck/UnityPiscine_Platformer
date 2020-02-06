using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class playerScript_ex00 : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float forceAmount;
    [SerializeField] float jumpForce;

    enum CharacterColor {Red, Yellow, Blue };

    [SerializeField] CharacterColor myColor;

    bool canJump = true;

    [HideInInspector]
    public bool atExit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector2.right * forceAmount);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector2.left * forceAmount);
        }
        if (canJump && Input.GetKey(KeyCode.Space))
        {
            canJump = false;
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
            canJump = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == myColor.ToString())
        {
            atExit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == myColor.ToString())
        {
            atExit = false;
        }
    }
}
