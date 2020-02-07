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

    float distToGround;

    [SerializeField] LayerMask lm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        distToGround = GetComponent<BoxCollider2D>().bounds.extents.y + 0.1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckIfGrounded();
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

    void CheckIfGrounded()
    {
        if (rb.velocity.y <= 0.0f)
        {
            //int layerMask = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("YellowPlayer") | 1 << LayerMask.NameToLayer("BluePlayer") | 1 << LayerMask.NameToLayer("RedPlayer");
            //layerMask = layerMask & ~(1 << LayerMask.NameToLayer(myColor.ToString() + "Player"));
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, distToGround, lm);
            if (hit2D)
            {
                canJump = true;
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.tag == "GroundSurface" ||
    //        collision.collider.tag == "Player")
    //        canJump = true;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == myColor.ToString())
        {
            atExit = true;
        }
        if (collision.tag == "Teleporter")
        {
            this.transform.position = collision.GetComponent<Teleporter>().destination.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "MovingPlatform")
        {
            this.transform.SetParent(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == myColor.ToString())
        {
            atExit = false;
        }
        if (collision.tag == "MovingPlatform")
        {
            this.transform.SetParent(null);
        }
    }
}
