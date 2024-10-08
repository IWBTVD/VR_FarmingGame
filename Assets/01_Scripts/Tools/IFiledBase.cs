public interface IFiledBase
{
    public CultivationField lastField { get; set; }
    public void SetField(CultivationField targetField);
}