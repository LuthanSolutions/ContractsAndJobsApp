namespace ContractsAndJobs.Pages;

public partial class Map
{
    private string MapUrl { get; set; } = "https://tile.openstreetmap.org/level/tileX/tileY.png";

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }
}
