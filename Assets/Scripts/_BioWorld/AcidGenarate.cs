using System;
using System.Collections.Generic;
using UnityEngine;

public class AcidGenarate : MonoBehaviour
{
    [Serializable]
    public struct Acid_WaitingRecipeSO
    {
        public GameObject acid;
        public RecipeSO waitingRecipeSO;
    }
    [Header("Acid To WaitingRecipeSO")]
    [SerializeField] private List<Acid_WaitingRecipeSO> acid_WaitingRecipeSOList;


    [Header("Generate")]
    [SerializeField] private Transform generateTransform;


    private void Start() 
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, DeliveryManager.OnRecipeSpawnedEventArgs e)
    {
        RecipeSO generateAcid = e.generateRecipeSO;

        foreach(Acid_WaitingRecipeSO acid_WaitingRecipeSO in acid_WaitingRecipeSOList)
        {
            if(acid_WaitingRecipeSO.waitingRecipeSO == generateAcid)
            {
                Instantiate(acid_WaitingRecipeSO.acid, generateTransform);
            }
        }
    }

}
