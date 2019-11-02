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
    public partial class FormDetail : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private readonly IDetailService detail;

        public FormDetail(IDetailService detail)
        {
            InitializeComponent();
            this.detail = detail;
        }

        private void button_CANCEL_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    detail.UpdateElement(new DetailBindingModel
                    {
                        Id = id.Value,
                        DetailName = textBoxName.Text,
                        Amount = Convert.ToInt32(textBoxTotalAmount.Text)
                    });
                }
                else
                {
                    detail.AddElement(new DetailBindingModel
                    {
                        DetailName = textBoxName.Text,
                        Amount = Convert.ToInt32(textBoxTotalAmount.Text)
                    });
                }
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

        private void FormDetail_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    DetailViewModel detailView = detail.GetElement(id.Value);
                    if (detailView != null)
                    {
                        textBoxName.Text = detailView.DetailName;
                        textBoxTotalAmount.Text = detailView.Amount.ToString();
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