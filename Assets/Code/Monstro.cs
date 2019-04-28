using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro : MonoBehaviour
{
    [SerializeField]
    private float velocidade;
    Rigidbody2D rigibodyCol;
    [SerializeField]
    private float vida;
    [SerializeField]
    private int pontos;

    private Animator anim;
    private int ANIM_MORREU;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigibodyCol = GetComponent<Rigidbody2D>();
        ANIM_MORREU = Animator.StringToHash("Morreu");
    }
    void FixedUpdate()
    {
        rigibodyCol.velocity = new Vector2(velocidade, 0);

        if (rigibodyCol.velocity.x < 0)
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        else
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 11)
        {
            velocidade *= -1;
        }
    }


    public void IniciaComADirecao(int valor)
    {
        velocidade *= valor;
    }

    public void TomarDano(float dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().AdicionarScore(pontos);
            anim.SetBool(ANIM_MORREU, true);
            StartCoroutine("Morrer");
        }
    }

    IEnumerator Morrer()
    {
        rigibodyCol.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }


}
