using DAL.Interface;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace View
{
    public partial class FormOrderView : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private List<OrderTOViewModel> pointsOfOrder;
        private readonly IMainService mainService;

        public FormOrderView(IMainService mainService)
        {
            InitializeComponent();
            this.mainService = mainService;
        }

        private void FormOrderView_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    OrderViewModel order = mainService.GetElement(id.Value);

                    List<OrderTOViewModel> carOrder = order.OrderTOs;

                    pointsOfOrder = new List<OrderTOViewModel>();

                    foreach (var element in carOrder)
                    {
                        pointsOfOrder.Add(new OrderTOViewModel
                        {
                            TOName = element.TOName,
                            Amount = element.Amount
                        });
                    }

                    if (pointsOfOrder != null)
                    {
                        dataGridView.DataSource = pointsOfOrder;
                        dataGridView.Columns[0].Visible = false;
                        dataGridView.Columns[1].Visible = false;
                        dataGridView.Columns[2].Visible = false;
                        dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
                }
            }
        }
    }
}