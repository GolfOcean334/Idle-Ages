using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapZoom : MonoBehaviour, IScrollHandler
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public float zoomSpeed = 0.1f;
    public float minZoom = 0.5f;
    public float maxZoom = 2f;

    private Vector2 originalSize;
    private bool isTouching = false;

    void Start()
    {
        if (content == null)
        {
            content = scrollRect.content;
        }
        originalSize = content.sizeDelta;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            isTouching = true;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Zoom(-deltaMagnitudeDiff);
        }
        else
        {
            isTouching = false;
        }
    }

    public void OnScroll(PointerEventData data)
    {
        if (!isTouching)
        {
            Zoom(data.scrollDelta.y);
        }
    }

    void Zoom(float increment)
    {
        float scaleFactor = 1 + increment * zoomSpeed;
        float newScaleX = Mathf.Clamp(content.localScale.x * scaleFactor, minZoom, maxZoom);
        float newScaleY = Mathf.Clamp(content.localScale.y * scaleFactor, minZoom, maxZoom);

        content.localScale = new Vector3(newScaleX, newScaleY, 1);
        AdjustScrollPosition(increment);
    }

    void AdjustScrollPosition(float increment)
    {
        Vector2 newPivot = content.pivot;
        if (increment > 0)
        {
            newPivot = new Vector2(0.5f, 0.5f);
        }
        content.pivot = newPivot;

        Vector2 newAnchoredPosition = content.anchoredPosition;
        if (increment > 0)
        {
            newAnchoredPosition = new Vector2(0, 0);
        }
        content.anchoredPosition = newAnchoredPosition;
    }
}
