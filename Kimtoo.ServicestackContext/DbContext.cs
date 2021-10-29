using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Kimtoo.DbContext
{
    [DebuggerStepThrough]
    public static class Db
    {
        private static Dictionary<string, IDbConnection> _dbs = new Dictionary<string, IDbConnection>();
        private static Dictionary<string, OrmLiteConnectionFactory> _factories = new Dictionary<string, OrmLiteConnectionFactory>();

        public static Exception Init(string connectionString, IOrmLiteDialectProvider dialectProvider, string instance_name = "default")
        {

            if (_dbs.ContainsKey(instance_name))
            {
                try
                {
                    if (_dbs.ContainsKey(instance_name) && _dbs[instance_name] == null)
                    {
                        _dbs[instance_name] = _factories[instance_name].Open();

                    }

                }
                catch (Exception err)
                {
                    return err;
                }
            }
            else
            {
                try
                {
                    _factories.Add(instance_name, new OrmLiteConnectionFactory
                                             (connectionString,
                                              dialectProvider));
                    _dbs.Add(instance_name, _factories[instance_name].Open());

                }
                catch (Exception err)
                {
                    return err;
                }
            }
            return null;
        }


        public static Exception Close(string instance_name = "default")
        {
            try
            {
                if (_dbs.ContainsKey(instance_name))
                {
                    try { _dbs[instance_name].Close(); } catch (Exception) { }
                    _dbs.Remove(instance_name);
                    _factories.Remove(instance_name);
                }
                return null;
            }
            catch (Exception err)
            {
                return err;
            }

        }

        public static IDbConnection Get(string instance_name = "default")
        {

            if (!_dbs.ContainsKey(instance_name)) throw new Exception($"Instance '{instance_name}' does not exist.");

            if (_dbs[instance_name].State == ConnectionState.Broken || _dbs[instance_name].State == ConnectionState.Closed)
                _dbs[instance_name] = _factories[instance_name].Open();

            return _dbs[instance_name];
        }

    }


}
