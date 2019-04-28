using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float impulsoPulo = 6.5f;
    [SerializeField]
    private float velocidadeMovimento = 3f;
    [SerializeField]
    private ChecaChao checaChao;
    [SerializeField]
    private int totalComida = 0;
    [SerializeField]
    private float energia;
    public float totalEnergia;
    [SerializeField]
    private GameObject LuzHelmet;

    [SerializeField]
    private Animator animPicareta;

    [SerializeField]
    private List<AudioSource> audios;


    float dano;
    Animator animator;
    Rigidbody2D rigidbody2D;
    private bool Olhandoesquerda = false;
    private int ANIM_ESTA_PULANDO;
    private int ANIM_ESTA_ANDANDO;
    private int ANIM_ADD_COMIDA;
    private int ANIM_RECEBEU_DANO;
    private int ANIM_MORREU;
    private int ANIM_ATACAR;

    [SerializeField]
    private bool EstaAndando = false;
    [SerializeField]
    private float vida;

    [Header("UI")]
    [SerializeField]
    private Text txtQtdComida;
    [SerializeField]
    private Animator animatorQtdComida;
    [SerializeField]
    private Image barraEnergia;
    [SerializeField]
    private List<CoracaoUI> vidaUI;
    [SerializeField]
    private GameObject gameOver;



    private bool keyPressedJump = false;

    private bool vulneravel = true;

    private void Start()
    {
        totalEnergia = energia;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ANIM_ESTA_PULANDO = Animator.StringToHash("EstaPulando");
        ANIM_ESTA_ANDANDO = Animator.StringToHash("EstaAndando");
        ANIM_ADD_COMIDA = Animator.StringToHash("AddComida");
        ANIM_RECEBEU_DANO = Animator.StringToHash("RecebeuDano");
        ANIM_MORREU = Animator.StringToHash("Morreu");
        ANIM_ATACAR = Animator.StringToHash("Atacar");

    }
    void Update()
    {
        Andar();
        Pulo();
        VirarPersonagem();
        ChecaSeEstaNoChaoParaPular();
        UsarComida();
        UIAtualizaEnergia();
        Atacar();

        if (energia <= 0)
        {
            Faleceu();
        }
    }

    private void Atacar()
    {
        float inpAtk = Input.GetAxisRaw("Fire1");
        if (inpAtk == 1 && !animPicareta.GetBool(ANIM_ATACAR))
        {
            animPicareta.SetBool(ANIM_ATACAR, true);
        }
    }

    private void Pulo()
    {
        if (Input.GetAxisRaw("Jump") == 1 && checaChao.EstaNoChao && !keyPressedJump)
        {
            rigidbody2D.velocity = Vector2.up * impulsoPulo;
            keyPressedJump = true;
        }

        if (Input.GetAxisRaw("Jump") == 0 && keyPressedJump)
            keyPressedJump = false;
    }
    private void Andar()
    {
        Vector2 movimento = new Vector2(Input.GetAxisRaw("Horizontal") * velocidadeMovimento, rigidbody2D.velocity.y);

        if (movimento != Vector2.zero)
        {
            energia -= Time.deltaTime;
            GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().podeContar = true;
        }
        else
            GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().podeContar = false;
        rigidbody2D.velocity = movimento;
    }

    private void VirarPersonagem()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (inputHorizontal == -1)
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            animator.SetBool(ANIM_ESTA_ANDANDO, true);

            EstaAndando = false;
        }
        else if (inputHorizontal == 1)
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }

        if (inputHorizontal == 1)
        {
            EstaAndando = true;
            animator.SetBool(ANIM_ESTA_ANDANDO, true);
        }

        if (inputHorizontal == 0)
        {
            EstaAndando = false;
            animator.SetBool(ANIM_ESTA_ANDANDO, false);
        }
    }
    public void TomarDano(float dano)
    {
        if (vulneravel && !animator.GetBool(ANIM_MORREU))
        {
            vidaUI[(int)vida - 1].Explodir();
            vida -= dano;
            PlayReceberDano();
            if (vida <= 0)
            {
                Faleceu();
            }
            else
            {
                animator.ResetTrigger(ANIM_RECEBEU_DANO);
                animator.SetTrigger(ANIM_RECEBEU_DANO);

            }
        }
    }

    private void Faleceu()
    {
        animator.SetBool(ANIM_MORREU, true);
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        LuzHelmet.SetActive(false);
        gameOver.SetActive(true);
        GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().podeContar = false;
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
    private void UsarComida()
    {
        var botaoComida = Input.GetKeyDown(KeyCode.E);
        if (botaoComida && totalComida > 0)
        {
            PlayMordeMaca();
            if (energia <= totalEnergia * 0.5f)
                energia += totalEnergia * 0.5f;
            else
                energia = totalEnergia;

            totalComida -= 1;
            UIAddQtdComida(totalComida);
        }
    }

    public void ObterComida(int qtd)
    {
        PlayPegaMaca();
        totalComida += qtd;
        UIAddQtdComida(totalComida);
    }

    private void UIAddQtdComida(int valor)
    {
        animatorQtdComida.ResetTrigger(ANIM_ADD_COMIDA);
        animatorQtdComida.SetTrigger(ANIM_ADD_COMIDA);
        txtQtdComida.text = valor < 10 ? "0" + valor.ToString() : valor.ToString();
    }
    private void UIAtualizaEnergia()
    {
        barraEnergia.fillAmount = energia / totalEnergia;
    }

    public void TornarInvulneravel()
    {
        vulneravel = false;
    }

    public void TornarVulneravel()
    {
        vulneravel = true;
    }

    public void PlayAndando()
    {
        audios[0].Play(0);
    }

    public void PlayReceberDano()
    {
        audios[Random.Range(1, 3)].Play(0);
    }
    public void PlayPicareta()
    {
        audios[4].Play(0);
    }
    public void PlayResposta()
    {
        audios[5].Play(0);
    }
    public void PlayPegaMaca()
    {
        audios[6].Play(0);
    }
    public void PlayMordeMaca()
    {
        audios[7].Play(0);
    }
}
