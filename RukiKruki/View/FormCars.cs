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
    public partial class FormCars : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ICarService car;

        public FormCars(ICarService car)
        {
            InitializeComponent();
            this.car = car;
        }

        private void LoadData()
        {
            try
            {
                List<CarViewModel> list = car.GetList();
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
              MessageBoxIcon.Error);
            }
        }


        private void buttonDeleteElement_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id =
                   Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        car.DeleteElement(id);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void FormCars_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}