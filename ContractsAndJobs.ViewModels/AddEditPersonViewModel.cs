using ContractsAndJobs.Models;

namespace ContractsAndJobs.ViewModels
{
    public interface IAddEditPersonViewModel
    {
        public Person? Person { get; set; }

        public List<string>? Titles { get; set; }

        public List<string>? Countries { get; set; }

        public List<string>? Regions { get; set; }

    }
}
