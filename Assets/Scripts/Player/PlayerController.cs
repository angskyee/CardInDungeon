using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<GameObject, Vector2> OnSelectCardEvent;
    
    protected bool IsClicking { get; set; }

    public void CallSelectCardEvent(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(direction, Vector2.zero, 0f);

        if (IsClicking == false || hit.collider == null)
        {
            return;
        }
        if (hit.collider && IsClicking == true)
        {
            OnSelectCardEvent?.Invoke(hit.collider.gameObject, direction);
        }
    }
}
