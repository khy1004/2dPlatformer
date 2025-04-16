
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator pAni;
   private bool isGrounded;

    private bool mj = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLeve();

        }

        switch (collision.tag)
        {
            case "test":
                jumpForce += 10;
                moveSpeed += 10;
                Destroy(collision.gameObject);
                break;
            case "Jump":
                jumpForce += 10;
                Destroy(collision.gameObject);
                break;
            case "Speed":     
                moveSpeed += 10;
                Destroy(collision.gameObject);
                break;
            case "shieid":
                mj = true;
                Destroy(collision.gameObject);
                break;
            case "Enemy":
                if (mj)
                {
                    Destroy(collision.gameObject);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                break;
            case "Respawn":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Finish":
                LevelObject levelObject = collision.GetComponent<LevelObject>();
                if (levelObject != null)
                {
                    levelObject.MoveToNextLeve();
                }
                break;

        }
    }

    // Update is called once per frame
    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if(moveInput > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);

        if (moveInput < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if (moveInput != 0)
            pAni.SetBool("runing", true);
        else
            pAni.SetBool("runing", false);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pAni.SetTrigger("JumpAction");
        }
       
            
    }
}
