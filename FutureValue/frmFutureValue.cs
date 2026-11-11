namespace FutureValue
{
    public partial class frmFutureValue : Form
    {
        public frmFutureValue()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidData())
                {
                    decimal monthlyInvestment = Convert.ToDecimal(txtMonthlyInvestment.Text);
                    decimal yearlyInterestRate = Convert.ToDecimal(txtInterestRate.Text);
                    int years = Convert.ToInt32(txtYears.Text);

                    int months = years * 12;
                    decimal monthlyInterestRate = yearlyInterestRate / 12 / 100;

                    decimal futureValue = CalculateFutureValue(monthlyInvestment, months, monthlyInterestRate);

                    txtFutureValue.Text = futureValue.ToString("c");
                    txtMonthlyInvestment.Focus();
                }
                
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace, "Exception");
            }
        }

        private bool IsValidData()
        {
            bool success = true;
            string errormessage = "";
            errormessage += IsDecimal(txtMonthlyInvestment.Text, "Monthly Investment");
            errormessage += IsWithinRange(txtMonthlyInvestment.Text, "Monthly Investment", 1, 1000);
            errormessage += IsDecimal(txtInterestRate.Text, "Yearly Interest Rate");
            errormessage += IsWithinRange(txtInterestRate.Text, "Yearly Interest Rate", 1, 20);
            errormessage += IsInt32(txtYears.Text, "Number of Years");
            errormessage += IsWithinRange(txtYears.Text, "Number of Years", 1, 40);

            if (errormessage != "")
            {
                success = false;
                MessageBox.Show(errormessage, "Entry Error");
            }
            return success;
        }

        private string IsDecimal(string value, string name)
        {
            string message = "";
            if (!Decimal.TryParse(value, out _))
            {
                message = name + " must be a valid decimal value \n";
            }
            return message;
        }

        private string IsInt32(string value, string name)
        {
            string message = "";
            if (!Int32.TryParse(value, out _))
            {
                message = name + " must be a valid integer value \n";
            }
            return message;
        }

        private string IsWithinRange(string value, string name, decimal min, decimal max)
        {
            string message = "";
            if (Decimal.TryParse(value, out decimal number))
            {
                if (number < min || number > max)
                {
                    message = name + " must be between " + min + " and " + max + "\n";
                }
            }
            return message;
        }

        private static decimal CalculateFutureValue(decimal monthlyInvestment, int months, decimal monthlyInterestRate)
        {
            decimal futureValue = 0m;
            for (int i = 0; i < months; i++)
            {
                futureValue = (futureValue + monthlyInvestment)
                            * (1 + monthlyInterestRate);
            }

            return futureValue;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearFutureValue(object sender, EventArgs e)
        {
            txtFutureValue.Text = "";
        }
    }
}