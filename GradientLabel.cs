using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SatIp
{
    [ToolboxBitmap(typeof(Label))]
    public class GradientLabel22 : UserControl
    {
        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private readonly Container components = null;

        private Color firstColor;


        private Label label1;
        private Color lastColor;

        private int paddingTop, paddingLeft;
       

        public GradientLabel22()
        {
            SetStyle(ControlStyles.DoubleBuffer, true);

            //
            // This call is required by the Windows.Forms Form Designer.
            //
            InitializeComponent();

            firstColor = SystemColors.InactiveCaption;
            lastColor = Color.White;

            label1.Paint += workingLabel_Paint;
        }

        [Browsable(true)]
        [Category("Gradient")]
        public Color FirstColor
        {
            get => firstColor;
            set
            {
                firstColor = value;
                label1.Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Gradient")]
        public Color LastColor
        {
            get => lastColor;
            set
            {
                lastColor = value;
                label1.Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Gradient")]
        public string Caption
        {
            get => label1.Text;
            set
            {
                label1.Text = value;
                label1.Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Gradient")]
        public Color TextColor
        {
            get => label1.ForeColor;
            set
            {
                label1.ForeColor = value;
                label1.Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Gradient")]
        public Font TextFont
        {
            get => label1.Font;
            set
            {
                label1.Font = value;
                label1.Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Gradient")]
        [DefaultValue(0)]
        public int PaddingTop
        {
            get => paddingTop;
            set
            {
                paddingTop = value;
                label1.Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Gradient")]
        [DefaultValue(0)]
        public int PaddingLeft
        {
            get => paddingLeft;
            set
            {
                paddingLeft = value;
                label1.Invalidate();
            }
        }

        private void DrawBackground(Graphics graphics)
        {
            //
            // Create gradient brush
            //
            Brush gradientBrush = new LinearGradientBrush(label1.ClientRectangle,
                firstColor,
                lastColor,
                LinearGradientMode.Horizontal);

            //
            // Draw brush
            //
            graphics.FillRectangle(gradientBrush, ClientRectangle);
            gradientBrush.Dispose();
        }

        private void DrawForeground(Graphics graphics)
        {
            //
            // Draw bevelbox
            //
            var grayPen = new Pen(Color.FromArgb(200, 200, 200));
            graphics.DrawLine(grayPen, 0, 0, Width - 1, 0);
            graphics.DrawLine(Pens.WhiteSmoke, 0, Height - 1, Width - 1, Height - 1);
            grayPen.Dispose();

            //
            // Draw caption
            //
            graphics.DrawString(Caption,
                TextFont,
                new SolidBrush(TextColor),
                paddingLeft, paddingTop);
        }

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 150);
            this.label1.TabIndex = 0;
            
            // 
            // GradientLabel22
            // 
            
            this.Controls.Add(this.label1);
            this.Name = "GradientLabel22";
            this.ResumeLayout(false);
        }

        #endregion

        private void workingLabel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(SystemColors.Control);

            DrawBackground(e.Graphics);
            DrawForeground(e.Graphics);
        }
    }
}