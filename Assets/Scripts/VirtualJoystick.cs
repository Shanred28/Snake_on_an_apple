using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _joystickBackground;
    [SerializeField] private Image _stick;

    [HideInInspector] public float Direction;
    public Vector3 Value { get; private set; }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position, eventData.pressEventCamera, out position);

        position.x = (position.x / _joystickBackground.rectTransform.sizeDelta.x);
        position.y = (position.y / _joystickBackground.rectTransform.sizeDelta.y);

        position.x = position.x * 2 - 1;
        position.y = position.y * 2 - 1;

       Value = new Vector3(position.x, position.y, 0);

        if (Value.magnitude > 1)
            Value = Value.normalized;

        float offsetX = _joystickBackground.rectTransform.sizeDelta.x / 2 - _stick.rectTransform.sizeDelta.x / 2;
        float offsetY = _joystickBackground.rectTransform.sizeDelta.y / 2 - _stick.rectTransform.sizeDelta.y / 2;

        _stick.rectTransform.anchoredPosition = new Vector2(Value.x * offsetX, Value.y * offsetY);
        Direction = Value.x;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Value = Vector3.zero;
        _stick.rectTransform.anchoredPosition = Vector3.zero;
    }
}


