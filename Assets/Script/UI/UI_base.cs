using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_base : MonoBehaviour
{
    protected Dictionary< Type, UnityEngine.Object[]> _dictionary = new Dictionary< Type, UnityEngine.Object[]>();
    protected void Bind<T>(Type EnumNames) where T : UnityEngine.Object
    {
        string[] Names = Enum.GetNames(EnumNames);
        UnityEngine.Object[] components = new UnityEngine.Object[Names.Length];
        for (int i = 0; i < Names.Length; i++)
        {
            components[i] = Utils.FindChild<T>(gameObject, Names[i], true);
        }
        _dictionary.Add(typeof(T), components);
    }
    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] components;
        if(_dictionary.TryGetValue(typeof(T), out components) == false)
        {
            return null;
        }
        return components[index] as T;
    }
    protected void AddUIEvent(GameObject go, Constant.UIEvent uIEvent, Action<PointerEventData> actionCallback)
    {
        go.AddComponent<UIEventHandler>();
        if (uIEvent == Constant.UIEvent.Click)
        {
            UIEventHandler EventHandler = go.GetComponent<UIEventHandler>();
            EventHandler.ClickHandlerBucket -= actionCallback;
            EventHandler.ClickHandlerBucket += actionCallback;
        }
    }
    void Start()
    {
    }
}
