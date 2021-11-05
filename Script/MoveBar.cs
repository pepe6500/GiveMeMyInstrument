using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MoveBar : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] Camera viewCamera;
    [SerializeField] RectTransform handle;
    [SerializeField] RectTransform canvas;
    [SerializeField] float value;
    [SerializeField] float maxPos;
    [SerializeField] float minValue;
    [SerializeField] PlayerController player;

    bool drag;
    Vector3 upPoint;

    [SerializeField] EventTrigger trigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!drag)
        {
            value = 0;
            handle.localPosition *= 0.5f;
        }
        else
        {
            if(value > 0)
            player.OnMove(minValue + (value*(1-minValue)));
            else
                player.OnMove(-minValue + (value * (1 - minValue)));

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        drag = true;
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float pointer = (viewCamera.ScreenToWorldPoint(eventData.position).x / canvas.localScale.x) - GetComponent<RectTransform>().localPosition.x;
        if (pointer > maxPos)
        {
            pointer = (maxPos);
        }
        else if(pointer < -maxPos)
        {
            pointer = -maxPos;
        }
        handle.localPosition = new Vector3(pointer, 0);
        value = pointer / maxPos;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        drag = false;
        upPoint = GetComponent<RectTransform>().localPosition;
    }
}
