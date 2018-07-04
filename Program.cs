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
            DummyCreator.CreateDummies();

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

                if (_command.Equals("2"))
                {
                    Console.Write("\r\nEnter a name: ");
                    Movie.Print(Movie.WhereName(Console.ReadLine()));
                }

                if (_command.Equals("3"))
                {
                    Console.Write("\r\nEnter a string to search for: ");
                    Movie.WhereNameLike(Console.ReadLine()).ForEach(movie => 
                    {
                        Movie.Print(movie);
                    });
                }

                if (_command.Equals("4"))
                {
                    Console.Write("\r\nEnter ID: ");
                    Actor.Print(Actor.ByID(Console.ReadLine()));
                }

                if (_command.Equals("5"))
                {
                    Console.Write("\r\nEnter a name: ");
                    Actor.Print(Actor.WhereName(Console.ReadLine()));
                }

                if (_command.Equals("6"))
                {
                    Console.Write("\r\nEnter a string to search for: ");
                    Actor.WhereNameLike(Console.ReadLine()).ForEach(actor => 
                    {
                        Actor.Print(actor);
                    });
                }

                Console.WriteLine(String.Empty);
            } while (_command != "0");
        }
    }
}
