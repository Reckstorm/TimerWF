using Button = System.Windows.Forms.Button;
using Timer = System.Windows.Forms.Timer;
using TrackBar = System.Windows.Forms.TrackBar;

namespace TimerWF
{
    public partial class Form1 : Form
    {
        private Label _hoursLB = new Label();
        private Label _minutesLB = new Label();
        private Label _secondsLB = new Label();

        private TrackBar _hoursTB = new TrackBar();
        private TrackBar _minutesTB = new TrackBar();
        private TrackBar _secondsTB = new TrackBar();

        private Timer _timer = new Timer();

        private Button _start = new Button();
        public Form1()
        {
            InitializeComponent();
            this.Load += RenderPicker;
        }
        private void RenderPicker(object sender, EventArgs e)
        {
            _hoursTB.Location = new Point(0, 0);
            _hoursTB.Maximum = 24;
            _hoursTB.Minimum = 0;
            _hoursTB.Size = new Size(50, 300);
            _hoursTB.Orientation = Orientation.Vertical;
            _hoursTB.TickStyle = TickStyle.Both;
            _hoursTB.ValueChanged += (s, e) =>
            {
                _hoursLB.Text = $"H: {_hoursTB.Value.ToString()}";
            };

            _hoursLB.Location = new Point(0, _hoursTB.Height);
            _hoursLB.Width = 50;
            _hoursLB.Text = "H: 0";

            _minutesTB.Location = new Point(_hoursTB.Location.X + _hoursTB.Width + 10, 0);
            _minutesTB.Maximum = 60;
            _minutesTB.Minimum = 0;
            _minutesTB.Size = new Size(50, 300);
            _minutesTB.Orientation = Orientation.Vertical;
            _minutesTB.TickStyle = TickStyle.Both;
            _minutesTB.ValueChanged += (s, e) =>
            {
                _minutesLB.Text = $"M: {_minutesTB.Value.ToString()}";
            };

            _minutesLB.Location = new Point(_minutesTB.Location.X, _minutesTB.Height);
            _minutesLB.Width = 50;
            _minutesLB.Text = "M: 0";

            _secondsTB.Location = new Point(_minutesTB.Location.X + _minutesTB.Width + 10, 0);
            _secondsTB.Maximum = 60;
            _secondsTB.Minimum = 0;
            _secondsTB.Size = new Size(50, 300);
            _secondsTB.Orientation = Orientation.Vertical;
            _secondsTB.TickStyle = TickStyle.Both;
            _secondsTB.ValueChanged += (s, e) =>
            {
                _secondsLB.Text = $"S: {_secondsTB.Value.ToString()}";
            };

            _secondsLB.Location = new Point(_secondsTB.Location.X, _secondsTB.Height);
            _secondsLB.Width = 50;
            _secondsLB.Text = "S: 0";

            _start.Text = "Start";
            _start.Location = new Point(_secondsTB.Width + _secondsTB.Location.X + 10, 0);
            _start.Size = new Size(80, 30);

            this.Controls.Add(_hoursTB);
            this.Controls.Add(_minutesTB);
            this.Controls.Add(_secondsTB);

            this.Controls.Add(_hoursLB);
            this.Controls.Add(_minutesLB);
            this.Controls.Add(_secondsLB);
            
            this.Controls.Add(_start);


        }
    }
}