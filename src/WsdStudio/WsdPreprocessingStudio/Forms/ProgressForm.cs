using System;
using System.Drawing;
using System.Windows.Forms;

namespace WsdPreprocessingStudio.Forms
{
    public partial class ProgressForm : Form
    {
        public ProgressForm(string title)
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
            
            Text = title;
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            if (Owner != null)
            {
                Owner.Enabled = false;

                Location = new Point(
                    Owner.Left + Owner.Width / 2 - Width / 2,
                    Owner.Top + Owner.Height / 2 - Height / 2);
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Owner != null)
                    Owner.Enabled = true;
                
                components?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
