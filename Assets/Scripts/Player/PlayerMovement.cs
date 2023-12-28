using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController _controller;
    private CharacterContactCardController _contact;
    private GameObject _gameObject;
    public GameObject instanceCharacter;
    public GameObject character;
    private Inventory _inventory;
    private bool isInstance;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _gameObject = gameObject;
        _inventory = GetComponent<Inventory>();
        instanceCharacter.SetActive(false);
        isInstance = false;
    }

    private void Start()
    {
        _controller.OnSelectPlayerCharacterCardEvent += Move;
        _controller.OnInventoryMove += InventoryMovement;
    }

    private void InventoryMovement(Vector2 direction)
    {
        _contact = GetComponent<CharacterContactCardController>();
        
        if (_contact.ContactInventory)
        {
            if (isInstance == false && !instanceCharacter.activeSelf)
            {
                instanceCharacter.transform.position = character.transform.position;
                instanceCharacter.SetActive(true);
                character.SetActive(false);
                isInstance = true;
            }

            if (instanceCharacter.activeSelf)
            {
                Vector2[] LayerDirection = new Vector2[4];
                LayerDirection[0] = new Vector2(direction.x, direction.y + 0.5f);
                LayerDirection[1] = new Vector2(direction.x, direction.y + -0.5f);
                LayerDirection[2] = new Vector2(direction.x + 0.5f, direction.y);
                LayerDirection[3] = new Vector2(direction.x - 0.5f, direction.y);

                RaycastHit2D[] raycastHit2Ds = new RaycastHit2D[4];
                int[] layers = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    raycastHit2Ds[i] = Physics2D.Raycast(LayerDirection[i], Vector2.zero, 0f);
                    if (raycastHit2Ds[i].collider != null)
                    {
                        layers[i] = raycastHit2Ds[i].collider.gameObject.layer;
                    }
                }
                if (layers[0] == 8 || layers[1] == 8 || layers[2] == 8 || layers[3] == 8 || layers[0] == 6 || layers[1] == 6 || layers[2] == 6 || layers[3] == 6 )
                {
                    instanceCharacter.transform.position = Vector2.Lerp(instanceCharacter.transform.position, direction, 0.1f);
                }
                else
                {
                    isInstance = false;
                    _contact.ContactInventory = false;
                    while (Vector2.Distance(instanceCharacter.transform.position, character.transform.position) > 0.1f)
                    {
                        instanceCharacter.transform.position = Vector2.Lerp(instanceCharacter.transform.position, character.transform.position, 0.00001f);
                        if(Vector2.Distance(instanceCharacter.transform.position, character.transform.position) < 0.2f)
                        {
                            instanceCharacter.SetActive(false);
                            character.SetActive(true);
                            _inventory.inventoryWindow.SetActive(false);
                        }
                    }

                }
            }
        }
    }

    private void Move(GameObject obj, Vector2 direction)
    {
        obj.transform.position = direction;
    }
}
