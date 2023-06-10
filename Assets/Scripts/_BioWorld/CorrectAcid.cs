using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CorrectAcid : BaseCounter
{
    [SerializeField] private KitchenObjSO kitchenObjSO;
    [SerializeField] private AcidDeliveryResultUI acidDeliveryResultUI;

    public static event EventHandler OnCorrectPlace;


    /* Extend BaseCounter*/
    public override void Interact(PlayerMovement player)
    {
        // 桌上沒東西
        if(!HasKitchenObj())
        {
            // Player is carrying something
            if(player.HasKitchenObj())
            {
                player.GetKitchenObj().SetKitchenObjParent(this);

                acidDeliveryResultUI.CorrectAcid_OnCorrectPlace();
                OnCorrectPlace?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
