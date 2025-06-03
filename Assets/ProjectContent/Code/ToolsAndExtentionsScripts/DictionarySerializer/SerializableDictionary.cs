using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectContent.Code.ToolsAndExtentionsScripts.DictionarySerializer
{
  [Serializable]
  public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver,
    IEnumerable
  {
    [SerializeField] private List<SerializableKeyValuePair<TKey, TValue>> list = new();


    public new int Count => base.Count;

    public TValue this[TKey key]
    {
      get => base[key];
      set
      {
        base[key] = value;
        var existing = list.Find(kvp => EqualityComparer<TKey>.Default.Equals(kvp.Key, key));
        if (existing != null)
          existing.Value = value;
        else
          list.Add(new SerializableKeyValuePair<TKey, TValue> { Key = key, Value = value });
      }
    }

    public IEnumerator GetEnumerator()
    {
      throw new NotImplementedException();
    }

    public void OnBeforeSerialize()
    {
      // Список уже обновляется при изменении словаря, поэтому здесь ничего не делаем
    }

    public void OnAfterDeserialize()
    {
      base.Clear();
      foreach (var kvp in list)
        if (kvp.Key != null && !base.ContainsKey(kvp.Key))
          base[kvp.Key] = kvp.Value;
    }

    public new void Add(TKey key, TValue value)
    {
      base.Add(key, value);
      list.Add(new SerializableKeyValuePair<TKey, TValue> { Key = key, Value = value });
    }

    public new bool TryGetValue(TKey key, out TValue value)
    {
      return base.TryGetValue(key, out value);
    }

    public new void Clear()
    {
      base.Clear();
      list.Clear();
    }

    public new bool ContainsKey(TKey key)
    {
      return base.ContainsKey(key);
    }

    public new bool Remove(TKey key)
    {
      if (base.Remove(key))
      {
        list.RemoveAll(kvp => EqualityComparer<TKey>.Default.Equals(kvp.Key, key));
        return true;
      }

      return false;
    }
  }
}