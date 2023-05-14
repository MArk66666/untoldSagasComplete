using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTransformHandler : MonoBehaviour
{
    [SerializeField] private GameObject frontSide;

    private Vector2 _initialScale = Vector2.zero;
    private Vector2 _targetScale = Vector2.zero;

    private Vector2 _highlightedScale = new Vector2(1.07f, 1.07f);

    public bool Used { get; set; }

    private DecisionCard _card;

    private void Awake()
    {
        _initialScale = transform.localScale;
        _card = GetComponent<DecisionCard>();
    }

    private void Update()
    {
        ChangeScale();
    }

    public void ChangeScale()
    {
        if (IsHighlighted())
        {
            _targetScale = _highlightedScale;
        }
        else
        {
            _targetScale = _initialScale;
        }

        Vector2 currentScale = transform.localScale;
        if (currentScale != _targetScale)
            transform.localScale = Vector2.Lerp(transform.localScale, _targetScale, 10f * Time.deltaTime);
    }

    public bool IsHighlighted()
    {
        Vector2 cursorPosition = Input.mousePosition;
        return RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), cursorPosition);
    }

    public IEnumerator RotateCard(Vector3 targetRotation, bool selected = false)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion newRotation = Quaternion.Euler(targetRotation);

        float elapsedTime = 0f;
        float rotationDuration = .45f;

        if (Quaternion.Angle(transform.rotation, newRotation) <= 0.1f)
        {
            yield break;
        }

        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;

            float time = elapsedTime / rotationDuration;
            time = Mathf.Clamp01(time);

            transform.rotation = Quaternion.Slerp(startRotation, newRotation, time);
            yield return null;
        }

        transform.rotation = newRotation;

        if (selected)
        {
            Used = true;
            frontSide.SetActive(false);
            yield return RotateCard(new Vector3(0f, 0f, 0f), true);
        }

        else if (Used && !selected)
        {
            _card.PlayEvent();
        }
    }
}
