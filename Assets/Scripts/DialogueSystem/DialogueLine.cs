using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace DialogueSystem{


    public class DialogueLine : DialogueBaseClass
    {
    private Text textHolder;

     [Header("文本选项")]
     [SerializeField] private string input;
     [SerializeField] private Color textColor;
     [SerializeField] private Font textFont;
    
     [Header("字节时间间隔")]
     [SerializeField] private float delay;
    // [SerializeField] private float delayBetweenLines;

     [Header("声音")]
     [SerializeField] private AudioClip sound;

     [Header("角色头像")]
     [SerializeField] private Sprite characterSprite;
     [SerializeField] private Image imageHolder;

     private void Awake()
     {
        textHolder = GetComponent<Text>();
       // textHolder.text = "";
        imageHolder.sprite = characterSprite;
        imageHolder.preserveAspect = true;  
     }

     private void Start()
      {
        StartCoroutine(WriteText(input,textHolder,textColor,textFont,delay,sound));
        
      }
    }


}
