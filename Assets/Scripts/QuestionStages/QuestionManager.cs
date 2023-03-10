using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private PartsOfSpeechManager ps;
    [SerializeField] private PartOfSpeechItem item;

    private QuestionData data;

    private void Start()
    {
        Init();
        ps.SetupItem(data.PartsOfSpeech, item);
    }

    private void Init()
    {
        List<string> pos = new List<string>
        {
            "S",
            "N",
            "V",
            "NP",
            "VP",
            "ADV"
        };

        data = new QuestionData(
            "",
            pos,
            "");
    }
}
