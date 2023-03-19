using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class mg_memorizeNumbersButton : MonoBehaviour, IPointerClickHandler
{
    public int number;
    mg_memorizeNumbers manager = null;
    Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetNumber(mg_memorizeNumbers _manager, int _number)
    {
        number = _number;
        manager = _manager;

        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.SetText(_number.ToString());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button.interactable == false) return;

        manager.NumberPressed(number);
    }

    public void SetInteractuable(bool _v)
    {
        button.interactable = _v;
    }
}