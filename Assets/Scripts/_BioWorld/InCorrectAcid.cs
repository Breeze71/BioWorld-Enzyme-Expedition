using System;
using UnityEngine;

public class InCorrectAcid : BaseCounter
{
    public static event EventHandler OnIncorrectPlace;

    [SerializeField] private AcidDeliveryResultUI acidDeliveryResultUI;

    public override void Interact(PlayerMovement player)
    {   
        acidDeliveryResultUI.InCorrectAcid_OnInCorrectPlace();
        OnIncorrectPlace?.Invoke(this, EventArgs.Empty);
    }

}
