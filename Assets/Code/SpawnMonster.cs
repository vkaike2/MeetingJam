using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    [SerializeField]
    private float coolDown;
    [SerializeField]
    private List<GameObject> monstros;

    [SerializeField]
    private Transform spwanEsquerda;
    [SerializeField]
    private Transform spwanDireita;



    private float checkCoolDown;

    private void Start()
    {
        checkCoolDown = 0f;
    }

    void Update()
    {
        SpawnaMonstro();
        checkCoolDown += Time.deltaTime;
        //Debug.Log(Random.Range(1, 3));
    }
    private void SpawnaMonstro()
    {
        if (checkCoolDown >= coolDown)
        {
            int numeroAleatorio = Random.Range(1, 3);
            int direcao = 0;
            Vector2 spawn = new Vector2();
            if (numeroAleatorio == 1)
            {
                direcao = 1;
                spawn = spwanDireita.position;
            }
            else
            {
                direcao = -1;
                spawn = spwanEsquerda.position;
            }

            var monstroSpawnado = Instantiate(monstros[Random.Range(0, monstros.Count)], spawn, transform.rotation);
            monstroSpawnado.GetComponent<Monstro>().IniciaComADirecao(direcao);

            checkCoolDown = 0f;
        }

    }

}
