using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine;

public class AcidDeliveryResultUI : MonoBehaviour
{
    private const string Popup = "Popup";
    //[SerializeField] private Image backgroundImg;
    [SerializeField] private Image iconImg;

    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;

    private Animator anim;

    private void Awake() 
    {
        anim = GetComponent<Animator>();    
    }

    private void Start() 
    {
        gameObject.SetActive(false);
    }

    public void InCorrectAcid_OnInCorrectPlace()
    {
        gameObject.SetActive(true);

        iconImg.sprite = failedSprite;
        iconImg.color = failedColor;

        anim.SetTrigger(Popup);
    }

    public void CorrectAcid_OnCorrectPlace()
    {
        gameObject.SetActive(true);

        iconImg.color = successColor;
        iconImg.sprite = successSprite;

        anim.SetTrigger(Popup);  
    }

}
