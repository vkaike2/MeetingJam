using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPicareta : MonoBehaviour
{
    [SerializeField]
    private float dano = 3;

    private Animator anim;
    private int ANIM_ATACAR;
    [SerializeField]
    private Player player;
    private void Start()
    {
        anim = GetComponent<Animator>();
        ANIM_ATACAR = Animator.StringToHash("Atacar");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 9)
        {
            player.PlayPicareta();
            var inimigo = collision.gameObject.GetComponent<Monstro>();
            inimigo.TomarDano(dano);
        }
    }

    public void PararAtaque()
    {
        anim.SetBool(ANIM_ATACAR, false);
    }

}
