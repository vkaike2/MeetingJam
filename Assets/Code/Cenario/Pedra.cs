using System.Collections.Generic;
using UnityEngine;

public class Pedra : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> tiposPedra;

    private void Start()
    {
        if(tiposPedra.Count > 0)
        {
            var SpriteRenderer = this.GetComponent<SpriteRenderer>();

            int posicaoRandomica = Random.Range(0, tiposPedra.Count);
            Sprite spriteRandomico = tiposPedra[posicaoRandomica];
            SpriteRenderer.sprite = spriteRandomico;
        }
    }
}
