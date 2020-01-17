using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			ThreadLocal<string> threadName = new ThreadLocal<string>(() =>
			{
				return "Thread" + Thread.CurrentThread.ManagedThreadId;
			});

			Action action = () =>
			{
				bool repeat = threadName.IsValueCreated;

				tb.Text = string.Format("ThreadName = {0} {1}", threadName.Value, repeat ? "(repeat)" : "");
			};

			Parallel.Invoke(action, action, action, action);
			threadName.Dispose();

			//List<string> vs = new List<string>
			//{
			//	"abc",
			//	"def"
			//};
			//var vb = vs.Select(x => x != null);
		}
	}
}
