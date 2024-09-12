using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    public void PlaySfx()
    {
        SoundManager.instance.PlayOneShootSound(SoundReference.instance.ui_Click, new Vector2(0, 0));
    }
    public void PlaySfxPlay()
    {
        SoundManager.instance.PlayOneShootSound(SoundReference.instance.sfx_DoorClose, new Vector2(0, 0));

    }

}
