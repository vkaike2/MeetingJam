using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanoEnergia : MonoBehaviour
{
    [SerializeField]
    private Animator animatorCano;
    [SerializeField]
    private float cdwComida;
    [SerializeField]
    private float cdwComnunicacao;
    [SerializeField]
    private eTipoCano tipoCano;

    [SerializeField]
    private float cdwPermaneceAberto;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private AudioSource audio;

    private int ANIM_ABRIR_CANO;
    private float checkCdwComida = 0;
    private float checkCdwComnunicacao = 0;
    private float checkCdwPermaneceAberto = 0;

    private bool keyPressed = false;
    public int comida = 0;
    public bool comunicacao = false;

    public CanoEnergia canoEnergia;

    public bool podeReceberComida;

    [Header("UI")]
    [SerializeField]
    private GameObject ChatBombeiro;

    private void Start()
    {
        ANIM_ABRIR_CANO = Animator.StringToHash("PossuiComida");
    }

    private void FixedUpdate()
    {
        if (tipoCano == eTipoCano.Energia)
        {
            animatorCano.SetBool(ANIM_ABRIR_CANO, comida > 0);
            if (podeReceberComida)
            {
                AdicionarComidaCano();
            }
            else
            {
                FecharCanoEnergia();
            }
        }
        else if (tipoCano == eTipoCano.Comunicacao)
        {
            animatorCano.SetBool(ANIM_ABRIR_CANO, comunicacao);
            PerguntarSobreComunicacao();
        }

        checkCdwPermaneceAberto += Time.deltaTime;
        if (checkCdwPermaneceAberto >= cdwPermaneceAberto && animatorCano.GetBool(ANIM_ABRIR_CANO))
        {
            switch (tipoCano)
            {
                case eTipoCano.Energia:
                    FecharCanoEnergia();
                    break;
                case eTipoCano.Comunicacao:
                    FecharCanoComunicacao();
                    canoEnergia.podeReceberComida = false;
                    break;
                default:
                    break;
            }
        }
    }


    private void AdicionarComidaCano()
    {
        checkCdwComida += Time.deltaTime;
        if (checkCdwComida >= cdwComida && comida == 0)
        {
            comida = 1;
            checkCdwPermaneceAberto = 0;
        }
    }

    private void PerguntarSobreComunicacao()
    {
        checkCdwComnunicacao += Time.deltaTime;

        if (checkCdwComnunicacao >= cdwComnunicacao - 0.5f && !comunicacao)
        {
            if (!audio.isPlaying)
                audio.Play(0);
            ChatBombeiro.SetActive(true);
        }

        if (checkCdwComnunicacao >= cdwComnunicacao && !comunicacao)
        {
            checkCdwPermaneceAberto = 0;
            comunicacao = true;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float click = Input.GetAxisRaw("Fire1");

        if (click == 1 && !keyPressed && collision.gameObject.layer == 8)
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (tipoCano == eTipoCano.Energia && comida > 0)
            {
                if (player != null)
                {
                    player.ObterComida(comida);
                    FecharCanoEnergia();
                }
            }
            else if (tipoCano == eTipoCano.Comunicacao && comunicacao)
            {
                player.PlayResposta();
                canoEnergia.podeReceberComida = true;
                FecharCanoComunicacao();
            }
            keyPressed = true;
        }

        if (click == 0 && keyPressed)
            keyPressed = false;
    }

    public void FecharCanoEnergia()
    {
        comida = 0;
        checkCdwComida = 0f;
    }
    private void FecharCanoComunicacao()
    {
        comunicacao = false;
        checkCdwComnunicacao = 0f;
        ChatBombeiro.SetActive(false);
    }
}
