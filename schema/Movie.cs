using System;
using System.Linq;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Linq;
using Couchbase.Linq.Filters;
using Couchbase.N1QL;
using Newtonsoft.Json;
using couchbase.Core;
using couchbase.Schema;

namespace couchbase.Schema
{
    /// <summary>
    /// Class representing a movie.
    /// </summary>
    public class Movie : MainDocument {

        /// <summary>
        /// The movies name.
        /// </summary>
        /// <returns>The name</returns>
        public string Name { get; set; }

        /// <summary>
        /// The movies release date.
        /// </summary>
        /// <returns>The release date</returns>
        public DateTime Release { get; set; }

        /// <summary>
        /// The movies description.
        /// </summary>
        /// <returns>The description</returns>
        public string Description { get; set; }

        /// <summary>
        /// The ids of the involved actors.
        /// </summary>
        /// <returns>List of actor ids</returns>
        public List<string> Actors = new List<string>();
        
        /// <summary>
        /// The name of the used bucket.
        /// </summary>
        private static readonly string _table = "movies";

        /// <summary>
        /// The used facade for this class.
        /// </summary>
        /// <returns>The facade</returns>
        public static readonly CouchFacade Facade = CouchFacade.create(_table);

        /// <summary>
        /// Create an empty actor.
        /// </summary>
        /// <returns>An empty actor</returns>
        public static Movie Empty
        {
            get => new Movie(String.Empty, "No Result!");
        }

        /// <summary>
        /// Create a new movie.
        /// </summary>
        /// <param name="id">The movies id</param>
        /// <param name="name">The movies name</param>
        /// <returns>The new movie</returns>
        public Movie(string id, string name) : base(id)
        {
            this.Name = name;
        }

        /// <summary>
        /// Create a new movie.
        /// </summary>
        /// <param name="id">The movies id</param>
        /// <param name="name">The movies name</param>
        /// <param name="actors">List of movies actors</param>
        /// <returns>The nwe movie</returns>
        public Movie(string id, string name, List<string> actors) : this(id, name)
        {
            this.Actors = actors;
        }

        /// <summary>
        /// Create a new movie. Is used for Newtonsoft.JSON
        /// </summary>
        /// <param name="id">The movies id</param>
        /// <param name="name">The movies name</param>
        /// <param name="actors">List of movies actors</param>
        /// <param name="release">The movies release date</param>
        /// <param name="description">The movies description</param>
        /// <returns>The new movie</returns>
        [JsonConstructor]
        public Movie(string id, string name, List<string> actors, DateTime release, string description) : this(id, name, actors)
        {
            this.Release = release;
            this.Description = description;
        }

        /// <summary>
        /// Get a list with all actors.
        /// </summary>
        /// <returns>List with actors</returns>
        public List<Actor> GetActors()
        {
            List<Actor> actors = new List<Actor>();

            Actors.ForEach(id => actors.Add(Actor.Facade.GetDocument<Actor>(id)));
            return actors;
        }

        /// <summary>
        /// Get a movie by it's id.
        /// </summary>
        /// <param name="id">The id to search for</param>
        /// <returns>An empty movie or the found movie</returns>
        public static Movie ByID(string id)
        {
            var context = new BucketContext(ClusterHelper.GetBucket(_table));
            var query = (from m in context.Query<Movie>()
                        where m.ID == id
                        select m);

            return query.Count() == 0 ? Movie.Empty : query.First();
        }

        /// <summary>
        /// Get a moview by it's name.
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <returns>An empty movie or the found movie</returns>
        public static Movie WhereName(string name)
        {
            var context = new BucketContext(ClusterHelper.GetBucket(_table));
            var query = (from m in context.Query<Movie>()
                        where m.Name == name
                        select m);

            return query.Count() == 0 ? Movie.Empty : query.First();
        }

        /// <summary>
        /// Get a list of movies by a string.
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="count">The cound of results to return</param>
        /// <returns>A list of movies</returns>
        public static List<Movie> WhereNameLike(string name, int count = 10)
        {
            var context = new BucketContext(ClusterHelper.GetBucket(_table));
            var query = (from m in context.Query<Movie>()
                        where m.Name.Contains(name)
                        select m).Take(count);

            return query.ToList();
        }

        /// <summary>
        /// Print the given movie.
        /// </summary>
        /// <param name="movie">The movie to print</param>
        public static void Print(Movie movie)
        {
            Console.WriteLine(String.Format("| Name: {0}", movie.Name));
            Console.WriteLine(String.Format("| Description: {0}", movie.Description));
            Console.WriteLine(String.Format("| Release: {0}", movie.Release));
            Console.WriteLine("| Actors: ");
            movie.GetActors().ForEach(m => 
            {
                Console.WriteLine(String.Format("| -Name: {0}", m.Name));
            });
        }
    }
}