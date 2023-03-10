using UnityEngine;
using UnityEngine.EventSystems;

public class NodeButton : MonoBehaviour, IPointerExitHandler
{
    //�ץ�SyntaxTreeNode prefab���A�ƹ����Xbutton�ɤ��|���������D
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!transform.parent.gameObject.Equals(eventData.pointerCurrentRaycast.gameObject))
            gameObject.SetActive(false);
    }
}
