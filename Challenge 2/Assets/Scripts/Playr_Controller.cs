using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playr_Controller : MonoBehaviour
{
    private Rigidbody2D rd2d;

    Animator anim;

    public float speed;

    public Text score;

    public Text lives;

    public Text winText;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    private int scoreValue;

    private int livesValue;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreValue = 0;
        SetScoreText();
        /*score.text = scoreValue.ToString();*/
        livesValue = 4;
        winText.text = "";
        SetLivesText();
        /*lives.text = livesValue.ToString();*/
        /* musicSource.clip = musicClipOne;
         musicSource.clip = musicClipTwo;*/
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            musicSource.clip = musicClipOne;

            musicSource.Play();
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement*speed, vertMovement*speed));

        if (Input.GetKey("escape"))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreText();
            Destroy(collision.collider.gameObject);
        }

        if (collision.collider.tag == "enemy")
        {
            livesValue -= 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }

        if (collision.collider.tag == "SSSCoin")
        {
            scoreValue += 4;
            SetScoreText();
            Destroy(collision.collider.gameObject);
        }
        /* if (scoreValue == 4)
         {
             transform.position = new Vector2(71.0f, 5.0f);
         }
         */
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                anim.SetInteger("State", 2);
            }
        }
     }

    void SetScoreText()
    {
        score.text = "Score: " + scoreValue.ToString();

        if (scoreValue == 4)
        {
            transform.position = new Vector2(71.0f, 0.0f);
            livesValue = 5;
        }

        if (scoreValue == 8)
        {
            winText.text = "You Win! game made by Nicholas Johnson music credits: Happy Adventure by TinyWorlds, 8-bit victory by Wolfgang found on opengameart.com.";
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
    }

    void SetLivesText()
    {
        lives.text = "Lives: " + livesValue.ToString();

        if (livesValue <= 0)
        {
            winText.text = "you lose!";
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}
