using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class _Sc_inventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [HideInInspector] public _So_item _item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public TextMeshProUGUI countText = null;
    public Transform parentAfterDrag = null;
    public Transform previousSlotParent = null;

    private _Sc_cerveau _sc_cerveau = null;
    Image image = null;
    string itemName;

    [SerializeField] LayerMask myLayerMask;
    [SerializeField] string layerName = "Walkable";
    private int LayerGround;

    [SerializeField] GameObject itemLdo = null;
    [SerializeField] float spawnMaxRadius = 10.0f;
    _Sc_tooltipTrigger _sc_tolltipTrigger = null;
    _Sc_CraftManager _sc_CraftManager = null;
    _Sc_ressourcesPremeption _ressourcesPeremption = null;
    _Sc_formulaDisplay _formulaDisplay = null;
    //doucle click values
    [SerializeField] float clicked = 0;
    [SerializeField] float clicktime = 0;
    [SerializeField] float clickdelay = 0.5f;
    [SerializeField] LayerMask _layerMask;
    private void Awake()
    {
        image = GetComponent<Image>();
        countText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        previousSlotParent = transform.parent;
        _sc_tolltipTrigger = transform.GetChild(1).GetComponent<_Sc_tooltipTrigger>();
    }
    private void Start()
    {
        _sc_cerveau = _Sc_cerveau.instance;
        LayerGround = LayerMask.NameToLayer(layerName);
        _sc_CraftManager = _Sc_CraftManager.instance;
        _ressourcesPeremption = _Sc_ressourcesPremeption.instance;
        _formulaDisplay = _Sc_formulaDisplay.instance;
    }

    public void InitializeItem(_So_item _newItem)
    {
        _item = _newItem;
        image.sprite = _newItem.image;
        itemName = _newItem.itemName;
        SetCount();
    }

    public void SetCount()
    {
        countText.text = count.ToString();
        if(count > 1)
        {
            countText.gameObject.SetActive(true);
        }
        else if( count > 0)
        {
            countText.gameObject.SetActive(false);            
        }
        else
        {
            Destroy(gameObject);
        }

        VisualFeedback();

        if(_sc_tolltipTrigger != null)
        {
            if(count > 1)
            {
                _sc_tolltipTrigger.header = _item.itemName.ToString() + "(" + count.ToString() + ")";
            }
            else
            {
                _sc_tolltipTrigger.header = _item.itemName.ToString();
            }
        }
       
    }

    private void VisualFeedback()
    {
        image.transform.DORewind();
        image.transform.DOKill();
        image.transform.DOPunchScale(new Vector3(-0.25f, 0.25f, 0.0f), 0.35f, 10, 1);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _sc_tolltipTrigger.transform.GetComponent<Image>().raycastTarget = false;
        image.raycastTarget = false;
        _sc_cerveau.canMove = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.parent.parent.parent.parent);
        transform.SetAsLastSibling();
        _sc_CraftManager.OnMouseDownItem(_item);
        _sc_CraftManager.lastUsedIventoryItem = this;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = _sc_cerveau.mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Drag EndSlot");
            EndMoveSlot();       
        }
        else
        {
            Debug.Log("Drag Drop");
            DropItem();           
        }
    }

    private void DropItem()
    {
        _sc_cerveau.canMove = true;

        GameObject _itemSpawned = Instantiate(itemLdo, RandomPointInCircle(_sc_cerveau.transform.position, spawnMaxRadius), _sc_cerveau.transform.rotation);
        _itemSpawned.transform.position = new Vector3(_itemSpawned.transform.position.x, GetHeightDrop(_itemSpawned.transform)+0.25f, _itemSpawned.transform.position.z);

        //Juice
        _itemSpawned.transform.GetChild(0).DOPunchScale(new Vector3(0.25f, 0.25f, 0.25f), 0.35f, 10, 1);
        _itemSpawned.transform.GetChild(0).DOJump(_itemSpawned.transform.GetChild(0).position, 1, 1, 0.5f, false);
        //

        _Sc_itemLdo _sc_itemLdo = _itemSpawned.GetComponent<_Sc_itemLdo>();

        _sc_itemLdo._item = _item;
        _sc_itemLdo.count = count;
        _ressourcesPeremption.ClearList(_sc_itemLdo._item);
        //StartCoroutine(_Sc_inventoryManager.instance.CheckInventoryDelay());
        _formulaDisplay.UpdateAutoButton();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _Sc_inventoryManager.instance.CheckInventory();
    }

    public Vector3 RandomPointInCircle(Vector3 origin, float maxRadius)
    {
        Vector3 centerOfRadius = origin;
        float radius = maxRadius;
        Vector3 target = centerOfRadius + (Vector3)(radius * UnityEngine.Random.insideUnitSphere);
        return target;
    }

    public void EndMoveSlot()
    {
        _sc_tolltipTrigger.transform.GetComponent<Image>().raycastTarget = true;
        image.raycastTarget = true;
        _sc_cerveau.canMove = true;
        transform.SetParent(parentAfterDrag);
        previousSlotParent = transform.parent;
    }
    //double click
    public void OnPointerDown(PointerEventData data)
    {
        if(_sc_CraftManager.CheckFreeSlots() > 0)
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;

            if (clicked > 1 && Time.time - clicktime < clickdelay)
            {
                clicked = 0;
                clicktime = 0;
                Debug.Log("Double CLick: " + this.GetComponent<RectTransform>().name);
                _sc_CraftManager.AddInFirstFreeSlot(_item);
            }
            else if (clicked > 2 || Time.time - clicktime > 1)
            {
                clicked = 0;
            }
        }
        else
        {
            clicked = 0;

        }
    }

    public float GetHeightDrop(Transform itemDropped)
    {
        RaycastHit hit;
        if (Physics.Raycast(itemDropped.position + new Vector3(0,100,0), itemDropped.TransformDirection(Vector3.down), out hit, Mathf.Infinity, _layerMask))
        {
            return hit.point.y;
        }
        else
        {
            return 0;
        }

    }
}
