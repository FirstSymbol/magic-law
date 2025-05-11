using UnityEngine;
using Zenject;

namespace ProjectContent.Code.PrototypingFolder.UI
{
  public abstract class UIWindow : MonoBehaviour
  {
    protected WindowsController _windowsController;
    
    [Inject]
    private void Inject(GameInput gameInput, WindowsController windowsController)
    {
      _windowsController = windowsController;
    }

    public abstract void Initialize();
    public bool isOpened => gameObject.activeSelf;
    public void Open()
    {
      gameObject.SetActive(true);
      
    }

    public void Close()
    {
      gameObject.SetActive(false);
    }

    public abstract void Toggle();
  }
}