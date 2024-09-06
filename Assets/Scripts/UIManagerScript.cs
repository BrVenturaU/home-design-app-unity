using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class UIManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenuCanvas;
    [SerializeField]
    private GameObject _itemsMenuCanvas;
    [SerializeField]
    private GameObject _arMenuCanvas;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance().OnShowMainMenu += ActivateMainMenu;
        GameManager.GetInstance().OnShowItemsMenu += ActivateItemsMenu;
        GameManager.GetInstance().OnShowARPositions += ActivateARMenu;
    }

    private void ActivateMainMenu()
    {
        _mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1,1 ,1), 0.3f);
        _mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1,1 ,1), 0.3f);
        _mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1,1 ,1), 0.3f);

        _itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        _itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        _itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(180, 0.3f);

        _arMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        _arMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
    }

    private void ActivateItemsMenu()
    {
        _mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        _mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        _mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        _itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1,1), 0.5f);
        _itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        _itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(300, 0.3f);
    }

    private void ActivateARMenu()
    {
        _mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        _mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        _mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        _itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        _itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        _itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(180, 0.3f);

        _arMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        _arMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
