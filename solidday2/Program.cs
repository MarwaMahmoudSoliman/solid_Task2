//using static SqlFileReaderManager;

using Castle.Windsor.Configuration.Interpreters;
using Castle.Windsor;
using Unity;
using Unity.Lifetime;

namespace solidday2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //ITask task = new Task();
            //IDeveloper dev = new Developer { Name = "Dev1" };
            //IAssignable teamLead = new TeamLead(task, dev);
            //IAssignable manager = new Manager(teamLead);
            //manager.AssignTask();


            var container = new WindsorContainer(new XmlInterpreter("castle.config"));

            // Resolve the Shopper object from container
            var shopper = container.Resolve<Shopper>();

            shopper.Checkout();

            Console.ReadLine();
            //var container = new UnityContainer();

            //// Register dependencies with different scopes
            //container.RegisterType<ICreditCard, VisaCard>(new TransientLifetimeManager()); // New instance every time
            //container.RegisterType<Shopper>(new ContainerControlledLifetimeManager()); // Singleton

            //// Resolve and use the Shopper object
            //var shopper = container.Resolve<Shopper>();
            //shopper.Checkout();

            //Console.ReadLine();

        }
    }
}
