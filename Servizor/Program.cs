namespace Servizor
{
    using Topshelf;

    internal class Program
    {
        private static int Main(string[] args)
        {
            return (int)HostFactory.Run(x => x.Service<RequesterService>());
        }
    }
}