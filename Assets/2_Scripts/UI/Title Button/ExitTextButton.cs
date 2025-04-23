using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTextButton : TextButtonUI
{
    public override void ButtonClicked()
    {
        Application.Quit();
    }
}
