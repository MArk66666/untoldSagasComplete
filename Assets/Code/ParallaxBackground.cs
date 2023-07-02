using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultiplier = 0.05f;
    private Vector2 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        Vector2 mousePositionNormalized = (mousePosition / screenSize) * 2f - Vector2.one;

        transform.position = new Vector3(_initialPosition.x + mousePositionNormalized.x * parallaxEffectMultiplier,
                                         _initialPosition.y + mousePositionNormalized.y * parallaxEffectMultiplier,
                                         transform.position.z);
    }
}