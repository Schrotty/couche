using System;
using System.Collections.Generic;

namespace couchbase
{
    /// <summary>
    /// Class for creating dummy movies and actors.
    /// </summary>
    public static class DummyCreator
    {
        /// <summary>
        /// Create dummy movies and actors.
        /// </summary>
        public static void CreateDummies()
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
            Actor.Facade.UpdateDocument<Actor>(new Actor("9", "Marlo Brando"));
            Actor.Facade.UpdateDocument<Actor>(new Actor("10", "Al Pacino"));
            Actor.Facade.UpdateDocument<Actor>(new Actor("11", "Diane Keaton"));

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

            string[] godFather = { "9", "10", "11" };
            Movie.Facade.UpdateDocument<Movie>(
                new Movie("3", "Der Pate")
                {
                    Release = new DateTime(1972, 01, 01),
                    Description = "Die Handlung setzt im Jahr 1945 ein: Don Vito Corleone, einer der mächtigsten Mafiabosse von New York City und Oberhaupt...",
                    Actors = new List<string>(godFather)
                }
            );
        }
    }
}