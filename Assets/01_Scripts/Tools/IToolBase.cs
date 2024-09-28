public interface IToolBase
{
    public int toolID { get; set; }

    public abstract void DoAction(CultivationField targetField);
}