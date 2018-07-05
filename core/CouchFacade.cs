using System;
using System.Collections.Generic;
using couchbase.Schema;
using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using Couchbase.Linq;

namespace couchbase.Core
{
    /// <summary>
    /// Facade for accessing couchbase.
    /// </summary>
    public class CouchFacade {

        /// <summary>
        /// Couchbase cluster.
        /// </summary>
        private Cluster _cluster;

        /// <summary>
        /// Name of the used bucket.
        /// </summary>
        private string _bucketName;

        /// <summary>
        /// Create a new facade for accessing a specific bucket.
        /// </summary>
        /// <param name="bucketName">The name of the bucket to access</param>
        private CouchFacade(string bucketName)
        {
            _bucketName = bucketName;
            _cluster = new Cluster(new ClientConfiguration
            {
                Servers = new List<Uri> {
                    new Uri(Program.Settings.Uri)
                }
            });

            try
            {
                ClusterHelper.Initialize(_cluster.Configuration);
                _cluster.Authenticate(new PasswordAuthenticator(Program.Settings.Username, Program.Settings.Password));

                var manager = _cluster.CreateManager(Program.Settings.Username, Program.Settings.Password);
                manager.CreateBucket(bucketName);
                
                using (var bucket = _cluster.OpenBucket(_bucketName))
                {
                    bucket.CreateManager().CreateN1qlIndex("index", false, "name", "id");
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(String.Format("Error: {0}", e.Message));
            }        
        }

        /// <summary>
        /// Public method to create a new facade for a specific bucket.
        /// </summary>
        /// <param name="bucketName">The name of the bucket to open</param>
        /// <returns>A facade to access the specified bucket</returns>
        public static CouchFacade create(string bucketName)
        {
            return new CouchFacade(bucketName);
        }

        /// <summary>
        /// Get a sperate bucket object.
        /// </summary>
        /// <returns>A seperate bucket</returns>
        public IBucket Bucket()
        {
            return _cluster.OpenBucket(_bucketName);
        }

        /// <summary>
        /// Get a specific document from bucket using it's id.
        /// </summary>
        /// <param name="id">The id to look for</param>
        /// <typeparam name="T">The document type</typeparam>
        /// <returns>The found document or an empty document if id not existing</returns>
        public T GetDocument<T>(string id)
        {
            using (var bucket = _cluster.OpenBucket(_bucketName))
            {
                var result = bucket.GetDocument<T>(id);
                if (result.Success)
                {
                    return result.Content;
                }

                return default(T);
            }
        }

        /// <summary>
        /// Store a given document.
        /// </summary>
        /// <param name="t">The document to store</param>
        /// <typeparam name="T">The document type</typeparam>
        /// <returns>The stored document</returns>
        public T StoreDocument<T>(T t) where T : MainDocument
        {
            using (var bucket = _cluster.OpenBucket(_bucketName))
            {
                var result = bucket.Insert(new Document<T>
                {
                    Id = t.ID,
                    Content = t
                });

                if (result.Success)
                {
                    return result.Content;
                }

                return default(T);
            } 
        }

        /// <summary>
        /// Update a given document or create it.
        /// </summary>
        /// <param name="t">The document to udpate/ store</param>
        /// <typeparam name="T">The document type</typeparam>
        /// <returns>The updated/ stored document</returns>
        public T UpdateDocument<T>(T t) where T : MainDocument
        {
            using (var bucket = _cluster.OpenBucket(_bucketName))
            {
                var result = bucket.Upsert(new Document<T>
                {
                    Id = t.ID,
                    Content = t
                });

                if (result.Success)
                {
                    return result.Content;
                }

                return default(T);
            }
        }

        /// <summary>
        /// Replace a given document.
        /// </summary>
        /// <param name="t">The document to replace</param>
        /// <typeparam name="T">The document type</typeparam>
        /// <returns>The replaced document</returns>
        public T ReplaceDocument<T>(T t) where T : MainDocument
        {
            using (var bucket = _cluster.OpenBucket(_bucketName))
            {
                var result = bucket.Replace(new Document<T>
                {
                    Id = t.ID,
                    Content = t
                });

                if (result.Success)
                {
                    return result.Content;
                }

                return default(T);
            }
        }

        /// <summary>
        /// Delete a given document.
        /// </summary>
        /// <param name="t">The document to delete</param>
        /// <typeparam name="T">The document type</typeparam>
        /// <returns>Was deleting successful?</returns>
        public bool DeleteDocument<T>(T t) where T : MainDocument
        {
            using (var bucket = _cluster.OpenBucket(_bucketName))
            {
                var result = bucket.Remove(new Document<string>
                {
                    Id = t.ID
                });

                return result.Success;
            }
        }
    }
}