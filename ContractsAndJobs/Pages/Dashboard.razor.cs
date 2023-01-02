using ContractsAndJobs.Services;
using ContractsAndJobs.Services.ToastService;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace ContractsAndJobs.Pages
{
    public partial class Dashboard
    {
        [Inject]
        private IBrowserService? BrowserService { get; set; }

        [Inject]
        private IToastService? ToastService { get; set; }

        public class PopulationReport
        {
            public int Year { get; set; }
            public int Population { get; set; }
        };
        private List<PopulationReport> PopulationData = new List<PopulationReport> {
        new  PopulationReport { Year= 2005, Population= 10090440 },
        new  PopulationReport { Year= 2006, Population= 20264080 },
        new  PopulationReport { Year= 2007, Population= 2043418 },
        new  PopulationReport { Year= 2008, Population= 11007310 },
        new  PopulationReport { Year= 2009, Population= 21262640 },
        new  PopulationReport { Year= 2010, Population= 2151575 },
        new  PopulationReport { Year= 2011, Population= 21766710 },
        new  PopulationReport { Year= 2012, Population= 12015580 },
        new  PopulationReport { Year= 2013, Population= 22262500 },
        new  PopulationReport { Year= 2014, Population= 2250762 }
    };
        private string cardContent = @"
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer magna enim, consequat iaculis lobortis malesuada, aliquet eu lorem. Integer pretium vehicula tellus non suscipit. Aliquam erat volutpat. 
            Pellentesque nunc felis, sollicitudin quis neque sit amet, interdum semper nulla. Aliquam non ex ut leo scelerisque commodo. Nunc enim mi, elementum id nisi quis, elementum ultrices arcu. Mauris rutrum metus sem, vitae accumsan turpis commodo egestas. Nulla facilisi. Ut at convallis orci. Vivamus ac commodo dui. Sed vehicula vestibulum est at tincidunt. Cras mauris lorem, fermentum ac bibendum et, ullamcorper ac risus. Vivamus in nibh pretium, tristique felis ut, tempor purus. Aliquam vulputate efficitur imperdiet. Suspendisse ut laoreet leo. Aliquam erat volutpat. Vivamus sit amet maximus massa. 
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer magna enim, consequat iaculis lobortis malesuada, aliquet eu lorem. Integer pretium vehicula tellus non suscipit. Aliquam erat volutpat. 
            Pellentesque nunc felis, sollicitudin quis neque sit amet, interdum semper nulla. Aliquam non ex ut leo scelerisque commodo. Nunc enim mi, elementum id nisi quis, elementum ultrices arcu. Mauris rutrum metus sem, vitae accumsan turpis commodo egestas. Nulla facilisi. Ut at convallis orci. Vivamus ac commodo dui. Sed vehicula vestibulum est at tincidunt. Cras mauris lorem, fermentum ac bibendum et, ullamcorper ac risus. Vivamus in nibh pretium, tristique felis ut, tempor purus. Aliquam vulputate efficitur imperdiet. Suspendisse ut laoreet leo. Aliquam erat volutpat. Vivamus sit amet maximus massa.
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer magna enim, consequat iaculis lobortis malesuada, aliquet eu lorem. Integer pretium vehicula tellus non suscipit. Aliquam erat volutpat. 
            Pellentesque nunc felis, sollicitudin quis neque sit amet, interdum semper nulla. Aliquam non ex ut leo scelerisque commodo. Nunc enim mi, elementum id nisi quis, elementum ultrices arcu. Mauris rutrum metus sem, vitae accumsan turpis commodo egestas. Nulla facilisi. Ut at convallis orci. Vivamus ac commodo dui. Sed vehicula vestibulum est at tincidunt. Cras mauris lorem, fermentum ac bibendum et, ullamcorper ac risus. Vivamus in nibh pretium, tristique felis ut, tempor purus. Aliquam vulputate efficitur imperdiet. Suspendisse ut laoreet leo. Aliquam erat volutpat. Vivamus sit amet maximus massa. 
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer magna enim, consequat iaculis lobortis malesuada, aliquet eu lorem. Integer pretium vehicula tellus non suscipit. Aliquam erat volutpat. 
            Pellentesque nunc felis, sollicitudin quis neque sit amet, interdum semper nulla. Aliquam non ex ut leo scelerisque commodo. Nunc enim mi, elementum id nisi quis, elementum ultrices arcu. Mauris rutrum metus sem, vitae accumsan turpis commodo egestas. Nulla facilisi. Ut at convallis orci. Vivamus ac commodo dui. Sed vehicula vestibulum est at tincidunt. Cras mauris lorem, fermentum ac bibendum et, ullamcorper ac risus. Vivamus in nibh pretium, tristique felis ut, tempor purus. Aliquam vulputate efficitur imperdiet. Suspendisse ut laoreet leo. Aliquam erat volutpat. Vivamus sit amet maximus massa.";

        private static List<Task> Tasks = new ();
        private void OnButtonClicked()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Tasks.Add(Task.Run(() => DoSomethingAsync(1, 5)));
            Tasks.Add(Task.Run(() => DoSomethingAsync(2, 1)));
            Tasks.Add(Task.Run(() => DoSomethingAsync(3, 1)));
            Tasks.Add(Task.Run(() => DoSomethingAsync(4, 1)));
            Tasks.Add(Task.Run(() => DoSomethingAsync(5, 1)));
            Task.WaitAll(Tasks.ToArray());

            stopwatch.Stop();

            this.ToastService!.ShowToast(new ToastOption()
            {
                Title = "Tasks Finished",
                Content = $"Tasks finished in {stopwatch.Elapsed.TotalSeconds} seconds.",
                ToastPosition = ToastPositions.BottomRight
            });
        }

        private async Task DoSomethingAsync(int runNumber, int secondsToWait)
        {
            await Task.Delay(1000 * secondsToWait);
        }

    }
}
