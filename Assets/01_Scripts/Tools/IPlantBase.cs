using Jun;
using Jun.Ground.Crops;
public interface IPlantBase
{
    public CropPoint cropPoint { get; set; }
    public void SetCropPoint(CropPoint targetCropPoint);
}