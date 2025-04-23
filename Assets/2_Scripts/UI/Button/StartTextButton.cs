using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTextButton : TextButtonUI
{
    public override void ButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
}
