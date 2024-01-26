using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform thumbStickTransform;
    [SerializeField] RectTransform backGroundTransform;
    [SerializeField] RectTransform centerTransform;
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"On drag fired {eventData.position}");
        Vector2 touchPosition = eventData.position;
        Vector2 centerPosition = backGroundTransform.position;

        Vector2 localOffSet = Vector2.ClampMagnitude(touchPosition - centerPosition, backGroundTransform.sizeDelta.x/2);
        thumbStickTransform.position = centerPosition + localOffSet;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        backGroundTransform.position = eventData.position;
        thumbStickTransform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        backGroundTransform.position = centerTransform.position;
        thumbStickTransform.position = backGroundTransform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
