using DAL.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace View
{
    public partial class FormClientStat : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set => id = value; }
        private int? id;
        private readonly IStatisticService statistic;

        public FormClientStat(IStatisticService statistic)
        {
            InitializeComponent();
            this.statistic = statistic;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormClientStat_Load(object sender, EventArgs e)
        {
            if (!id.HasValue) return;
            try
            {
                var average = statistic.GetAverageCustomerCheck(id.Value);
                textBoxAverage.Text = average.ToString(CultureInfo.InvariantCulture);

                var countAllTOs = statistic.GetClientTOsCount(id.Value);
                textBoxAllTOs.Text = countAllTOs.ToString();

                var popTO = statistic.GetPopularTOClient(id.Value).name;
                textBoxPopular.Text = popTO;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}