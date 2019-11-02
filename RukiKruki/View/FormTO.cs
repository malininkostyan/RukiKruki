using DAL.BindingModel;
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
    public partial class FormTO : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private List<TO_DetailViewModel> _TODetails;
        private readonly ITOService _TO;

        public FormTO(ITOService _TO)
        {
            InitializeComponent();
            this._TO = _TO;
        }

        private void FormTO_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    TOViewModel _TOView = _TO.GetElement(id.Value);
                    if (_TOView != null)
                    {
                        textBoxName.Text = _TOView.TOName;
                        textBoxPrice.Text = _TOView.Price.ToString();
                        _TODetails = _TOView.TODetails;
                        LoadData();
                    }

                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                _TODetails = new List<TO_DetailViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (_TODetails != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = _TODetails;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonAddElement_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormTODetail>();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.TOId = id.Value;
                    }

                    _TODetails.Add(form.Model);

                }
                LoadData();
            }
        }

        private void buttonDeleteElement_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _TODetails.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
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

        private void buttonChangeElement_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormTODetail>();

                form.Model = _TODetails[dataGridView.SelectedRows[0].Cells[0].RowIndex];

                if (form.ShowDialog() == DialogResult.OK)
                {
                    _TODetails[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_TODetails == null || _TODetails.Count == 0)
            {
                MessageBox.Show("Заполните детали", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<TO_DetailBindingModel> _TODetailsBinding = new List<TO_DetailBindingModel>();

                for (int i = 0; i < _TODetails.Count; ++i)
                {
                    _TODetailsBinding.Add(new TO_DetailBindingModel
                    {
                        Id = _TODetails[i].Id,
                        TOId = _TODetails[i].TOId,
                        DetailId = _TODetails[i].DetailId,
                        Amount = _TODetails[i].Amount,
                    });
                }

                if (id.HasValue)
                {
                    _TO.UpdateElement(new TOBindingModel
                    {
                        Id = id.Value,
                        TOName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        TODetails = _TODetailsBinding
                    });
                }
                else
                {
                    _TO.AddElement(new TOBindingModel
                    {
                        TOName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        TODetails = _TODetailsBinding
                    });
                }

                Console.WriteLine();

                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


    }
}