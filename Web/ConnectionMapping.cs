using System.Collections.Generic;
using System.Linq;

namespace Web
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connectionStore =
            new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get { return _connectionStore.Count; }
        }

        public IEnumerable<string> All()
        {
            lock (_connectionStore)
            {
                List<string> connections = new List<string>();
                foreach (var key in _connectionStore)
                {
                    foreach (var connection in key.Value)
                    {
                        connections.Add(connection);
                    }
                }
                return connections;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock (_connectionStore)
            {
                HashSet<string> connections;
                if (!_connectionStore.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connectionStore.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            lock (_connectionStore)
            {
                HashSet<string> connections;
                if (_connectionStore.TryGetValue(key, out connections))
                {
                    return connections;
                }
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connectionStore)
            {
                HashSet<string> connections;
                if (!_connectionStore.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connectionStore.Remove(key);
                    }
                }
            }
        }
    }


}