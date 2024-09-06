using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagerScript : MonoBehaviour
{
    [SerializeField]
    private List<Item> _items = new List<Item>();
    [SerializeField]
    private GameObject _btnContainer;
    [SerializeField]
    private ItemButtonManager _btnManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance().OnShowItemsMenu += CreateButtons;
    }

    private void CreateButtons()
    {
        foreach (var item in _items) { 
            var itemButtonManager = Instantiate(_btnManager, _btnContainer.transform);
            itemButtonManager.Name = item.Name;
            itemButtonManager.Description = item.Description;
            itemButtonManager.Sprite = item.Image;
            itemButtonManager.Model = item.Model;

            itemButtonManager.name = item.Name;
        }

        GameManager.GetInstance().OnShowItemsMenu -= CreateButtons;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
