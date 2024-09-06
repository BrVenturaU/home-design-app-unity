using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemButtonManager : MonoBehaviour
{
    public string Name { private get; set; }
    public string Description { private get; set; }
    public Sprite Sprite { private get; set; }
    public GameObject Model { private get; set; }

    private ARInteractionManagerScript _interactionManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = Name;
        transform.GetChild(1).GetComponent<TMP_Text>().text = Description;
        transform.GetChild(2).GetComponent<RawImage>().texture = Sprite.texture;
        

        var btn = GetComponent<Button>();
        btn.onClick.AddListener(GameManager.GetInstance().ShowARPositions);
        btn.onClick.AddListener(Create3DModel);

        _interactionManager = FindObjectOfType<ARInteractionManagerScript>();
    }

    private void Create3DModel()
    {
        _interactionManager.ItemModel = Instantiate(Model);
    }
}
