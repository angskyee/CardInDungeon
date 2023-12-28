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
    public event Action<int> OnEquipItemEvent;
    
    private PlayerController _controller;
    private PlayerContactCardController _contact;
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
        _contact = GetComponent<PlayerContactCardController>();
        
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
                Vector2[] LayerDirection = new Vector2[5];
                LayerDirection[0] = new Vector2(direction.x, direction.y + 0.5f);
                LayerDirection[1] = new Vector2(direction.x, direction.y + -0.5f);
                LayerDirection[2] = new Vector2(direction.x + 0.5f, direction.y);
                LayerDirection[3] = new Vector2(direction.x - 0.5f, direction.y);
                LayerDirection[4] = new Vector2(direction.x, direction.y);

                RaycastHit2D[] raycastHit2Ds = new RaycastHit2D[LayerDirection.Length];
                int[] layers = new int[LayerDirection.Length];
                for (int i = 0; i < LayerDirection.Length; i++)
                {
                    raycastHit2Ds[i] = Physics2D.Raycast(LayerDirection[i], Vector2.zero, 0f);
                    if (raycastHit2Ds[i].collider != null)
                    {
                        layers[i] = raycastHit2Ds[i].collider.gameObject.layer;
                    }
                }
                if (layers[0] == 8 || layers[1] == 8 || layers[2] == 8 || layers[3] == 8 || layers[4] == 8 || layers[0] == 6 || layers[1] == 6 || layers[2] == 6 || layers[3] == 6|| layers[4] == 6 || layers[0] > 9 || layers[1] > 9 || layers[2] > 9 || layers[3] > 9 || layers[4] > 9)
                {
                    instanceCharacter.transform.position = Vector2.Lerp(instanceCharacter.transform.position, direction, 0.1f);
                    OnEquipItemEvent?.Invoke(layers[4]);
                }
                else
                {
                    isInstance = false;
                    _contact.ContactInventory = false;

                    StartCoroutine(SlowMove());
                }
            }
        }
    }

    private IEnumerator SlowMove()
    {
        float lerpTime = 0f;
        float duration = 2f; // 이동에 걸리는 전체 시간

        while (lerpTime < 1f)
        {
            lerpTime += Time.deltaTime / duration;
            instanceCharacter.transform.position = Vector2.Lerp(instanceCharacter.transform.position, character.transform.position, lerpTime);

            yield return null; // 한 프레임 대기
            
            if (Vector2.Distance(instanceCharacter.transform.position, character.transform.position) < 0.1f)
            {
                instanceCharacter.SetActive(false);
                character.SetActive(true);
                _inventory.inventoryWindow.SetActive(false);
                break;
            }
        }
    }

    private void Move(GameObject obj, Vector2 direction)
    {
        obj.transform.position = Vector2.Lerp(obj.transform.position, direction, 0.1f);
    }
}
