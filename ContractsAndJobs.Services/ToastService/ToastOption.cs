namespace ContractsAndJobs.Services.ToastService;

public class ToastOption
{
    public string? Title { get; set; }
    public string? Content { get; set; }

    public int Timeout { get; set; } = 3000;

    public string? PositionX { get; private set; } = "Right";
    public string? PositionY { get; private set; } = "Bottom";

    private ToastPositions? position = ToastPositions.BottomRight;
    public ToastPositions? ToastPosition
    {
        get { return this.position; }
        set
        {
            this.position = value;
            switch (this.position)
            {
                case ToastPositions.TopLeft:
                    {
                        this.PositionX = "Left";
                        this.PositionY = "Top";
                        break;
                    }
                case ToastPositions.TopMiddle:
                    {
                        this.PositionX = "Center";
                        this.PositionY = "Top";
                        break;
                    }
                case ToastPositions.TopRight:
                    {
                        this.PositionX = "Right";
                        this.PositionY = "Top";
                        break;
                    }
                case ToastPositions.BottomLeft:
                    {
                        this.PositionX = "Left";
                        this.PositionY = "Bottom";
                        break;
                    }
                case ToastPositions.BottomMiddle:
                    {
                        this.PositionX = "Center";
                        this.PositionY = "Bottom";
                        break;
                    }
                case ToastPositions.BottomRight:
                    {
                        this.PositionX = "Right";
                        this.PositionY = "Bottom";
                        break;
                    }
                default:
                    this.PositionX = "Right";
                    this.PositionY = "Bottom";
                    break;
            }
        }
    }
}
