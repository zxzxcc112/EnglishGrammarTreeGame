using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionData
{
    private string _sentence;
    private string _solution;
    private List<string> _partsOfSpeech;

    public string Sentence { get { return _sentence; } }
    public string Solution { get { return _solution; } }
    public List<string> PartsOfSpeech { get { return _partsOfSpeech; } }

    public QuestionData(string sentence, List<string> partsOfSpeech, string solution)
    {
        _sentence = sentence;
        _partsOfSpeech = partsOfSpeech;
        _solution = solution;
    }

    public int GetPartsOfSpeechCount()
    {
        return _partsOfSpeech.Count;
    }
}
