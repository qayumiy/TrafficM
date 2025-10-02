using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{



    public Image BlackFade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FadeIn()
    {
        BlackFade.DOFade(1, 1).OnComplete(() =>
        {
            BlackFade.transform.GetChild(0).gameObject.SetActive(true);
        });


    }
}
