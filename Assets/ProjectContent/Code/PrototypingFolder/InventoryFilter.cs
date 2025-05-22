using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours;
using ProjectContent.Code.ScriptableObjects;
namespace ProjectContent.Code.PrototypingFolder
{
  public enum InvFilterType
  {
    Include = 0,
    Exclude = 1
  }

  public enum InvFilterParam
  {
    Empty,
    Equals,
    MaxCount,
  }
  
  public class InventoryFilter
  {
    private Dictionary<InvFilterType, List<InvFilterParam>> _settings;
    private InvFilterType _invFilterType;
    private ItemConfig _itemConfig;
    private Inventory _inv;
    private InvFilterType _filterDefault;
    public InventoryFilter(Inventory inventory, ItemConfig item, Dictionary<InvFilterType, List<InvFilterParam>> settings, InvFilterType defaultFilterType = InvFilterType.Include) => 
      Configure(inventory, item, settings, defaultFilterType);

    public void Configure(Inventory inventory, ItemConfig item, Dictionary<InvFilterType, List<InvFilterParam>> settings, InvFilterType defaultFilterType = InvFilterType.Include)
    {
      _inv = inventory;
      _itemConfig = item;
      _settings = settings;
      _filterDefault = defaultFilterType;
      
      SetSettingsDefaults();
    }

    public List<int> Filter(int resultCount = -1)
    {
      if (_inv is null) throw new NullReferenceException("Inventory is null");
      List<int> result = new List<int>();
      for (int i = 0; i < _inv.slots.Length; i++)
      {
        Slot s = _inv.slots[i];
        if (Empty(s) || Equals(s) && MaxCount(s))
        {
          result.Add(i);         
        }
      }
      return result;
    }

    private bool MaxCount(Slot slot)
    {
      if (_settings[InvFilterType.Include].Contains(InvFilterParam.MaxCount))
        return slot.SlotData.Count <= slot.SlotData.Item.MaxCount;
      else if (_settings[InvFilterType.Exclude].Contains(InvFilterParam.MaxCount))
        return slot.SlotData.Count != slot.SlotData.Item.MaxCount;
      else
        throw new Exception("InvFilterType not supported");
    }

    private bool Equals(Slot slot)
    {
      if (_settings[InvFilterType.Include].Contains(InvFilterParam.Equals))
        return slot.SlotData.Item == _itemConfig;
      else if (_settings[InvFilterType.Exclude].Contains(InvFilterParam.Equals))
        return slot.SlotData.Item != _itemConfig;
      else throw new Exception("InvFilterType not supported");
    }

    private bool Empty(Slot slot)
    {
      if (_settings[InvFilterType.Include].Contains(InvFilterParam.Empty))
        return slot.SlotData.Item is null;
      else if (_settings[InvFilterType.Exclude].Contains(InvFilterParam.Empty))
        return slot.SlotData.Item is not null;
      else throw new Exception("InvFilterType not supported");
    }

    private void SetSettingsDefaults()
    {
      if (!_settings.ContainsKey(InvFilterType.Include)) _settings[InvFilterType.Include] = new List<InvFilterParam>();
      if (!_settings.ContainsKey(InvFilterType.Exclude)) _settings[InvFilterType.Exclude] = new List<InvFilterParam>();

      HashSet<InvFilterParam> filterParams = new HashSet<InvFilterParam>()
      {
        InvFilterParam.Empty,
        InvFilterParam.Equals,
        InvFilterParam.MaxCount,
      };
      
      foreach (InvFilterParam filterParam in filterParams)
      {
        if (_settings[InvFilterType.Include].Contains(filterParam))
          continue;
        else if (_settings[InvFilterType.Exclude].Contains(filterParam))
          continue;
        _settings[_filterDefault].Add(filterParam);
      }
    }
  }
}