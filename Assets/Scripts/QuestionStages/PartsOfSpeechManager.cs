using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartsOfSpeechManager : MonoBehaviour
{
    //�q�L�k�T�w���ӷ�Ū���D�ءA�ھ��D�ػݭn�A���ͯS�w�ƶq����l�A�ù�ҤƵ��ʸ`�I

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
