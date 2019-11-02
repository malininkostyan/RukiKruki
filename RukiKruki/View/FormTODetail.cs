﻿using DAL.Interface;
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

namespace View
{
    public partial class FormTODetail : Form
    {
        public TO_DetailViewModel Model
        {
            set { model = value; }
            get
            {
                return model;
            }
        }
        private TO_DetailViewModel model;
        private readonly IDetailService detail;

        public FormTODetail(IDetailService detail)
        {
            InitializeComponent();
            this.detail = detail;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAmount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxDetails.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new TO_DetailViewModel
                    {
                        DetailId = Convert.ToInt32(comboBoxDetails.SelectedValue),
                        DetailName = comboBoxDetails.Text,
                        Amount = Convert.ToInt32(textBoxAmount.Text)
                    };
                }
                else
                {

                    model.Amount = Convert.ToInt32(textBoxAmount.Text);
                }

                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void FormTODetail_Load(object sender, EventArgs e)
        {
            try
            {
                List<DetailViewModel> detailView = detail.GetList();
                if (detailView != null)
                {
                    comboBoxDetails.DisplayMember = "DetailName";
                    comboBoxDetails.ValueMember = "Id";
                    comboBoxDetails.DataSource = detailView;
                    comboBoxDetails.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxDetails.Enabled = false;
                comboBoxDetails.SelectedValue = model.DetailId;
                textBoxAmount.Text = model.Amount.ToString();
            }
        }
    }
}