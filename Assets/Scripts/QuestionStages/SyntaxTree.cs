using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SyntaxTree : MonoBehaviour, IDropHandler
{
    //當接收到PartOfSpeechItem放置時，在放置的位置產生樹的節點

    [SerializeField] private SyntaxTreeNode syntaxTreeNodePrefab;
    [SerializeField] private GameObject SyntaxTreeLinePrefab;

    private PartOfSpeechItem m_draggedItem;
    private SyntaxTreeNode node;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out m_draggedItem) == false)
            return;

        CreateNode();
        CreateLine();
    }

    private void CreateNode()
    {
        node = Instantiate(syntaxTreeNodePrefab, m_draggedItem.DetectedParent);

        node.Initialize(m_draggedItem.transform.position, m_draggedItem.GetComponentInChildren<TextMeshProUGUI>().text);
    }

    private void CreateLine()
    {
        if (m_draggedItem.DetectedParent == PartOfSpeechItem.DefaultDetectedParent) return;

        LineController line = Instantiate(SyntaxTreeLinePrefab, node.transform).GetComponent<LineController>();
        line.Initialize(line.transform.parent, line.transform.parent.parent);
    }


    //-----------------------------------------
    //use to test    
    //-----------------------------------------
    public void GetTreeString()
    {
        SyntaxTreeNode[] nodes = transform.GetComponentsInChildren<SyntaxTreeNode>();
        foreach (SyntaxTreeNode node in nodes)
        {
            Debug.Log(node.Text);
        }

        if(nodes.Length > 0)
        {
            string str = SyntaxTreeConverter.TreeToString(nodes[0]);
            Debug.Log(str);
            MakeTree(str);
        }
    }

    public void CheckNodes(Transform root)
    {
        List<SyntaxTreeNode> nodes = new List<SyntaxTreeNode>();

        foreach(Transform child in root)
        {
            if (child.TryGetComponent<SyntaxTreeNode>(out SyntaxTreeNode node))
                nodes.Add(node);
        }

        nodes.Sort(comp);
        
        foreach(SyntaxTreeNode node in nodes)
        {
            node.transform.SetAsLastSibling();
            CheckNodes(node.transform);
        }
    }

    public int comp(SyntaxTreeNode node1, SyntaxTreeNode node2)
    {
        return node1.transform.position.x.CompareTo(node2.transform.position.x);
    }

    public void MakeTree(string str)
    {
        str = "[1[2][3]]";
        SyntaxTreeConverter.StringToTree(str, transform, syntaxTreeNodePrefab);
    }
}
