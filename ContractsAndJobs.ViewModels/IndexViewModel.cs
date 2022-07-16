using ContractsAndJobs.Models;

namespace ContractsAndJobs.ViewModels
{
    public interface IIndexViewModel
    {
        List<Contact> Contacts { get; }
        string SelectedContact { get; set; }
        Contact Contact { get; set; }
    }

    public class IndexViewModel : IIndexViewModel
    {
        public List<Contact> Contacts { get; }
        public string SelectedContact { get; set; }
        public Contact Contact { get; set; }
    }
}