using ContractsAndJobs.Data;
using ContractsAndJobs.Models;
using System.Runtime.CompilerServices;

namespace ContractsAndJobs.ViewModels
{
    public interface ILazyViewModel
    {
        AsyncLazy<List<Contact>> Contacts { get; }
    }

    public class LazyViewModel : ILazyViewModel
    {
        private readonly IContractsAndJobsDataService contractsAndJobsDataService;

        public LazyViewModel(IContractsAndJobsDataService contractsAndJobsDataService)
        {
            this.contractsAndJobsDataService = contractsAndJobsDataService;
        }

        private AsyncLazy<List<Contact>> contacts => new(() => GetAllContacts());

        public AsyncLazy<List<Contact>> Contacts => contacts;

        private async Task<List<Contact>> GetAllContacts()
        {
            return (await this.contractsAndJobsDataService.GetAllContactsAsync()).ToList();
        }
    }

    /// <summary>
    /// Provides support for asynchronous lazy initialization. This type is fully threadsafe.
    /// </summary>
    /// <typeparam name="T">The type of object that is being asynchronously initialized.</typeparam>
    public sealed class AsyncLazy<T>
    {
        /// <summary>
        /// The underlying lazy task.
        /// </summary>
        private readonly Lazy<Task<T>> instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncLazy&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="factory">The delegate that is invoked on a background thread to produce the value when it is needed.</param>
        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncLazy&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="factory">The asynchronous delegate that is invoked on a background thread to produce the value when it is needed.</param>
        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        /// <summary>
        /// Asynchronous infrastructure support. This method permits instances of <see cref="AsyncLazy&lt;T&gt;"/> to be await'ed.
        /// </summary>
        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }

        /// <summary>
        /// Starts the asynchronous initialization, if it has not already started.
        /// </summary>
        public void Start()
        {
            var unused = instance.Value;
        }
    }
}
