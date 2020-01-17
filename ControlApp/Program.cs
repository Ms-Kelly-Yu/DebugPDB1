using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ControlApp
{
	class Program
	{
		static void Main(string[] args)
		{
            //ThreadLocal<string> threadName = new ThreadLocal<string>(() =>
            //{
            //	return "Thread" + Thread.CurrentThread.ManagedThreadId;
            //});

            //Action action = () =>
            //{
            //	bool repeat = threadName.IsValueCreated;

            //	Console.WriteLine(string.Format("ThreadName = {0} {1}", threadName.Value, repeat ? "(repeat)" : ""));
            //};

            //Parallel.Invoke(action, action, action, action, action, action, action, action, action, action);
            //threadName.Dispose();
            //Console.Read();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Rows.Add("111", "yuli");
            dt.Rows.Add("222", "aza");

            var users = dt.ToModels<User>();
        }
	}

    public static class Extension
    {

        public static List<T> ToModels<T>(this DataTable dt) where T : new()
        {
            List<T> result = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T target = new T();
                Type targetType = target.GetType();
                foreach (DataColumn dc in dt.Columns)
                {
                    targetType.GetProperty(dc.ColumnName).SetValue(target, dr[dc.ColumnName]);
                }
                result.Add(target);
            }
            return result;
        }
    }

    public class User
    {
        public User()
        {

        }
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
