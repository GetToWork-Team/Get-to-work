using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoveredSfx : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlayOneShootSound(SoundReference.instance.ui_Highlight, new Vector2(0, 0));
    }
}
