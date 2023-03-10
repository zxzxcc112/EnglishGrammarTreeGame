using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyntaxTreeConverter
{
    //將SyntaxTree轉換成不同的資料的表現方式

    private static float rootAnchorX = 0.5f;
    private static float rootAnchorY = 1f;

    //將SyntaxTree轉換成string
    public static string TreeToString(SyntaxTreeNode root)
    {
        string tree = "";

        tree += "[" + root.Text;
        foreach (Transform child in root.transform)
            if(child.TryGetComponent(out SyntaxTreeNode childNode))
                tree += TreeToString(childNode);
        tree += "]";

        return tree;
    }


    //將string轉換成SyntaxTree
    public static void StringToTree(string tree, Transform place, SyntaxTreeNode prefab)
    {
        Transform parent = place;
        SyntaxTreeNode node = null;
        string content = string.Empty;

        for(int i = 0; i < tree.Length; i++)
        {
            if (tree[i] == '[')
            {
                SetContent(node, ref content);
                node = CreateNode(prefab, parent);
                parent = node.transform;
            }
            else if (tree[i] == ']')
            {
                SetContent(node, ref content);
                SetChildrenPosition(node);
                parent = node.transform.parent;
                node = parent.GetComponent<SyntaxTreeNode>();
            }
            else
            {
                content += tree[i];
            }
        }

        SyntaxTreeNode root = place.GetComponentInChildren<SyntaxTreeNode>();
        root.GetComponent<RectTransform>().anchorMax = new Vector2(rootAnchorX, rootAnchorY);
        root.GetComponent<RectTransform>().anchorMin = new Vector2(rootAnchorX, rootAnchorY);
        root.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, NodeViewer.Step);
    }

    private static SyntaxTreeNode CreateNode(SyntaxTreeNode prefab, Transform parent)
    {
        return Object.Instantiate(prefab, parent);
    }

    private static void SetContent(SyntaxTreeNode node, ref string text)
    {
        if (text == string.Empty)
            return;

        node.Text = text;
        text = string.Empty;
    }

    private static void SetChildrenPosition(SyntaxTreeNode node)
    {
        float newSpace = 0;
        List<SyntaxTreeNode> children = new List<SyntaxTreeNode>();

        foreach (Transform child in node.transform)
        {
            if (child.TryGetComponent(out SyntaxTreeNode _n))
            {
                newSpace += _n.viewer.SpaceSize;
                children.Add(_n);
            }
        }

        if (newSpace > 0)
            node.viewer.SpaceSize = newSpace;

        float lastChildPos = -(node.viewer.SpaceSize / 2);

        foreach(SyntaxTreeNode chNode in children)
        {
            chNode.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(lastChildPos + chNode.viewer.SpaceSize / 2, NodeViewer.Step);
            lastChildPos += chNode.viewer.SpaceSize;
        }
    }
}
