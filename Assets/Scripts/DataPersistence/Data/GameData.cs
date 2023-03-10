using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //GameSettings
    public bool isFullScreen;
    public int screenWidth;
    public int screenHeight;


    //�s�C���}�l�ɨS���ɮץi�H���J�ɡA�n���@�ӹw�]��
    public GameData()
    {
        isFullScreen = true;
        screenWidth = 0;
        screenHeight = 0;
    }
}
