using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenLinkButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string _link;

    [DllImport("__Internal")]
    private static extern void OpenNewTab(string url);

    public void OnPointerClick(PointerEventData eventData)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
             OpenNewTab(_link);
#endif
    }
}
