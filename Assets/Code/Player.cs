using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2D;
    [SerializeField]
    private float impulsoPulo = 6.5f;
    [SerializeField]
    private float velocidadeMovimento = 3f;
    [SerializeField]
    private ChecaChao checaChao;
    float dano;
    private bool Olhandoesquerda = false;
    private float energia = 100;
    private int ANIM_ESTA_PULANDO;
    private int ANIM_ESTA_ANDANDO;
    [SerializeField]
    private bool EstaAndando = false;
    [SerializeField]
    private float vida = 5;


    private void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ANIM_ESTA_PULANDO = Animator.StringToHash("EstaPulando");
        ANIM_ESTA_ANDANDO = Animator.StringToHash("EstaAndando");
    }
    void Update()
    {
        Andar();
        Pulo();
        VirarPersonagem();
        ChecaSeEstaNoChaoParaPular();

    }

    private void Pulo()
    {


        if (Input.GetAxisRaw("Jump") == 1 && checaChao.EstaNoChao)
        {
            rigidbody2D.velocity = Vector2.up * impulsoPulo;

        }

        //if (rigidbody2D.velocity.y < 0)
        //{
        //    rigidbody2D.gravityScale = 1.5f;
        //}
        //else if (rigidbody2D.velocity.y < 0 && Input.GetAxisRaw("Jump") == 0)
        //{
        //    rigidbody2D.gravityScale = 1.2f;
        //}
        //else
        //{
        //    rigidbody2D.gravityScale = 1f;
        //}
    }
    private void Andar()
    {

        energia -= Time.deltaTime * 2;
        Vector2 movimento = new Vector2(Input.GetAxisRaw("Horizontal") * velocidadeMovimento, rigidbody2D.velocity.y);

        rigidbody2D.velocity = movimento;

    }
    private void VirarPersonagem()
    {
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            animator.SetBool(ANIM_ESTA_ANDANDO, true);

            EstaAndando = false;
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            EstaAndando = true;
            animator.SetBool(ANIM_ESTA_ANDANDO, true);
        }

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            EstaAndando = false;
            animator.SetBool(ANIM_ESTA_ANDANDO, false);
        }
    }
    public void TomarDano(float dano)
    {
        vida -= dano;
    }
    private void ChecaSeEstaNoChaoParaPular()
    {
        if (checaChao.EstaNoChao)
        {
            animator.SetBool(ANIM_ESTA_PULANDO, false);
        }
        else
        {
            animator.SetBool(ANIM_ESTA_ANDANDO, false);
            animator.SetBool(ANIM_ESTA_PULANDO, true);
        }

    }
}
