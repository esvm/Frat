using UnityEngine;
//using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHelp : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb2d;
    public Animator animador;
    Vector2 vetor = new Vector2();
    private Vector2 touchOrigin = -Vector2.one;
    private float y = 0f;
    public CircleCollider2D collider2d;

    public AudioClip CafeSound;
    public AudioClip ComerRuimSound;
    public AudioClip CoxinhaSound;
    public AudioClip JumpSound;
    public AudioClip SlideSound;

    private void MakeSound(AudioClip Sound)
    {
        AudioSource.PlayClipAtPoint(Sound, transform.position);
    }

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity.Set(speed, 0);

        collider2d = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animador.GetBool("_isCrouched"))
        {
            if (animador.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                animador.SetBool("_isCrouched", false);
                vetor.Set(0f, 0f);
                collider2d.offset = vetor;
            }
        }

        if (Input.touchCount > 0)
        {
            Touch mytouch = Input.GetTouch(0);
            if (mytouch.phase == TouchPhase.Began)
            {
                touchOrigin = mytouch.position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Vector2 touchEnd = mytouch.position;
                y = touchEnd.y - touchOrigin.y;


                if (y < 0)
                {
                    Crouch();
                }
                else
                {
                    Jump();
                }


            }
        }



        //for desktop
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Crouch();
        }
    }
    private void Crouch()
    {
        if (animador.GetBool("_isCrouched") == false && animador.GetBool("_isJumping") == false)
        {
            vetor.Set(0f, -1.5f);
            collider2d.offset = vetor;
            animador.SetBool("_isCrouched", true);

            MakeSound(SlideSound);
        }

    }

    private void Jump()
    {
        if (animador.GetBool("_isCrouched") == false && animador.GetBool("_isJumping") == false)
        {
            rb2d.AddForce(transform.up * 2700);
            animador.SetBool("_isJumping", true);

            MakeSound(JumpSound);
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            animador.SetBool("_isJumping", false);
        }
    }
}
