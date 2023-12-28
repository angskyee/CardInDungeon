using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<GameObject, Vector2> OnSelectPlayerCharacterCardEvent;
    public event Action OnSelectPlayerInventoryCardEvent;
    public event Action<Vector2> OnInventoryMove;
    protected bool IsClicking { get; set; }

    public void CallSelectCardEvent(Vector2 direction)
    {
        
        RaycastHit2D hit = Physics2D.Raycast(direction, Vector2.zero, 0f);


        if (IsClicking == false || hit.collider == null)
        {
            return;
        }
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (hit.collider.CompareTag("Character"))
            {
                CharacterContactCardController _contact = hit.collider.GetComponent<CharacterContactCardController>();
            
                if( _contact.ContactEnemy == true && IsClicking == true)
                    OnSelectPlayerCharacterCardEvent?.Invoke(hit.collider.gameObject, direction);
            }
            else if(hit.collider.CompareTag("Inventory"))
            { 
                Debug.Log("Inventoty");
                OnSelectPlayerInventoryCardEvent?.Invoke();
            }
            
        }
    }

    public void CallInventoryLook(Vector2 direction)
    {
        OnInventoryMove?.Invoke(direction);
    }
}
