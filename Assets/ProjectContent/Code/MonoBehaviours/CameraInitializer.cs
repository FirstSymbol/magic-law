using ProjectContent.Code.Csharps;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours
{
  public class CameraInitializer : MonoBehaviour
  {
    public CinemachineCamera CinemachineCamera;
    private CreatureFabric _creatureFabric;

    private void Start()
    {
      _creatureFabric.OnPlayerCreated += OnPlayerCreated;
      if (_creatureFabric.Player != null)
        OnPlayerCreated();
    }

    private void OnDestroy()
    {
      _creatureFabric.OnPlayerCreated -= OnPlayerCreated;
    }

    [Inject]
    private void Inject(CreatureFabric creatureFabric)
    {
      _creatureFabric = creatureFabric;
    }

    private void OnPlayerCreated()
    {
      if (CinemachineCamera != null) CinemachineCamera.Follow = _creatureFabric.Player.transform;
    }
  }
}