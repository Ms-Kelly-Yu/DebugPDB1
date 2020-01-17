using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Util.Controls
{
	public interface ISimpleFactory<TKey, TValue>
	{
		TValue GetInstance(TKey key);
	}

	public static class Singleton<TItem> where TItem : class, new()
	{
		// Fields
		private static TItem _Instance;

		// Methods
		static Singleton()
		{
			Singleton<TItem>._Instance = default(TItem);
		}

		public static TItem GetInstance()
		{
			if (Singleton<TItem>._Instance == null)
			{
				Interlocked.CompareExchange<TItem>(ref Singleton<TItem>._Instance, Activator.CreateInstance<TItem>(), default(TItem));
			}
			return Singleton<TItem>._Instance;
		}
	}




	public static class UtilitySys
	{
		public static string GetPhysicalPath(string relativePath)
		{
			if (string.IsNullOrEmpty(relativePath))
			{
				return string.Empty;
			}
			relativePath = relativePath.Replace("/", @"\").Replace("~", string.Empty).Replace("~/", string.Empty);
			relativePath = relativePath.StartsWith(@"\") ? relativePath.Substring(1, relativePath.Length - 1) : relativePath;
			return Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, relativePath);
		}


		public static string ToSafeString(this object obj)
		{
			if (obj == null)
			{
				return string.Empty;
			}
			return obj.ToString();
		}




		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr hObject);



		public static ImageSource CreateImageSourceThumbnia(Image sourceImage, double width, double height)
		{
			if (sourceImage == null)
			{
				return null;
			}
			double num = width / ((double)sourceImage.Width);
			double num2 = height / ((double)sourceImage.Height);
			float num3 = (float)Math.Min(num, num2);
			int num4 = sourceImage.Width;
			int num5 = sourceImage.Height;
			if (num3 < 1f)
			{
				num4 = (int)Math.Round((double)(sourceImage.Width * num3));
				num5 = (int)Math.Round((double)(sourceImage.Height * num3));
			}
			Bitmap bitmap = new Bitmap(sourceImage, num4, num5);
			IntPtr hbitmap = bitmap.GetHbitmap();
			BitmapSource source = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
			source.Freeze();
			DeleteObject(hbitmap);
			sourceImage.Dispose();
			bitmap.Dispose();
			return source;
		}

		public static ImageSource CreateImageSourceThumbnia(string fileName, double width, double height)
		{
			Image original = Image.FromFile(fileName);
			double num = width / ((double)original.Width);
			double num2 = height / ((double)original.Height);
			float num3 = (float)Math.Min(num, num2);
			int num4 = original.Width;
			int num5 = original.Height;
			if (num3 < 1f)
			{
				num4 = (int)Math.Round((double)(original.Width * num3));
				num5 = (int)Math.Round((double)(original.Height * num3));
			}
			Bitmap bitmap = new Bitmap(original, num4, num5);
			IntPtr hbitmap = bitmap.GetHbitmap();
			BitmapSource source = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
			source.Freeze();
			DeleteObject(hbitmap);
			original.Dispose();
			bitmap.Dispose();
			return source;
		}


		public static void TryRunByThreadPool(Action fun, bool skipException = true)
		{
			ThreadPool.QueueUserWorkItem(obj => TryRunSkipExceptoin(fun, skipException));
		}

		public static void TryRunSkipExceptoin(Action fun, bool skipException = true)
		{
			if (skipException)
			{
				try
				{
					fun();
				}
				catch (Exception exception)
				{
					//LogHelper.Error(exception.Message, exception);
				}
			}
			else
			{
				fun();
			}
		}


		public static bool IsInvalid(this string value)
		{
			bool flag = string.IsNullOrEmpty(value);
			if (!flag)
			{
				flag = string.IsNullOrEmpty(value);
			}
			return flag;
		}





		public static string GetFileSize(int len)
		{
			return GetFileSize((long)len, "F2");
		}

		public static string GetFileSize(string filePath)
		{
			if (!File.Exists(filePath))
			{
				return string.Empty;
			}
			FileInfo info = new FileInfo(filePath);
			return GetFileSize(info.Length, "F2");
		}

		public static string GetFileSize(long len, string format = "F2")
		{
			if (len <= 0L)
			{
				return "0 KB";
			}
			string str = " B";
			double d = len;
			double num2 = 1024.0;
			if (len >= num2)
			{
				d = ((double)len) / num2;
				str = " KB";
			}
			if (d > num2)
			{
				d /= num2;
				str = " MB";
			}
			if (d > num2)
			{
				d /= num2;
				str = " GB";
			}
			if ((d - Math.Truncate(d)) == 0.0)
			{
				return (d.ToString("F2") + str);
			}
			return (d.ToString("F2") + str);
		}
	}





}
