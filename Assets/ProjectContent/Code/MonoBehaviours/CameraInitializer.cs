using System;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.PrototypingFolder
{
  public class CameraInitializer : MonoBehaviour
  {
    public CinemachineCamera CinemachineCamera;
    private CreatureFabric _creatureFabric;

    [Inject]
    private void Inject(CreatureFabric creatureFabric)
    {
      _creatureFabric = creatureFabric;
    }

    private void Start()
    {
      _creatureFabric.OnPlayerCreated += OnPlayerCreated;
      if (_creatureFabric.Player != null) 
        OnPlayerCreated();
    }

    private void OnPlayerCreated()
    {
      if (CinemachineCamera != null)
      {
        CinemachineCamera.Follow = _creatureFabric.Player.transform;
      }
    }

    private void OnDestroy()
    {
      _creatureFabric.OnPlayerCreated -= OnPlayerCreated;
    }
  }
}