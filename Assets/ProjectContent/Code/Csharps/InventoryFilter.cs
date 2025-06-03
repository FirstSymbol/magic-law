using System;
using System.Collections.Generic;
using ProjectContent.Code.MonoBehaviours;
using ProjectContent.Code.ScriptableObjects;

namespace ProjectContent.Code.Csharps
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
    MaxCount
  }

  /// <summary>
  ///   Класс для фильтрации слотов в инвентаре
  /// </summary>
  public class InventoryFilter
  {
    private InvFilterType _filterDefault;
    private Inventory _inv;
    private InvFilterType _invFilterType;
    private ItemConfig _itemConfig;
    private Dictionary<InvFilterType, List<InvFilterParam>> _settings;

    public InventoryFilter(Inventory inventory, ItemConfig item,
      Dictionary<InvFilterType, List<InvFilterParam>> settings, InvFilterType defaultFilterType = InvFilterType.Include)
    {
      Configure(inventory, item, settings, defaultFilterType);
    }

    public void Configure(Inventory inventory, ItemConfig item,
      Dictionary<InvFilterType, List<InvFilterParam>> settings, InvFilterType defaultFilterType = InvFilterType.Include)
    {
      _inv = inventory;
      _itemConfig = item;
      _settings = settings;
      _filterDefault = defaultFilterType;

      SetSettingsDefaults();
    }

    /// <summary>
    ///   Согласно введенным настройкам ищет все индексы в инвентаре, которые походят по нужному критерию.
    /// </summary>
    /// <param name="resultCount"></param>
    /// <returns>Список индексов соответствующих настройкам</returns>
    /// <exception cref="NullReferenceException"></exception>
    public List<int> Filter(int resultCount = -1)
    {
      if (_inv is null) throw new NullReferenceException("Inventory is null");
      var result = new List<int>();
      for (var i = 0; i < _inv.Slots.Length; i++)
      {
        var s = _inv.Slots[i];
        if (Empty(s) || (Equals(s) && MaxCount(s))) result.Add(i);
      }

      return result;
    }

    /// <summary>
    ///   Проверяет слот на содержание максимального количества предмета из возможного для конфига этого предмета.
    /// </summary>
    /// <param name="slot"></param>
    /// <returns>True or False</returns>
    /// <exception cref="Exception"></exception>
    private bool MaxCount(Slot slot)
    {
      if (_settings[InvFilterType.Include].Contains(InvFilterParam.MaxCount))
        return slot.SlotData.Count <= slot.SlotData.Item.MaxCount;
      if (_settings[InvFilterType.Exclude].Contains(InvFilterParam.MaxCount))
        return slot.SlotData.Count != slot.SlotData.Item.MaxCount;
      throw new Exception("InvFilterType not supported");
    }

    /// <summary>
    ///   Проверяет слот на то, содержит ли он эквивалентный фильтруемому предмету конфиг.
    /// </summary>
    /// <param name="slot"></param>
    /// <returns>True or False</returns>
    /// <exception cref="Exception"></exception>
    private bool Equals(Slot slot)
    {
      if (_settings[InvFilterType.Include].Contains(InvFilterParam.Equals))
        return slot.SlotData.Item == _itemConfig;
      if (_settings[InvFilterType.Exclude].Contains(InvFilterParam.Equals))
        return slot.SlotData.Item != _itemConfig;
      throw new Exception("InvFilterType not supported");
    }

    /// <summary>
    ///   Проверяет слот на то, пустой он или нет.
    /// </summary>
    /// <param name="slot"></param>
    /// <returns>True or False</returns>
    /// <exception cref="Exception"></exception>
    private bool Empty(Slot slot)
    {
      if (_settings[InvFilterType.Include].Contains(InvFilterParam.Empty))
        return slot.SlotData.Item is null;
      if (_settings[InvFilterType.Exclude].Contains(InvFilterParam.Empty))
        return slot.SlotData.Item is not null;
      throw new Exception("InvFilterType not supported");
    }

    /// <summary>
    ///   Установка всех настроек исходя из полей в переменных.
    /// </summary>
    private void SetSettingsDefaults()
    {
      if (!_settings.ContainsKey(InvFilterType.Include)) _settings[InvFilterType.Include] = new List<InvFilterParam>();
      if (!_settings.ContainsKey(InvFilterType.Exclude)) _settings[InvFilterType.Exclude] = new List<InvFilterParam>();

      var filterParams = new HashSet<InvFilterParam>
      {
        InvFilterParam.Empty,
        InvFilterParam.Equals,
        InvFilterParam.MaxCount
      };

      foreach (var filterParam in filterParams)
      {
        if (_settings[InvFilterType.Include].Contains(filterParam))
          continue;
        if (_settings[InvFilterType.Exclude].Contains(filterParam))
          continue;
        _settings[_filterDefault].Add(filterParam);
      }
    }
  }
}