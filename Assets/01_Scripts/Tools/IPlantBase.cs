using Jun;
using Jun.Ground.Crops;
public interface IPlantBase
{
    public int toolID { get; set; }
    abstract void DoAction(CropPoint cropPoint);
}