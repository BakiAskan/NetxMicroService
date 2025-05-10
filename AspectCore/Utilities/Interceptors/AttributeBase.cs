namespace ErpMikroservis.AspectCore;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public abstract class AttributeBase : Attribute
{
    public int Priority { get; set; }
}
