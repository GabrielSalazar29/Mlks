using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    public static InventoryController instance;
    public List<GameObject> inventory = new();
    private int _inventorySize = 5;
    private Vector2 _inventoryInput;
    private int selectedItem = 0;
    private bool hasSpace;
    private float scrollDelay = 0.5f;
    private float lastScrollTime = 0f;
    
    void Start()
    {
        inventory = new List<GameObject>(new GameObject[_inventorySize]);
        print($"Inventário de {inventory.Count} espaços vazios");
    }
    
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    public void OnScrollWheel(InputValue value)
    {
        _inventoryInput = value.Get<Vector2>();

        if (Time.time - lastScrollTime >= scrollDelay)
        {
            if (_inventoryInput.y != 0)
            {
                selectedItem = (selectedItem + (int)Mathf.Sign(_inventoryInput.y) + inventory.Count) % inventory.Count;
                
                if (inventory[selectedItem] != null)
                {
                    print(inventory[selectedItem].name);
                }
                else
                {
                    print($"Slot {selectedItem} está vazio.");
                }
                lastScrollTime = Time.time;
            }
        }
    }

    public bool AddItemToInventory(GameObject item)
    {
        for (int i = 0; i <= inventory.Count - 1; i++)
        {
                if (!inventory[i])
                {
                    inventory[i] = item;
                    return true;
                }
        }
        return false;
    }
}
