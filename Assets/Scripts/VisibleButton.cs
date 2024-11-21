using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VisibleButton : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] private Sprite visible;
    [SerializeField] private Sprite nonVisible;

    [Header("Image")]
    [SerializeField] private Image icon;

    public Func<bool> SetVisibility { get; set; }

    private bool isVisible = true;

    public void VisibleButtonClick()
    {
        isVisible = SetVisibility();

        icon.sprite = isVisible ? visible : nonVisible;
    }
}
