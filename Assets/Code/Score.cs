using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    [Header("UI")]
    [SerializeField]
    private Text txtScore;
    [SerializeField]
    private Animator animatorScore;

    public int score;

    private int ANIM_ADD_COMIDA;
    private float timer = 0.0f;

    public bool podeContar = true;

    void Start()
    {
        ANIM_ADD_COMIDA = Animator.StringToHash("AddComida");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= 1.0f)
        {
            if (podeContar)
                score += 1;
            timer = 0f;
        }

        txtScore.text = score < 10 ? "0" + score.ToString() : score.ToString();
    }

    public void AdicionarScore(int qtd)
    {

        animatorScore.ResetTrigger(ANIM_ADD_COMIDA);
        animatorScore.SetTrigger(ANIM_ADD_COMIDA);
        score += qtd;
    }
}
