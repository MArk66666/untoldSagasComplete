using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequiredCharacteristicSetup : MonoBehaviour
{
    [SerializeField] private Image characteristicIcon;

    private Material _characteristicMaterial;
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    private IEnumerator ShowIcon()
    {
        float initialDissolveLevel = 1f;
        float dissolveTime = 0f;
        float dissolveSpeed = .5f;

        while (dissolveTime < 1f)
        {
            dissolveTime += Time.deltaTime * dissolveSpeed;
            float currentDissolveLevel = Mathf.Lerp(initialDissolveLevel, 0, dissolveTime);
            _characteristicMaterial.SetFloat("_Level", currentDissolveLevel);
            yield return null;
        }

        _characteristicMaterial.SetFloat("_Level", 0);
        _characteristicMaterial = null;
    }

    public void VisualizeIcon()
    {
        StartCoroutine(ShowIcon());
        _source.Play();
    }

    public void SetActive(bool value)
    {
        characteristicIcon.gameObject.SetActive(value);
    }

    public void SetupCharacteristic(Characteristic characteristic)
    {
        _characteristicMaterial = GetComponent<Image>().material;
        characteristicIcon.sprite = characteristic.Icon;
    }
}
