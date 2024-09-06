using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARInteractionManagerScript : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    private ARRaycastManager _raycastManager;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private GameObject _arPointer;
    private GameObject _itemModel;
    private GameObject itemSelected;
    private bool _isInitialPosition;
    private bool _isOverUI;
    private bool isOver_itemModel;

    private Vector2 initialTouchPos;
    public GameObject ItemModel
    {
        set { 
            _itemModel = value;
            _itemModel.transform.position = _arPointer.transform.position;
            _itemModel.transform.parent = _arPointer.transform;
            _isInitialPosition = true;
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        _arPointer = transform.GetChild(0).gameObject;
        _raycastManager = FindObjectOfType<ARRaycastManager>();
        GameManager.GetInstance().OnShowMainMenu += SetItemPosition;
    }

    private void SetItemPosition()
    {
        if (_itemModel != null)
        {
            _itemModel.transform.parent = null;
            _arPointer.SetActive(false);
            _itemModel = null;
        }
    }

    public void RemoveItem()
    {
        Destroy(_itemModel);
        _arPointer.SetActive(false);
        GameManager.GetInstance().ShowMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInitialPosition) {
            Vector2 midScreenPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            _raycastManager.Raycast(midScreenPoint, _hits, TrackableType.Planes);
            if (_hits.Count > 0) {
                transform.position = _hits[0].pose.position;
                transform.rotation = _hits[0].pose.rotation;
                _arPointer.SetActive(true);
                _isInitialPosition = false;
            }
        }

        if (Input.touchCount > 0) { 
            Touch firstTouch = Input.GetTouch(0);

            if (firstTouch.phase == TouchPhase.Began) {
                var touchPosition = firstTouch.position;
                _isOverUI = IsTapOverUI(touchPosition);
                isOver_itemModel = IsTapOver_itemModel(touchPosition);
            }

            if(firstTouch.phase == TouchPhase.Moved)
            {
                if (_raycastManager.Raycast(firstTouch.position, _hits, TrackableType.Planes))
                {
                    Pose hitPose = _hits[0].pose;
                    if (!_isOverUI && isOver_itemModel)
                    {
                        transform.position = hitPose.position;
                    }
                }
            }
            if (Input.touchCount == 2)
            {
                Touch touchTwo = Input.GetTouch(1);
                if (firstTouch.phase == TouchPhase.Began || touchTwo.phase == TouchPhase.Began)
                {
                    initialTouchPos = touchTwo.position - firstTouch.position;
                }
                if(firstTouch.phase == TouchPhase.Moved || touchTwo.phase == TouchPhase.Moved)
                {
                    Vector2 currentTouchPos = touchTwo.position - firstTouch.position;
                    float angle = Vector2.SignedAngle(initialTouchPos, currentTouchPos);
                    _itemModel.transform.rotation = Quaternion.Euler(0, _itemModel.transform.eulerAngles.y - angle, 0);
                    initialTouchPos = currentTouchPos;
                }
            }
            if (isOver_itemModel && _itemModel == null && !_isOverUI)
            {
                GameManager.GetInstance().ShowARPositions();
                _itemModel = itemSelected;
                itemSelected = null;
                _arPointer.SetActive(true);
                transform.position = _itemModel.transform.position;
                _itemModel.transform.parent = _arPointer.transform;
            }
        }
    }

    private bool IsTapOver_itemModel(Vector2 touchPosition)
    {
         Ray ray = _camera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit_itemModel))
        {
            if(hit_itemModel.collider.CompareTag("Item"))
            {
                itemSelected = hit_itemModel.transform.gameObject;
                return true;
            }
        }
        return false;

    }

    private bool IsTapOverUI(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touchPosition.x, touchPosition.y);
        var raycastResult = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResult);

        return raycastResult.Count > 0;
    }
}
