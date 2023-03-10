using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartsOfSpeechManager : MonoBehaviour
{
    //從無法確定的來源讀取題目，根據題目需要，產生特定數量的格子，並實例化詞性節點

    [SerializeField] private GameObject Slot;

    private GridLayoutGroup m_gridLayoutGroup;

    private void Awake()
    {
        m_gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    public void SetupItem(List<string> partsOfSpeech, PartOfSpeechItem prefab)
    {
        for(int i = 0;i < partsOfSpeech.Count; i++)
        {
            GameObject slot = Instantiate(Slot, transform);
            Instantiate(prefab, slot.transform).Text = partsOfSpeech[i];
        }
    }
}
