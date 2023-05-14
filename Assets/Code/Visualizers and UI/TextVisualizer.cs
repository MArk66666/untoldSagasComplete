using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextVisualizer : MonoBehaviour
{
    private float _typeSmoothness = 0.01f;

    private string _targetText;
    private Text _textField;

    private int _currentChaptersAmount;

    public void SetTitleField(Text field)
    {
        _textField = field;
    }

    public void WriteTitle(string title)
    {
        _targetText = title;
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        while (_currentChaptersAmount <= _targetText.Length)
        {
            _textField.text = _targetText.Substring(0, _currentChaptersAmount + 1);
            _currentChaptersAmount++;

            if (_currentChaptersAmount == _targetText.Length)
            {
                _currentChaptersAmount = 0;
                yield break;
            }

            yield return new WaitForSeconds(_typeSmoothness);
        }
    }
}