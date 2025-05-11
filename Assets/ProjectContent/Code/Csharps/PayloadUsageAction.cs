using System;

namespace ProjectContent.Code.Csharps
{
  [Serializable]
  public abstract class PayloadUsageAction<T> : UsageAction
  {
    public T payload;

    public PayloadUsageAction(T payload)
    {
      this.payload = payload;
    }
  }
}