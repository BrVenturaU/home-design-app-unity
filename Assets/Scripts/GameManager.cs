using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    public event Action OnShowMainMenu;
    public event Action OnShowItemsMenu;
    public event Action OnShowARPositions;

    private static GameManager _instance;
    private GameManager()
    {

    }

    public static GameManager GetInstance()
    {
        if(_instance == null)
            _instance = new GameManager();
        return _instance;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else { 
            _instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMainMenu()
    {
        OnShowMainMenu?.Invoke();
        Debug.Log("Main menu event executed");
    }

    public void ShowItemsMenu()
    {
        OnShowItemsMenu?.Invoke();
        Debug.Log("Items menu event executed");
    }

    public void ShowARPositions()
    {
        OnShowARPositions?.Invoke();
        Debug.Log("ARPosition menu event executed");
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
