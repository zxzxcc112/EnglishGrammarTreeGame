using UnityEngine;
using UnityEngine.EventSystems;

public class NodeButton : MonoBehaviour, IPointerExitHandler
{
    //修正SyntaxTreeNode prefab中，滑鼠移出button時不會消失的問題
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!transform.parent.gameObject.Equals(eventData.pointerCurrentRaycast.gameObject))
            gameObject.SetActive(false);
    }
}
