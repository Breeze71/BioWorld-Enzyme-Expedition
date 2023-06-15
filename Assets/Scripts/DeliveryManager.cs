using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler<OnRecipeSpawnedEventArgs> OnRecipeSpawned;
    public class OnRecipeSpawnedEventArgs : EventArgs
    {
        public RecipeSO generateRecipeSO;
    }
    
    //public event EventHandler OnRecipeCompleted;

    // sound event 
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;


    public static DeliveryManager Instance{ get; private set;}

    [SerializeField] private RecipeListSO recipeListSO;

    # region variable
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    [SerializeField] private float spawnRecipeTimerMax = 5f;
    [SerializeField] int waitingRecipesMax = 4;
    private int successfulRecipeAmount;
    # endregion

    private void Awake() 
    {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }

    // OnWaitingRecipeSOListChanged
    private void Start() 
    {
        DeliveryAcid.OnWaitingRecipeSOListChanged += DeliveryAcid_OnWaitingRecipeSOListChanged;
    }
    private void DeliveryAcid_OnWaitingRecipeSOListChanged(object sender, DeliveryAcid.OnWaitingRecipeSOListChangedEventArgs e)
    {
        waitingRecipeSOList = e._waitingRecipeSOList;
    }

    private void Update() 
    {
        spawnRecipeTimer -= Time.deltaTime;

        if(spawnRecipeTimer <= 0)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            
            // 如果目前訂單數小於 waitingRecipesMax  && isPlaying() 隨機生成
            if(GameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);

                // 傳遞要生成的 Acid
                OnRecipeSpawned?.Invoke(this, new OnRecipeSpawnedEventArgs
                {
                    generateRecipeSO = waitingRecipeSO
                });
            }
        }
    }

    
    public void DeliverRecipe(PlateKitchenObj plateKitchenObj)
    {
        
        for(int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            // 需要的食材跟盤上一樣
            if(waitingRecipeSO.kitchenObjSOList.Count == plateKitchenObj.GetKitchenObjSOListOnPlate().Count)
            {
                bool plateContentsMatchesRecipe = true; 

                foreach(KitchenObjSO recipeKitchenObjSO in waitingRecipeSO.kitchenObjSOList)
                {
                    bool ingredientFound = false;

                    foreach(KitchenObjSO plateKitchenObjSO in plateKitchenObj.GetKitchenObjSOListOnPlate())
                    {
                        if(plateKitchenObjSO == recipeKitchenObjSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    // this gredient was not found on the plate
                    if(!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }

                // player delivered the correct recipe
                if(plateContentsMatchesRecipe)
                {
                    waitingRecipeSOList.RemoveAt(i);

                    successfulRecipeAmount++;

                    // event
                    //OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                    return;
                }
            }
        }

        // no match found
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }
    
    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipe()
    {
        return successfulRecipeAmount;
    }
}
