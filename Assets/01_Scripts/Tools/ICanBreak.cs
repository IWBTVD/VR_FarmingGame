public interface ICanBreak
{
    public int toolID { get; set; }
    abstract void DoAction(BreakableObject nearBreakable);

}