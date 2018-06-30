using System;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using Couchbase.Linq;

public class CouchFacade {

    private Settings _settings = new Settings();

    private Cluster _cluster;

    private string _bucketName;

    private CouchFacade(string bucketName)
    {
        _bucketName = bucketName;
        _cluster = new Cluster(new ClientConfiguration
        {
            Servers = new List<Uri> {
                new Uri(_settings.Uri)
            }
        });

        ClusterHelper.Initialize(_cluster.Configuration);
        _cluster.Authenticate(new PasswordAuthenticator(_settings.Username, _settings.Password));
    }

    public static CouchFacade create(string bucketName)
    {
        return new CouchFacade(bucketName);
    }

    public IBucket Bucket()
    {
        return _cluster.OpenBucket(_bucketName);
    }

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