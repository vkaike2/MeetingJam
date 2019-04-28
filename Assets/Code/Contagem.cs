using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contagem : MonoBehaviour
{
    private Animator anim;

    private int valorMaximo = 5;
    private float timer = 0.0f;
    private int ANIM_ADICIONAR;
    [SerializeField]
    private Text txtCountDown;

    void Start()
    {
        anim = GetComponent<Animator>();
        ANIM_ADICIONAR = Animator.StringToHash("Adicionar");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1.0f)
        {
            anim.ResetTrigger(ANIM_ADICIONAR);
            anim.SetTrigger(ANIM_ADICIONAR);
            valorMaximo -= 1;
            timer = 0f;
        }
        txtCountDown.text = valorMaximo < 10 ? "0" + valorMaximo.ToString() : valorMaximo.ToString();

        if(valorMaximo <= 0)
        {
            txtCountDown.text = "SCORE: "+ GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().score.ToString();
        }
    }
}
