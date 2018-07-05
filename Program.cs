using System;
using System.Collections.Generic;
using couchbase.Core;
using couchbase.Schema;
using couchbase.Util;
using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;

namespace couchbase
{
    /// <summary>
    /// The Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application settings.
        /// </summary>
        /// <returns>Application settings</returns>
        public static Settings Settings = new Settings();

        /// <summary>
        /// Main entrance point for the application.
        /// </summary>
        /// <param name="args">start parameter</param>
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("===================================");
                Console.WriteLine("=== Enter couchbase credentials ===");
                Console.WriteLine("===================================");

                // enter uri
                Console.Write(String.Format("Enter uri ({0}): ", Program.Settings.Uri));

                var _hostname = Console.ReadLine();
                Program.Settings.Uri = String.IsNullOrEmpty(_hostname) ? Program.Settings.Uri : _hostname;

                // enter username
                Console.Write(String.Format("Enter username ({0}): ", Program.Settings.Username));

                var _username = Console.ReadLine();
                Program.Settings.Username = String.IsNullOrEmpty(_username) ? Program.Settings.Username : _username;

                // enter password
                Console.Write(String.Format("Enter password ({0}): ", Program.Settings.Password));

                var _password = Console.ReadLine();
                Program.Settings.Password = String.IsNullOrEmpty(_password) ? Program.Settings.Password : _password;               

                DummyCreator.CreateDummies();
                string _command = String.Empty;

                // clear console
                Console.Clear();
                
                do {
                    Console.WriteLine("+=== Couchbase Access Console ===+");
                    Console.WriteLine("| 1 - Get movie by ID            |");
                    Console.WriteLine("| 2 - Get movie by name          |");
                    Console.WriteLine("| 3 - Get list of movies by name |");
                    Console.WriteLine("| 4 - Get actor by ID            |");
                    Console.WriteLine("| 5 - Get actor by name          |");
                    Console.WriteLine("| 6 - Get list of actors by name |");
                    Console.WriteLine("| 0 - Exit                       |");
                    Console.WriteLine("+================================+");

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
            catch (Exception e)
            {
                Console.Error.WriteLine(String.Format("Error: {0}", e.Message));
                Console.ReadKey();
            }
        }
    }
}
