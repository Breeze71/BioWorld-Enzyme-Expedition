using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//单独存储空间，防止以后有重名
namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
      public bool finished {get;private set;}
        //对话输入，然后一个字一个字的输出
      protected IEnumerator WriteText(string input,Text textHolder, Color textColor,Font textFont,float delay,AudioClip sound/*,float delayBetweenLines*/)
      {
         textHolder.color = textColor;
         textHolder.font = textFont;
         
         for (int i = 0; i < input.Length;i++)
         {
            textHolder.text += input[i];
            //SoundManager.instance.PlaySound(sound);
            //对话延迟时间
            yield return new WaitForSeconds(delay);
         
         }
         // 段落之间通过时间延迟
         //yield return new WaitForSeconds(delayBetweenLines);
         //段落之间点击继续
         yield return new WaitUntil(()=>Input.GetKeyDown("e"));

         finished = true;
      }
     
      

    }

}

