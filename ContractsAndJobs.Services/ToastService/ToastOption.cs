namespace ContractsAndJobs.Services.ToastService
{
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
            get { return position; }
            set
            {
                position = value;
                switch (position)
                {
                    case ToastPositions.TopLeft:
                        {
                            PositionX = "Left";
                            PositionY = "Top";
                            break;
                        }
                    case ToastPositions.TopMiddle:
                        {
                            PositionX = "Center";
                            PositionY = "Top";
                            break;
                        }
                    case ToastPositions.TopRight:
                        {
                            PositionX = "Right";
                            PositionY = "Top";
                            break;
                        }
                    case ToastPositions.BottomLeft:
                        {
                            PositionX = "Left";
                            PositionY = "Bottom";
                            break;
                        }
                    case ToastPositions.BottomMiddle:
                        {
                            PositionX = "Center";
                            PositionY = "Bottom";
                            break;
                        }
                    case ToastPositions.BottomRight:
                        {
                            PositionX = "Right";
                            PositionY = "Bottom";
                            break;
                        }
                    default:
                        PositionX = "Right";
                        PositionY = "Bottom";
                        break;
                }
            }
        }
    }
}
