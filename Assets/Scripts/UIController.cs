using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    public Text notificacao;
    public static UIController instance;
    
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        
        instance = this;
    }

public void CallMensageBox(string mensagem)
{
        StopAllCoroutines();
        StartCoroutine(MensageBoxRoutine(mensagem));
    }

    private IEnumerator MensageBoxRoutine(string mensagem)
    {
        notificacao.gameObject.SetActive(true);
        notificacao.text = mensagem;
        yield return new WaitForSeconds(3);
        notificacao.gameObject.SetActive(false);
    }
}
