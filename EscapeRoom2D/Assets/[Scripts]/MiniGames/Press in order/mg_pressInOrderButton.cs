using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class mg_pressInOrderButton : MonoBehaviour, IPointerClickHandler
{

    public int number;
    mg_pressInOrder manager = null;

    public void SetNumber(mg_pressInOrder _manager, int _number)
    {
        number = _number;
        manager = _manager;

        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.SetText(_number.ToString());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        manager.NumberPressed(number);
    }
}
