using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class mg_pressInOrderButton : MonoBehaviour, IPointerClickHandler
{
    public int number;
    mg_pressInOrder manager = null;
    Button b;

    private void Start ()
    {
        b = transform.GetComponent<Button>();
    }

    public void SetNumber(mg_pressInOrder _manager, int _number)
    {
        number = _number;
        manager = _manager;

        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.SetText(_number.ToString());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        b.enabled = true;
        int _index = int.Parse(b.gameObject.name);
        manager.NumberPressed(number, _index);
    }
}
