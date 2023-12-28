using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void OnInteract();
}

public class InteractionManager : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public LayerMask layerMask;

    private GameObject curInteractGameobject;
    private IInteractable curInteractable;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject != curInteractGameobject && other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            curInteractGameobject = other.gameObject;
            curInteractable = other.gameObject.GetComponent<IInteractable>();
            curInteractable.OnInteract();
            curInteractGameobject = null;
            curInteractable = null;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        curInteractGameobject = null;
        curInteractable = null;
    }
}