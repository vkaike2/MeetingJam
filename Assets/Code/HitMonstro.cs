using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMonstro : MonoBehaviour
{
    [SerializeField]
    private float dano = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 8)
        {
            var inimigo = collision.gameObject.GetComponent<Player>();
            if (inimigo != null)
            {
                inimigo.TomarDano(dano);
            }
        }
    }
}
