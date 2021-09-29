using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SteamVenture/Dialog Object")]
public class DialogObjects : ScriptableObject
{
    public Sprite image;

    public string[] dialog;
}
