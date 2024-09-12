using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsPaperAnim : MonoBehaviour
{
    GameManager _GameManager => GameManager.instance;
    public void Kill()
    {
        _GameManager.DestoyNewsPaper();
    }
}
