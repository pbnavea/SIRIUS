﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//PARENT CLASS FOR SCRIPTS ATTACHED TO PLAYERS/CHESTS INVENTORY
public class ItemHolder : MonoBehaviour
{
    [SerializeField]
    private ushort inventorySize;   //size of the inventory

    [SerializeField]
    private ushort [,] itemsInInventory;    //items that are in the inventory row 0 = item id, row 1 = amount of that item
                                            //[0,0] id [1,0] amount

    void Start(){
        itemsInInventory = new ushort[2, inventorySize];
    }
     
    //Items picked up adding to first slot
    public ushort AddItem(ItemDescription itemDescription, ushort amount, InventoryHandlerScript inventoryHandler, ushort slotIndex){
        if (amount == 0 || slotIndex >= inventorySize)
        {
            return amount;
        }
        for (int column = slotIndex; column < inventorySize; column++)
        {
            if(itemsInInventory[0, column] == (ushort)EnumClass.TileEnum.EMPTY)
            {
                itemsInInventory[0, column] = itemDescription.id;
                itemsInInventory[1, column] = (ushort)Mathf.Clamp(amount, 0, itemDescription.stackAmnt);
                amount -= itemsInInventory[1, column];
                return (AddItem(itemDescription, amount, inventoryHandler, (ushort)(slotIndex++)));
            }
            if (itemsInInventory[0, column] == itemDescription.id)
            {
                int rAmount = amount - (itemDescription.stackAmnt - itemsInInventory[1, column]);
                amount = (ushort)(Mathf.Clamp(rAmount, 0, amount));
                return (AddItem(itemDescription, amount, inventoryHandler, (ushort)(slotIndex ++)));
            }
            else{
                return (AddItem(itemDescription, amount, inventoryHandler, (ushort)(slotIndex++)));
            }
        }
        return amount;
    }


    public void AddItemToSlot(ItemDescription item, ushort amount, InventorySlot newslot, InventorySlot parentSlot)
    {
        if(item.id == newslot.id)
        {
            // Amount being stored is less than max amount
            if(amount < item.stackAmnt)
            {
                ushort stackDifference = (ushort)(item.stackAmnt - amount);
                if(amount <= stackDifference)
                {
                    newslot.currentstack += amount;     // Update stack count
                    //parentslot to null
                }
                else
                {
                    stackDifference -= amount;
                    newslot.currentstack += stackDifference;    // Add what can be added to newSlot
                    AddItemToSlot(item, stackDifference, parentSlot, parentSlot);    //Return rest to parentSlot
                }
            }
            else
            {
                AddItemToSlot(item, amount, parentSlot, parentSlot);
            }
        }

    }

    public ushort FetchItemIdInInventorySlot(ushort slot)
    {
        if(slot < inventorySize)
        {
            return itemsInInventory[0, slot];
        }
        return 0;
    }

    public ushort FetchItemAmountInInventorySlot(ushort slot)
    {
        if (slot < inventorySize)
        {
            return itemsInInventory[1, slot];
        }
        return 0;
    }

    public ushort GetInventorySize()
    {
        return inventorySize;
    }
}
