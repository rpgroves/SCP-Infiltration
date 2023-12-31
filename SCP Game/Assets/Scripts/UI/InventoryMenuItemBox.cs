using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuItemBox : MonoBehaviour
{
    [SerializeField] GameObject ItemBoxImage;
    [SerializeField] InventoryMenu inventoryMenu;
    InventoryObjectSO item;
    [SerializeField] bool isEquipped = false;

    Canvas canvas;
    Camera mainCamera;

    void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        
    }

    public void ClickBox()
    {
        inventoryMenu.UpdateItemMenu(item, isEquipped);
    }

    public void UpdateBox(InventoryObjectSO i)
    {
        item = i;
        ItemBoxImage.GetComponent<Image>().sprite = item.getSpriteBlack();
    }
}
