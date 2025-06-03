using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.Architecture
{
  public class EntryPoint : MonoBehaviour
  {
    [SerializeField] private EntryPointBase _entryPoint;
    
    
    private void Awake()
    {
      Debug.LogWarning("EntryPoint Awake");
      _entryPoint.AwakeEnter();
    }
    
    private void Start()
    {
      Debug.LogWarning("EntryPoint Start");
      _entryPoint.StartEnter();
    }
    
  }
  
}