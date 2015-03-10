using System;

namespace AppDomain
{
    class Program1
    {
        static void Main()
        {
            var scanningTask = new ScanningTask();
            scanningTask.RunTask();
        }
    }

    public class ScanningTask : MarshalByRefObject
    {
        public void RunTask()
        {
            var domain = System.AppDomain.CreateDomain("LoadDomain");

            var loader = (Loader)domain.CreateInstanceFromAndUnwrap(
                typeof(Loader).Assembly.Location,
                typeof(Loader).FullName);

            loader.OnLoad += loader_OnLoad;
            loader.Load();

            System.AppDomain.Unload(domain);
        }

        void loader_OnLoad(object sender, EventArgs e)
        {
            Console.Write("load event called");
        }
    }

    public class Loader : MarshalByRefObject
    {
        public void Load()
        {
            if (OnLoad != null)
                OnLoad(this, EventArgs.Empty);
        }

        public event EventHandler OnLoad;
    }


}
