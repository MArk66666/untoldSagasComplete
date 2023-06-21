using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelationshipInteractionSetup : SecondaryEvent, IAnimatable
{
    [SerializeField] private Image characterIcon;
    [SerializeField] private Text characterNameField;
    [SerializeField] private Slider relationshipSlider;
    [SerializeField] private Text sliderValueField;

    private float _initialRelationshipValue = 0;
    private float _targetRelationshipValue = 0;
    private float _currentRelationshipValue = 0;

    private float _animationSmoothness = 5f;

    private bool _canPlayAnimation = false;

    private void Update()
    {
        sliderValueField.text = Mathf.RoundToInt(_currentRelationshipValue).ToString();

        if (_canPlayAnimation)
        {
            _currentRelationshipValue += _animationSmoothness * Time.deltaTime;

            relationshipSlider.value = _currentRelationshipValue;

            if (_currentRelationshipValue >= _targetRelationshipValue)
            {
                _currentRelationshipValue = _targetRelationshipValue;
                _canPlayAnimation = false;
            }
        }
    }

    public void ExecuteAnimation(float delay = 0f)
    {
        StartCoroutine(PlayAnimationAfterDelay(delay));
    }

    private IEnumerator PlayAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _canPlayAnimation = true;
    }

    private void SetSliderValues()
    {
        _currentRelationshipValue = _initialRelationshipValue;
        relationshipSlider.value = _currentRelationshipValue;
    }

    public void SetTargetValue(float value)
    {
        _targetRelationshipValue = value;
        SetSliderValues();
    }

    public void SetCharacter(Character character)
    {
        characterIcon.sprite = character.Icon;
        characterNameField.text = character.Name;

        _initialRelationshipValue = character.Relationship;
    }
}