using Couchbase.Core;

namespace couchbase.Schema
{
    /// <summary>
    /// Main document.
    /// </summary>
    public abstract class MainDocument
    {
        /// <summary>
        /// Id of the document.
        /// </summary>
        /// <returns>The documents id</returns>
        public string ID {
            get;
            set;
        }

        /// <summary>
        /// Create a new main document.
        /// </summary>
        /// <param name="id">The id of the document</param>
        public MainDocument(string id)
        {
            this.ID = id;
        }
    }
}