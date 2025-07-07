using ProjectContent.Code.ToolsAndExtentionsScripts.RequiredNotNull;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class UIController : MonoBehaviour
  {
    [RequireNotNull] public InventoryViewLinker InventoryViewLinker;
    public WindowsController WindowsController;
    public DragItems DragItems;
  }
}