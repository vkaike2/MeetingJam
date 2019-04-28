using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatUI : MonoBehaviour
{
    private Text texto;
    [TextArea]
    public string textoChat;
    [SerializeField]
    private float cdwEntreLetras;

    [SerializeField]
    private GameObject proximoTexto;


    void Start()
    {
        texto = GetComponent<Text>();
        StartCoroutine("EscreverTexto");
    }

    IEnumerator EscreverTexto()
    {
        if (texto.text != textoChat)
        {
            var todasLetras = textoChat.ToCharArray();
            foreach (var letra in todasLetras)
            {
                texto.text += letra;
                yield return new WaitForSeconds(cdwEntreLetras);
            }
            if(textoChat == texto.text && proximoTexto != null)
            {
                proximoTexto.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
