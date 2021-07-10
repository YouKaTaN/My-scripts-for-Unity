using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int amount;

    public InventorySlot(Item item, int amount = 1)
    {
        this.item = item;
        this.amount = amount;
    }
}

public class Inventory : MonoBehaviour 
{
    [SerializeField] private List<InventorySlot> items = new List<InventorySlot>();
    [SerializeField] private int maxSize = 4;
    [SerializeField] private UnityEvent OnInventoryChanged;
    
    public static int numberButton;

    private void Start()
    {
        OnInventoryChanged.Invoke();
    }

    private void OnEnable()
    {
        GameManager.UpdateMashroomTotal += UpdateMashroomTotal;
        InventoryUI.RemoveAmount += RemoveItems;
    }

    private void OnDisable()
    {
        GameManager.UpdateMashroomTotal -= UpdateMashroomTotal;
        InventoryUI.RemoveAmount -= RemoveItems;
    }

    public bool AddItems(Item item, int amount = 1) {
        foreach (InventorySlot slot in items) {
            if (slot.item.id == item.id) 
            {
                slot.amount += amount;
                OnInventoryChanged.Invoke();
                return true;
            }
        }

        if (items.Count >= maxSize) return false;

        InventorySlot new_slot = new InventorySlot(item, amount);
        items.Add(new_slot);
        OnInventoryChanged.Invoke();
        
        return true;
    }
    public void RemoveItems()
    {
        foreach (InventorySlot slot in items)
            if (slot.item.id == numberButton)
            {
                slot.amount -= 1;
            }
            OnInventoryChanged.Invoke();
    }

    public Item GetItem(int i)
    {
        return i < items.Count ? items[i].item : null;
    }
    public int GetAmount(int i)
    {
        return i < items.Count ? items[i].amount : 0;
    }
    public int GetSize()
    {
        return items.Count;
    }
    public void UpdateMashroomTotal()
    {
        for (int i = 0; i < GetSize(); i++)
        {
            PlayerPrefs.SetInt("Mashroom" + i, GetAmount(i));
            PlayerPrefs.SetInt("MashroomTotal" + i, PlayerPrefs.GetInt("MashroomTotal" + i) + GetAmount(i));
        }
    }
}