using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private GameObject interactNotification;
    [SerializeField]
    private Canvas canvasHUD;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        HideInteractNotification();
    }
    public Vector2? WorldToUILocalPosition(Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        if (screenPos.z < 0)
        {
            return null;
        }

        if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
        {
            return null;
        }

        RectTransform canvasRect = canvasHUD.GetComponent<RectTransform>();

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            canvasHUD.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
            out localPoint);

        return localPoint;
    }

    public void ShowInteractNotification()
    {
        interactNotification.SetActive(true);
    }

    public void HideInteractNotification()
    {
        interactNotification.SetActive(false);
    }
}
