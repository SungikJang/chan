using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClick : UI_base
{

    enum TextNames
    {
        ScoreText,
        MainText,
    }
    enum ButtonNames
    {
        ScoreButton,
    }
    int score = 0;
    public void OnButtonClick()
    {
        AddUIEvent(Get<Button>(0).gameObject, Constant.UIEvent.Click, (PointerEventData evt) =>
        { score++; Get<Text>((int)TextNames.ScoreText).text = $"Á¡¼ö : {score}"; }) ;
    }
    void Start()
    {
        Bind<Text>(typeof(TextNames));
        Bind<Button>(typeof(ButtonNames));
    }


    void Update()
    {
        
    }
}
