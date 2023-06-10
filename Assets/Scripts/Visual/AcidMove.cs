using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidMove : MonoBehaviour
{
    [SerializeField] private bool speedMode;
    [SerializeField] private float speedSlow;
    [SerializeField] private float speedFast;


    private void Update() 
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(-1f, 0f, 0f);

        if(speedMode)
        {
            transform.position += moveDirection * speedFast * 0.001f;
        }
        else
        {
            transform.position += moveDirection * speedSlow * 0.001f;
        }
    }
}
