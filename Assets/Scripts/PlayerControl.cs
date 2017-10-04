using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class PlayerControl : MonoBehaviour
{
    #region Variables
    public float speed;
    public Rigidbody2D rb2d;
    public Animator animador;
    public TextMesh ExbLife;
    public TextMesh HighScore;
    public CircleCollider2D collider2d;
    public static int life = 0;
    public GameObject Paused;
    public GameObject play;
    public GameObject home;
    private int lifeCompair = 20;
    private int highScore = 0;
    Vector2 vetor = new Vector2();
    
    private float y = 0f;
    private bool paused = false;
    private bool started = true;

    public AudioSource AudioSource;
    public AudioClip CafeSound;
    public AudioClip ComerRuimSound;
    public AudioClip CoxinhaSound;
    public AudioClip JumpSound;
    public AudioClip SlideSound;
    #endregion

    Ranking rank = new Ranking();
    private void MakeSound(AudioClip Sound)
    {
        AudioSource.PlayClipAtPoint(Sound, transform.position);
    }

    void Start()
    {
        if (Ranking.ranking.Count == 0)
        {
            rank.Deserialize();
            HighScore.text = "HighScore \n" + Ranking.ranking[0].score;
        }
        AudioSource.Pause();
        Paused.SetActive(false);
        play.SetActive(false);
        home.SetActive(false);
        SpawnObjetos.speed = 1;
        Play();

        life = 0;
        lifeCompair = 20;
        SpawnObjetos.TempoParaObjetoAparecerEmCena = 3f;
        MoveOffset.Speed = 5f;

        //highScore = PlayerPrefs.GetInt("HighScore", 0);
        //if (HighScore != null)
        //    HighScore.text = "HighScore \n" + Ranking.ranking[0].score;

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity.Set(speed, 0);

        collider2d = GetComponent<CircleCollider2D>();

    }

    public Vector2 touchOrigin = new Vector2();
    public Vector2 touchEnd = new Vector2();
    public bool directionChosen;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Pause();
                return;
            }
            else
                Play();
        }

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
            switch (mytouch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    touchOrigin = mytouch.position;
                    directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    touchEnd = mytouch.position;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;
            }
            if(directionChosen)
            {
                y = touchEnd.y - touchOrigin.y;
                if (paused)
                {
                    Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    Opcoes(pos);
                }
                else
                {
                    if (y < 0)
                    {
                        Crouch();
                    }
                    else
                    {
                        if (!started)
                            Jump();
                        else
                            started = !started;
                    }
                }
            }
            //if (mytouch.phase == TouchPhase.Began)
            //{
            //    touchOrigin = mytouch.position;
            //}
            //else if (mytouch.phase == TouchPhase.Stationary || mytouch.phase == TouchPhase.Moved)
            //{
            //    touchEnd = mytouch.position;
            //}
            //else if (mytouch.phase == TouchPhase.Ended)
            //{
                

            //}
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

    void Opcoes(Vector2 pos)
    {
        Collider2D[] col = Physics2D.OverlapPointAll(pos);

        if (col.Length > 0)
            foreach (Collider2D c in col)
            {
                if (c.CompareTag("play"))
                    Play();
                else if (c.CompareTag("home"))
                    SceneManager.LoadScene("Main");
            }
    }

    void Pause()
    {
        Time.timeScale = 0;
        Paused.SetActive(true);
        play.SetActive(true);
        home.SetActive(true);
        SpawnObjetos.speed = 0;
        paused = true;
        AudioSource.Pause();
    }

    void Play()
    {
        Time.timeScale = 1;
        Paused.SetActive(false);
        play.SetActive(false);
        home.SetActive(false);
        SpawnObjetos.speed = 1;
        paused = false;
        AudioSource.UnPause();
    }

    private void Crouch()
    {
        if (!animador.GetBool("_isCrouched") && !animador.GetBool("_isJumping"))
        {
            vetor.Set(0f, -1.5f);
            collider2d.offset = vetor;
            animador.SetBool("_isCrouched", true);
            MakeSound(SlideSound);
        }
    }

    private void Jump()
    {
        if (!animador.GetBool("_isCrouched") && !animador.GetBool("_isJumping"))
        {
            rb2d.AddForce(transform.up * 3200);
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
        else if (coll.gameObject.tag == "obstaculo")
        {
            PlayerPrefs.SetInt("Score", life);
            if (life > highScore)
            {
                PlayerPrefs.SetInt("HighScore", life);
            }
            SceneManager.LoadScene("GameOver");
            //Application.LoadLevel("GameOver");
        }
        else if (coll.gameObject.tag == "coxinha")
        {
            MakeSound(CoxinhaSound);
            life += 10;
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.tag == "cafe")
        {
            MakeSound(CafeSound);
            life += 5;
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.tag == "ruim")
        {
            MakeSound(ComerRuimSound);
            if (life >= 0)
            {
                life -= 5;
                Destroy(coll.gameObject);

                if (life <= 0)
                {
                    PlayerPrefs.SetInt("Score", life);
                    SceneManager.LoadScene("GameOver");
                }
            }
        }

        ExbLife.text = "Score \n" + life.ToString();
        if (life > lifeCompair)
        {
            lifeCompair += 10;
            if (SpawnObjetos.TempoParaObjetoAparecerEmCena > 1.5f)
            {
                SpawnObjetos.TempoParaObjetoAparecerEmCena -= 0.5f;
            }
            if (MoveOffset.Speed > 2)
            {
                MoveOffset.Speed -= 1f;
            }
        }
    }
}
