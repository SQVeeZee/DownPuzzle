public class FixedElement : BaseElement
{
    protected override IElementConfigs ElementConfigs { get; }

    protected override void InIt()
    {
        
    }

    public override EElementType ElementType => EElementType.FIXED;
}
