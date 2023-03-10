using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

//[CreateAssetMenu(menuName = "Debugger", fileName = "dd")]
//[RequireComponent(typeof(Debugger))]
public class Debugger : MonoBehaviour
{
    [Header("header")]
    public bool mousePosition = false;
    public bool screenInfo = false;
    public bool cameraInfo = false;

    // Update is called once per frame
    void Update()
    {
        if (mousePosition)
        {
            Debug.Log("mouse: " + Input.mousePosition);
            Debug.Log("camera: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log("viewport: " + Camera.main.ScreenToViewportPoint(Input.mousePosition));
        }

        if(screenInfo)
        {
            Debug.Log("height: " + Screen.height + "\n" + 
                      "width: " + Screen.width + "\n" +
                      "mainWindowPosition: " + Screen.mainWindowPosition + "\n" +
                      "mainWindowDisplayInfo: " + Screen.mainWindowDisplayInfo.height + "\n");
        }

        if(cameraInfo)
        {
            Debug.Log("aspect: " + Camera.main.aspect + "\n" +
                      "depth: " + Camera.main.depth + "\n" +
                      "farClipPlane: " + Camera.main.farClipPlane + "\n" +
                      "nearClipPlane: " + Camera.main.nearClipPlane + "\n" +
                      "fieldOfView: " + Camera.main.fieldOfView + "\n" +
                      "focalLength: " + Camera.main.focalLength + "\n" +
                      "orthographicSize: " + Camera.main.orthographicSize + "\n" );
        }

        //�ثe���P�ѪR�פU�A�Y��canvas���覡 (�P�즲����Z������)
        //�Ncanvas�j�p�]����e�ѪR��
        //�Q�ά۾�����j�p�P�ѪR�װ��ת���Ҷi���Y��A��:
        //Camera.main.orthographicSize / Screen.height = 10 / 1080 = 0.0092592592
        //�Ncanvas scale �Y��0.0092592592

    }




    //���Ͱߤ@��id
    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public override string ToString()
    {
        StringBuilder info = new StringBuilder();
        RectTransform rectTransform = GetComponent<RectTransform>();

        if(rectTransform != null)
        {
            info.AppendLine("<b>RectTransform:</b>");
            info.AppendLine("<b>LocalPosition:</b>" + rectTransform.localPosition.ToString());
            info.AppendLine("<b>anchoredPosition:</b>" + rectTransform.anchoredPosition.ToString());
            info.AppendLine("<b>position:</b>" + rectTransform.position.ToString());
            info.AppendLine("<b>anchorMin:</b>" + rectTransform.anchorMin.ToString());
            info.AppendLine("<b>anchorMax:</b>" + rectTransform.anchorMax.ToString());
            info.AppendLine("<b>offsetMax:</b>" + rectTransform.offsetMax.ToString());
            info.AppendLine("<b>offsetMin:</b>" + rectTransform.offsetMin.ToString());
            info.AppendLine("<b>pivot:</b>" + rectTransform.pivot.ToString());
            info.AppendLine("<b>sizeDelta:</b>" + rectTransform.sizeDelta.ToString());
            info.AppendLine("<b>rect:</b>" + rectTransform.rect.ToString());
            info.AppendLine("-----------");
        }
        
        info.AppendLine("<b>Transform:</b>");
        info.AppendLine("<b>LocalPosition:</b>" + transform.localPosition.ToString());
        info.AppendLine("<b>position:</b>" + transform.position.ToString());
        info.AppendLine("-----------");
        info.AppendLine("<b>Input:</b>");
        info.AppendLine("<b>mousePosition:</b>" + Input.mousePosition);
        info.AppendLine("-----------");
        info.AppendLine("<b>Camera:</b>");
        info.AppendLine("<b>cameraToWorldMatrix:</b>" + Camera.main.cameraToWorldMatrix.ToString());
        info.AppendLine("<b>pixelRect:</b>" + Camera.main.pixelRect.ToString());
        info.AppendLine("<b>rect:</b>" + Camera.main.rect.ToString());
        info.AppendLine("<b>ScreenToViewportPoint:</b>" + Camera.main.ScreenToViewportPoint(Input.mousePosition));
        info.AppendLine("<b>ScreenToWorldPoint:</b>" + Camera.main.ScreenToWorldPoint(Input.mousePosition));

        return info.ToString();
    }
}
