using System.Collections.Generic;
using ProjectContent.Code.PrototypingFolder.UI;
using ProjectContent.Code.ToolsAndExtentionsScripts.DictionarySerializer;
using ProjectContent.Code.ToolsAndExtentionsScripts.TypeSerializer;
using UnityEngine;

namespace ProjectContent.Code.PrototypingFolder.Good
{
  public class WindowsController : MonoBehaviour
  {
    public SerializableDictionary<TypeReference<UIWindow>, UIWindow> Windows;
    public HashSet<UIWindow> OpenedWindows { get; } = new();

    private void Start()
    {
      foreach (var window in Windows.Values) window.Initialize();
    }

    public void OpenWindow<T>() where T : UIWindow
    {
      UIWindow uiWindow = null;

      uiWindow = GetWindow<T>();
      
      if (!uiWindow.IsOpened)
      {
        uiWindow.Open();
        OpenedWindows.Add(uiWindow);
      }
    }

    public T GetWindow<T>() where T : UIWindow
    {
      foreach (TypeReference<UIWindow> windowsKey in Windows.Keys)
      {
        if (windowsKey.Type == typeof(T))
        {
          UIWindow window = Windows[windowsKey];
          return window as T;
        }
      }

      throw new KeyNotFoundException();
    }

    public void CloseWindow(UIWindow window)
    {
      if (OpenedWindows.TryGetValue(window, out var uiWindow))
      {
        uiWindow.Close();
        OpenedWindows.Remove(uiWindow);
      }
    }
  }
}