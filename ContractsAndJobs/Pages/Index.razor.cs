using ContractsAndJobs.Models;
using ContractsAndJobs.ViewModels;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;

namespace ContractsAndJobs.Pages
{
    public partial class Index
    {
        [Inject]
        private IIndexViewModel? ViewModel { get; set; }

        private void SelectedPersonChangeHandler(ChangeEventArgs<string, Contact> args)
        {
            this.ViewModel!.Contact = args.ItemData;
        }
    }

    public class StubIndexViewModel : IIndexViewModel
    {
        private readonly List<Contact> contacts;

        public StubIndexViewModel()
        {
            this.contacts = new List<Contact>
            {
                new ()
                {
                    Id = 1, FirstName = "Glen", LastName = "Wilkin", Agency = "Lorien", 
                    Interactions = new List<Interaction>
                    {
                        new ()
                        {
                            Id = 1, Date = new DateTime(2022, 3, 25), 
                            Role = new Role
                            {
                                Id = 1, Company = "AIG", DayRate = 495, InsideIr35 = false, Location = "Reigate", Type = RoleTypes.Contract, WorkType = WorkTypes.Hybrid
                            }
                        },
                        new ()
                        {
                            Id = 2, Date = new DateTime(2022, 4, 26),
                            Role = new Role
                            {
                                Id = 2, Company = "SYPTE", DayRate = 500, InsideIr35 = false, Location = "Sheffield", Type = RoleTypes.Contract, WorkType = WorkTypes.InOffice
                            }
                        },
                        new ()
                        {
                            Id = 3, Date = new DateTime(2022, 5, 27),
                            Role = new Role
                            {
                                Id = 3, Company = "GMSL", DayRate = 550, InsideIr35 = true, Location = "Cambridge", Type = RoleTypes.Contract, WorkType = WorkTypes.WFH
                            }
                        },
                        new ()
                        {
                            Id = 4, Date = new DateTime(2022, 6, 28),
                            Role = new Role
                            {
                                Id = 4, Company = "Bank of America", DayRate = 550, InsideIr35 = true, Location = "London", Type = RoleTypes.Contract, WorkType = WorkTypes.WFH
                            }
                        }
                    }
                },
                new (){Id = 2, FirstName = "Katie", LastName = "Wilkin", Agency = "Gravitas"},
                new (){Id = 3, FirstName = "Adam", LastName = "Wilkin", Agency = "Lorien"},
                new (){Id = 4, FirstName = "Jana", LastName = "Statute", Agency = "NP Group"}
            };
        }

        public List<Contact> Contacts => this.contacts;
        public string? SelectedContact { get; set; }
        public Contact Contact { get; set; }
    }
}
