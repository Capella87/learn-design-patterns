// Singleton pattern

// To run this code, .NET 6 or above is required.

namespace Singleton
{
    public class Singleton
    {
        private static Singleton _singleton;
        
        private Singleton()
        {
            Console.WriteLine("The singleton object is constructed.");
        }

        public static Singleton GetInstance()
        {
            if (_singleton == null)
                _singleton = new Singleton();
            else
                Console.WriteLine("The singleton object is already generated.");

            return _singleton;
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            var s1 = Singleton.GetInstance();
            var s2 = Singleton.GetInstance();
        }
    }
}