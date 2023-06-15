using UnityEngine;
using System;
using System.Collections.Generic;

public class DeliveryAcid : BaseCounter
{
    [SerializeField] private AcidDeliveryResultUI acidDeliveryResultUI;

    [SerializeField] private RecipeSO correctSugerRecipeSO;
    [SerializeField] private float DestoryTimerMax = 30f;

    public static event EventHandler OnIncorrectPlace;
    public static event EventHandler OnCorrectPlace;
    public static EventHandler OnAcidDestory;
    [SerializeField] private GameObject DestoryGamePrefabs;


    // Sync Icon and Generate List
    private List<RecipeSO> waitingRecipeSOList;
    public static EventHandler<OnWaitingRecipeSOListChangedEventArgs> OnWaitingRecipeSOListChanged;
    public class OnWaitingRecipeSOListChangedEventArgs : EventArgs
    {
        public List<RecipeSO> _waitingRecipeSOList;
    }

    private void Start() 
    {
        DestoryOverTime();
        
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, DeliveryManager.OnRecipeSpawnedEventArgs e)
    {
        waitingRecipeSOList = DeliveryManager.Instance.GetWaitingRecipeSOList();
    }

    /* Extend BaseCounter */
    public override void Interact(PlayerMovement player)
    {
        // 桌上沒東西
        if(!HasKitchenObj())
        {
            // Player is carrying something
            if(player.HasKitchenObj() && player.GetKitchenObj().TryGetPlate(out PlateKitchenObj plateKitchenObj))
            {
                DeliverRecipeCorrect(plateKitchenObj, player);
            }
        }
    }

    private void DeliverRecipeCorrect(PlateKitchenObj plateKitchenObj, PlayerMovement player)
    {
        bool plateContentsMatchesRecipe = true;
        
        foreach(KitchenObjSO plateKitchenObjSO in plateKitchenObj.GetKitchenObjSOListOnPlate())
        {
            bool ingredientFound = false;

            foreach(KitchenObjSO correctKitchenObjSO in correctSugerRecipeSO.kitchenObjSOList)
            {
                if(plateKitchenObjSO == correctKitchenObjSO)
                {
                    ingredientFound = true;
                    break;
                }

            }
            
            if(!ingredientFound)
            {
                plateContentsMatchesRecipe = false;
            }
        }

            // Correct
            if(plateContentsMatchesRecipe)
            {
                waitingRecipeSOList.Remove(correctSugerRecipeSO);
                OnWaitingRecipeSOListChanged?.Invoke(this, new OnWaitingRecipeSOListChangedEventArgs
                {
                    _waitingRecipeSOList = waitingRecipeSOList
                });

                player.GetKitchenObj().SetKitchenObjParent(this);

                // correct UI
                acidDeliveryResultUI.CorrectAcid_OnCorrectPlace();
                OnCorrectPlace?.Invoke(this, EventArgs.Empty);

                return;
            }
        
        

        // Incorrect 全部判定還是沒
        acidDeliveryResultUI.InCorrectAcid_OnInCorrectPlace();
        OnIncorrectPlace?.Invoke(this, EventArgs.Empty);
    }

    private void DestoryOverTime()
    {
        Invoke("RemovefromList", DestoryTimerMax);
        Destroy(DestoryGamePrefabs, DestoryTimerMax);
    }
    private void RemovefromList() 
    {
        waitingRecipeSOList.Remove(correctSugerRecipeSO);
        OnWaitingRecipeSOListChanged?.Invoke(this, new OnWaitingRecipeSOListChangedEventArgs
        {
            _waitingRecipeSOList = waitingRecipeSOList
        });
    }
}
