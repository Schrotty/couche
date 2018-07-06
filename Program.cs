using System;
using System.Linq;
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
                Console.Clear();

                Console.WriteLine("+===================================+");
                Console.WriteLine("| == Enter couchbase credentials == |");
                Console.WriteLine("+===================================+");

                Console.WriteLine(String.Format("| Enter uri ({0}): ", Program.Settings.Uri));
                Console.WriteLine(String.Format("| Enter username ({0}): ", Program.Settings.Username));
                Console.WriteLine(String.Format("| Enter password ({0}): ", Program.Settings.Password));
                Console.WriteLine("+===================================+");

                // enter uri
                Console.SetCursorPosition(16 + Program.Settings.Uri.Length, 3);
                var _hostname = Console.ReadLine();
                Program.Settings.Uri = String.IsNullOrEmpty(_hostname) ? Program.Settings.Uri : _hostname;

                // enter username
                Console.SetCursorPosition(21 + Program.Settings.Username.Length, 4);
                var _username = Console.ReadLine();
                Program.Settings.Username = String.IsNullOrEmpty(_username) ? Program.Settings.Username : _username;

                // enter password
                Console.SetCursorPosition(21 + Program.Settings.Password.Length, 5);
                var _password = Console.ReadLine();
                Program.Settings.Password = String.IsNullOrEmpty(_password) ? Program.Settings.Password : _password;

                // move curser beyond menu
                Console.SetCursorPosition(0, 7);               

                DummyCreator.CreateDummies();
                string _command = String.Empty;

                // clear console
                Console.Clear();
                
                do {
                    Console.WriteLine("+=== Couchbase Access Console ===+");
                    Console.WriteLine("| Movie:                         |");
                    Console.WriteLine("| 1 - Get movie by ID            |");
                    Console.WriteLine("| 2 - Get movie by name          |");
                    Console.WriteLine("| 3 - Get list of movies by name |");
                    Console.WriteLine("| 7 - Create new movie           |");
                    Console.WriteLine("+================================+");
                    Console.WriteLine("| Actor:                         |");
                    Console.WriteLine("| 4 - Get actor by ID            |");
                    Console.WriteLine("| 5 - Get actor by name          |");
                    Console.WriteLine("| 6 - Get list of actors by name |");
                    Console.WriteLine("| 8 - Create new actor           |");
                    Console.WriteLine("+================================+");
                    Console.WriteLine("| Other:                         |");
                    Console.WriteLine("| 0 - Exit                       |");
                    Console.WriteLine("+================================+");
                    Console.WriteLine("| Enter command:                 |");
                    Console.WriteLine("+================================+");

                    Console.SetCursorPosition(17, 16);
                    _command = Console.ReadLine();

                    Console.Clear();
                    if (_command.Equals("1"))
                    {
                        Console.WriteLine("+=======================+");
                        Console.WriteLine("| == Get movie by ID == |");
                        Console.WriteLine("+=======================+");
                        Console.WriteLine("| Enter ID:             |");
                        Console.WriteLine("+=======================+");

                        Console.SetCursorPosition(12, 3);

                        var movie = Movie.ByID(Console.ReadLine());

                        Console.SetCursorPosition(0, 5);
                        Movie.Print(movie);

                        Console.WriteLine("+=======================+");
                    }

                    if (_command.Equals("2"))
                    {
                        Console.WriteLine("+=============================+");
                        Console.WriteLine("| ==   Get movie by name   == |");
                        Console.WriteLine("+=============================+");
                        Console.WriteLine("| Enter name:                 |");
                        Console.WriteLine("+=============================+");
                        Console.SetCursorPosition(14, 3);

                        var movie = Movie.WhereName(Console.ReadLine());

                        Console.SetCursorPosition(0, 5);
                        Movie.Print(movie);

                        Console.WriteLine("+=============================+");
                    }

                    if (_command.Equals("3"))
                    {
                        Console.WriteLine("+======================================+");
                        Console.WriteLine("| ==   Get list of movies by name   == |");
                        Console.WriteLine("+======================================+");
                        Console.WriteLine("| Enter string:                        |");
                        Console.WriteLine("+======================================+");
                        Console.SetCursorPosition(16, 3);

                        var movies = Movie.WhereNameLike(Console.ReadLine());
                        Console.SetCursorPosition(0, 5);

                        movies.ForEach(movie => 
                        {
                            Movie.Print(movie);
                            Console.WriteLine("+======================================+");
                        });
                       
                       if (movies.Count == 0)
                       {
                            Console.WriteLine("+ ==           No Result!           == +");
                            Console.WriteLine("+======================================+");
                       }
                    }

                    if (_command.Equals("4"))
                    {
                        Console.WriteLine("+=======================+");
                        Console.WriteLine("| == Get actor by ID == |");
                        Console.WriteLine("+=======================+");
                        Console.WriteLine("| Enter ID:             |");
                        Console.WriteLine("+=======================+");

                        Console.SetCursorPosition(12, 3);

                        var actor = Actor.ByID(Console.ReadLine());

                        Console.SetCursorPosition(0, 5);
                        Actor.Print(actor);

                        Console.WriteLine("+=======================+");
                    }

                    if (_command.Equals("5"))
                    {
                        Console.WriteLine("+=============================+");
                        Console.WriteLine("| ==   Get actor by name   == |");
                        Console.WriteLine("+=============================+");
                        Console.WriteLine("| Enter name:                 |");
                        Console.WriteLine("+=============================+");
                        Console.SetCursorPosition(14, 3);

                        var actor = Actor.WhereName(Console.ReadLine());

                        Console.SetCursorPosition(0, 5);
                        Actor.Print(actor);

                        Console.WriteLine("+=============================+");
                    }

                    if (_command.Equals("6"))
                    {
                        Console.WriteLine("+======================================+");
                        Console.WriteLine("| ==   Get list of actors by name   == |");
                        Console.WriteLine("+======================================+");
                        Console.WriteLine("| Enter string:                        |");
                        Console.WriteLine("+======================================+");
                        Console.SetCursorPosition(16, 3);

                        var actors = Actor.WhereNameLike(Console.ReadLine());
                        Console.SetCursorPosition(0, 5);

                        actors.ForEach(actor => 
                        {
                            Actor.Print(actor);
                            Console.WriteLine("+======================================+");
                        });
                       
                       if (actors.Count == 0)
                       {
                            Console.WriteLine("+ ==           No Result!           == +");
                            Console.WriteLine("+======================================+");
                       }
                    }

                    if (_command.Equals("7"))
                    {
                        Console.WriteLine("+========================================+");
                        Console.WriteLine("| ==      Create new a new movie      == |");
                        Console.WriteLine("+========================================+");
                        Console.WriteLine("| Enter ID:                              |");
                        Console.WriteLine("+========================================+");
                        Console.WriteLine("| Enter name:                            |");
                        Console.WriteLine("+========================================+");
                        Console.WriteLine("| Enter description:                     |");
                        Console.WriteLine("|                                        |");
                        Console.WriteLine("+========================================+");
                        Console.WriteLine("| Enter release date:                    |");
                        Console.WriteLine("+========================================+");
                        Console.WriteLine("| Enter ',' seperated list of actor IDs: |");
                        Console.WriteLine("|                                        |");
                        Console.WriteLine("+========================================+");

                        Console.SetCursorPosition(12, 3);
                        var id = Console.ReadLine();

                        Console.SetCursorPosition(14, 5);
                        var name = Console.ReadLine();

                        Console.SetCursorPosition(2, 8);
                        var description = Console.ReadLine();

                        Console.SetCursorPosition(22, 10);
                        var release = Console.ReadLine();

                        Console.SetCursorPosition(2, 13);
                        var actors = Console.ReadLine();

                        var acc = actors.Split(new string[] { ", ", "," }, StringSplitOptions.RemoveEmptyEntries);
                        Movie.Facade.StoreDocument(new Movie(id, name, acc.OfType<string>().ToList(), DateTime.Parse(release), description));

                        Console.SetCursorPosition(0, 15);
                    }

                    if (_command.Equals("8"))
                    {
                        Console.WriteLine("+========================================+");
                        Console.WriteLine("| ==      Create new a new actor      == |");
                        Console.WriteLine("+========================================+");
                        Console.WriteLine("| Enter ID:                              |");
                        Console.WriteLine("+========================================+");
                        Console.WriteLine("| Enter name:                            |");
                        Console.WriteLine("+========================================+");

                        Console.SetCursorPosition(12, 3);
                        var id = Console.ReadLine();

                        Console.SetCursorPosition(14, 5);
                        var name = Console.ReadLine();

                        Actor.Facade.StoreDocument(new Actor(id, name));

                        Console.SetCursorPosition(0, 7);
                    }

                    if (!_command.Equals("0"))
                    {
                        Console.WriteLine("Press any key to continue...");

                        Console.ReadKey();
                        Console.Clear();
                    }
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
