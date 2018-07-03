using System;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;

namespace couchbase
{
    class Program
    {
        static void Main(string[] args)
        {
            // create some actors
            Actor.Facade.UpdateDocument<Actor>(new Actor("0", "Mark Hamill"));
            Actor.Facade.UpdateDocument<Actor>(new Actor("1", "Harrison Ford"));
            Actor.Facade.UpdateDocument<Actor>(new Actor("2", "Carrie Fisher"));
            Actor.Facade.UpdateDocument<Actor>(new Actor("3", "Michael Cera"));
            Actor.Facade.UpdateDocument<Actor>(new Actor("4", "Mary Elizabeth Winstead"));
            Actor.Facade.UpdateDocument<Actor>(new Actor("5", "Chris Evans"));
            Actor.Facade.UpdateDocument<Actor>(new Actor("6", "Bradley Cooper"));
            Actor.Facade.UpdateDocument<Actor>(new Actor("7", "Jennifer Lawrence"));
            Actor.Facade.UpdateDocument<Actor>(new Actor("8", "Robert De Niro"));

            // create some movies
            string[] starWars = { "0", "1", "2"};
            Movie.Facade.UpdateDocument<Movie>(
                new Movie("0", "Krieg der Sterne")
                {
                    Release = new DateTime(1977, 5, 25),
                    Description = "Seit 19 Jahren regiert das Imperium mit eiserner Hand über eine ferne Galaxie...",
                    Actors = new List<string>(starWars)
                }
            );

            string[] scott = { "3", "4", "5" };
            Movie.Facade.UpdateDocument<Movie>(
                new Movie("1", "Scott Pilgrim gegen den Rest der Welt")
                {
                    Release = new DateTime(2010, 1, 1),
                    Description = "Der 22-jährige Scott Pilgrim lebt in Toronto und ist Bassist...",
                    Actors = new List<string>(scott)
                }
            );

            string[] silver = { "6", "7", "8" };
            Movie.Facade.UpdateDocument<Movie>(
                new Movie("2", "Silver Linings")
                {
                    Release = new DateTime(1977, 5, 25),
                    Description = "Pat Solitano Jr. wird aus einer Klinik entlassen, in der er aufgrund einer manisch-depressiven Störung acht Monate verbracht hatte...",
                    Actors = new List<string>(silver)
                }
            );

            string _command = String.Empty;
            
            do {
                Console.WriteLine("+--- Couchbase Access Console ---+");
                Console.WriteLine("| 1 - Get movie by ID            |");
                Console.WriteLine("| 2 - Get movie by name          |");
                Console.WriteLine("| 3 - Get list of movies by name |");
                Console.WriteLine("| 4 - Get actor by ID            |");
                Console.WriteLine("| 5 - Get actor by name          |");
                Console.WriteLine("| 6 - Get list of actors by name |");
                Console.WriteLine("| 0 - Exit                       |");
                Console.WriteLine("+--------------------------------+");

                // read command from console
                Console.Write("Enter command: ");
                _command = Console.ReadLine();

                if (_command.Equals("1"))
                {
                    Console.Write("\r\nEnter ID: ");
                    Movie.Print(Movie.ByID(Console.ReadLine()));
                }
            } while (_command != "0");
        }
    }
}
