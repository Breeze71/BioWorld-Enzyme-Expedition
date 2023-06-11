using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidGenarate : MonoBehaviour
{
    [SerializeField] private List<GameObject> AcidList;
    [SerializeField] private Transform generateTrans;
    [SerializeField] private float generateTimerMax = 5;
    private float generateTimer;

    private void Update() 
    {
        generateTimer -= Time.deltaTime;
        if(generateTimer <= 0)
        {
            generateTimer = generateTimerMax;

            GenarateAcid();
        }    
    }

    private void GenarateAcid()
    {
        GameObject acid = AcidList[UnityEngine.Random.Range(0, AcidList.Count)];
        GameObject iconTransform = Instantiate(acid, generateTrans);
    }
}
