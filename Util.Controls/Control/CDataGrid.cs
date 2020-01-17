using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Util.Controls
{
	public class CDataGrid : DataGrid
	{
		public CDataGrid()
		{
			
		}



		public ICommand Command_ColumnFilter
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register("Command_ColumnFilter", typeof(ICommand), typeof(CDataGrid), new PropertyMetadata(null, (o, e) =>
			{
				var dg = o as CDataGrid;
				dg.Command_ColumnFilter = e.NewValue as ICommand;
			}));

	

		private ChildType FindVisualChild<ChildType>(DependencyObject obj) where ChildType : DependencyObject
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				if (child != null && child is ChildType)
				{
					return (child as ChildType);
				}
				else
				{
					ChildType childOfChild = FindVisualChild<ChildType>(child);
					if (childOfChild != null)
						return (childOfChild);
				}
			}
			return null;
		}



		#region  键盘方向键，非只读列直接处于可以编辑状态
		protected override void OnCurrentCellChanged(EventArgs e)
		{
			base.OnCurrentCellChanged(e);
			this.BeginEdit();
			this.SelectionUnit = DataGridSelectionUnit.FullRow;
		}

		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			if (e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Up || e.Key == Key.Right)
			{
				this.CommitEdit();
				UnselectAllCells();
			}
			base.OnPreviewKeyDown(e);
		}

		protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
		{
			this.SelectionUnit = DataGridSelectionUnit.Cell;
			
			base.OnMouseRightButtonUp(e);

		}
		#endregion



	

		

		

	


		
	}

}
