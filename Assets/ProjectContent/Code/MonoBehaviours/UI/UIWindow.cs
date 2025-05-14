using UnityEngine;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.UI
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
    public bool IsOpened => gameObject.activeSelf;
    public virtual void Open()
    {
      gameObject.SetActive(true);
      
    }

    public virtual void Close()
    {
      gameObject.SetActive(false);
    }

    public abstract void Toggle();
  }
}