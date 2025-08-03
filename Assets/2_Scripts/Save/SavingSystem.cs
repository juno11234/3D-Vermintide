using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    public void Save(string saveFile)
    {
        print("Save" +  saveFile);
    }

    public void Load(string saveFile)
    {
        print("Load" +  saveFile);
    }

    private string GetPathFromSaveFile(string saveFile)
    {
        return "??";
    }
}