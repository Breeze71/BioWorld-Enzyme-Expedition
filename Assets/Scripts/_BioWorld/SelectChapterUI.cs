using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectChapterUI : MonoBehaviour
{
    [SerializeField] private Button chapter1_Buttonn;
    [SerializeField] private Button chapter2_Button;
    [SerializeField] private Button chapter3_Button;

    private void Awake() 
    {
        chapter1_Buttonn.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scene.Chapter1);
        });

        chapter2_Button.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scene.Chapter2);
        });
        chapter3_Button.onClick.AddListener(() =>
        {
            //
        });
        Time.timeScale = 1f;
    }
}
