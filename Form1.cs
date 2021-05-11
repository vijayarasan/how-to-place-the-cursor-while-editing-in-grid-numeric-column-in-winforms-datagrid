using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Renderers;
using Syncfusion.WinForms.GridCommon.ScrollAxis;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using Syncfusion.WinForms.Input;

namespace SfDataGridDemo
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            sfDataGrid.AutoGenerateColumns = false;
            sfDataGrid.DataSource = new ViewModel().Orders;
            sfDataGrid.LiveDataUpdateMode = LiveDataUpdateMode.AllowDataShaping;
            sfDataGrid.AllowEditing = true;
            sfDataGrid.ShowGroupDropArea = true;
            GridNumericColumn gridTextColumn1 = new GridNumericColumn() { MappingName = "OrderID", HeaderText = "Order ID" };
            GridTextColumn gridTextColumn2 = new GridTextColumn() { MappingName = "CustomerID", HeaderText = "Customer ID", AllowEditing = true };
            GridTextColumn gridTextColumn3 = new GridTextColumn() { MappingName = "CustomerName", HeaderText = "Customer Name" };
            GridTextColumn gridTextColumn4 = new GridTextColumn() { MappingName = "Country", HeaderText = "Country" };
            GridTextColumn gridTextColumn5 = new GridTextColumn() { MappingName = "ShipCity", HeaderText = "Ship City" };
            GridCheckBoxColumn checkBoxColumn = new GridCheckBoxColumn() { MappingName = "IsShipped", HeaderText = "Is Shipped" };
            sfDataGrid.Columns.Add(gridTextColumn1);
            sfDataGrid.Columns.Add(gridTextColumn2);
            sfDataGrid.Columns.Add(gridTextColumn3);
            sfDataGrid.Columns.Add(gridTextColumn4);
            sfDataGrid.Columns.Add(gridTextColumn5);
            sfDataGrid.Columns.Add(checkBoxColumn);
            this.sfDataGrid.CellRenderers.Remove("Numeric");
            this.sfDataGrid.CellRenderers.Add("Numeric", new GridNumericCellRendererExt(sfDataGrid));
        }
    }

    public class GridNumericCellRendererExt : GridNumericCellRenderer
    {
        private SfDataGrid dataGrid;

        public GridNumericCellRendererExt(SfDataGrid dataGrid) : base()
        {
            this.dataGrid = dataGrid;
        }

        protected override void OnInitializeEditElement(DataColumnBase column, RowColumnIndex rowColumnIndex, SfNumericTextBox uiElement)
        {
            base.OnInitializeEditElement(column, rowColumnIndex, uiElement);
            // Below code is used to set caret position based on mouse position
            switch (dataGrid.EditorSelectionBehavior)
            {
                case EditorSelectionBehavior.Default:
                    uiElement.SelectionStart = 1;
                    uiElement.SelectionLength = 0;
                    break;
                case EditorSelectionBehavior.SelectAll:
                    uiElement.SelectAll();
                    break;

                case EditorSelectionBehavior.MoveLast:
                    uiElement.SelectionStart = uiElement.Text.Length;
                    uiElement.SelectionLength = 0;
                    break;
            }
        }
    }

    public class OrderInfo : INotifyPropertyChanged
    {
        decimal? orderID;
        string customerId;
        string country;
        string customerName;
        string shippingCity;
        bool isShipped;

        public OrderInfo()
        {

        }

        public decimal? OrderID
        {
            get { return orderID; }
            set { orderID = value; this.OnPropertyChanged("OrderID"); }
        }

        public string CustomerID
        {
            get { return customerId; }
            set { customerId = value; this.OnPropertyChanged("CustomerID"); }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; this.OnPropertyChanged("CustomerName"); }
        }

        public string Country
        {
            get { return country; }
            set { country = value; this.OnPropertyChanged("Country"); }
        }

        public string ShipCity
        {
            get { return shippingCity; }
            set { shippingCity = value; this.OnPropertyChanged("ShipCity"); }
        }

        public bool IsShipped
        {
            get { return isShipped; }
            set { isShipped = value; this.OnPropertyChanged("IsShipped"); }
        }


        public OrderInfo(decimal? orderId, string customerName, string country, string customerId, string shipCity, bool isShipped)
        {
            this.OrderID = orderId;
            this.CustomerName = customerName;
            this.Country = country;
            this.CustomerID = customerId;
            this.ShipCity = shipCity;
            this.IsShipped = isShipped;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ViewModel
    {
        private ObservableCollection<OrderInfo> orders;
        public ObservableCollection<OrderInfo> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

        public ViewModel()
        {
            orders = new ObservableCollection<OrderInfo>();
            orders.Add(new OrderInfo(1, "Thomas Hardy", "Germany", "ALFKI", "Berlin", true));
            orders.Add(new OrderInfo(2, "Laurence Lebihan", "Mexico", "ANATR", "Mexico", false));
            orders.Add(new OrderInfo(3, "Antonio Moreno", "Mexico", "ANTON", "Mexico", true));
            orders.Add(new OrderInfo(4, "Thomas Hardy", "UK", "AROUT", "London", true));
            orders.Add(new OrderInfo(5, "Christina Berglund", "Sweden", "BERGS", "Lula", false));
        }
    }
}
