using System;
using System.Data.SqlTypes;
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
        private Label _timerLB = new Label();

        private TrackBar _hoursTB = new TrackBar();
        private TrackBar _minutesTB = new TrackBar();
        private TrackBar _secondsTB = new TrackBar();

        private Timer _timer;

        private Button _start = new Button();
        private Button _reset = new Button();

        private ProgressBar _progress = new ProgressBar();

        private int _temp;
        public Form1()
        {
            InitializeComponent();
            this.Load += RenderPicker;
        }
        private void RenderPicker(object sender, EventArgs e)
        {
            _hoursTB.Location = new Point(0, 0);
            _hoursTB.Maximum = 23;
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
            _minutesTB.Maximum = 59;
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
            _start.Click += TimerStart;

            _reset.Text = "Reset";
            _reset.Location = new Point(_secondsTB.Width + _secondsTB.Location.X + 10, _start.Location.Y + _start.Height + 10);
            _reset.Size = new Size(80, 30);
            _reset.Click += (s, e) =>
            {
                _hoursTB.Value = 0;
                _minutesTB.Value = 0;
                _secondsTB.Value = 0;
            };

            this.Controls.Add(_hoursTB);
            this.Controls.Add(_minutesTB);
            this.Controls.Add(_secondsTB);

            this.Controls.Add(_hoursLB);
            this.Controls.Add(_minutesLB);
            this.Controls.Add(_secondsLB);

            this.Controls.Add(_start);
            this.Controls.Add(_reset);
        }

        private void TimerStart(object? sender, EventArgs e)
        {
            _temp = _hoursTB.Value * 3600 + _minutesTB.Value * 60 + _secondsTB.Value;

            HideControls();

            _timerLB.Font = new Font(_timerLB.Font.FontFamily, 38);
            _timerLB.Size = new Size(this.Width, 75);
            _timerLB.Location = new Point(0, (this.Height - _timerLB.Height - 100) / 2);

            _progress.Width = this.Width;
            _progress.Location = new Point(0, _timerLB.Location.Y + _timerLB.Height + 10);
            _progress.Height = 50;
            _progress.Minimum = 0;
            _progress.Maximum = _temp;
            _progress.Value = _temp;

            _timer = new Timer() { Interval = 1000 };
            _timer.Start();

            _timer.Tick += (s, e) =>
            {
                string str;
                _temp--;
                _progress.Value--;
                int hrs = _temp / 3600;
                str = hrs > 9 ? hrs.ToString() : $"0{hrs}:";
                int min = (_temp - hrs * 3600) / 60;
                str = string.Concat(str, min > 9 ? min.ToString() : $"0{min}:");
                int sec = (_temp - hrs * 3600) - (min * 60);
                str = string.Concat(str, sec > 9 ? sec.ToString() : $"0{sec}");
                _timerLB.Text = str;
                if (_temp <= 0)
                {
                    _timer.Stop();
                    int i = 0;
                    bool f = false;
                    _timer = new Timer() { Interval = 500 };
                    _timer.Start();
                    _timer.Tick += (s, e) =>
                    {
                        _timerLB.Visible = false;
                        _progress.Visible = false;
                        if (f)
                        {
                            this.BackColor = Color.Blue;
                            f = !f;
                        }
                        else
                        {
                            this.BackColor = Color.Red;
                            f = !f;
                        }
                        if (++i >= 8)
                        {
                            _timer.Stop();
                            MakeVisible();
                        };
                    };
                };
                this.Controls.Add(_timerLB);
                this.Controls.Add(_progress);
            };
        }
        private void MakeVisible()
        {
            _hoursLB.Visible = true;
            _minutesLB.Visible = true;
            _secondsLB.Visible = true;

            _hoursTB.Visible = true;
            _minutesTB.Visible = true;
            _secondsTB.Visible = true;

            _start.Visible = true;
            _reset.Visible = true;

            this.BackColor = Color.WhiteSmoke;
        }

        private void HideControls()
        {
            _hoursLB.Visible = false;
            _minutesLB.Visible = false;
            _secondsLB.Visible = false;

            _hoursTB.Visible = false;
            _hoursTB.Value = 0;
            _minutesTB.Visible = false;
            _minutesTB.Value = 0;
            _secondsTB.Visible = false;
            _secondsTB.Value = 0;

            _start.Visible = false;
            _reset.Visible = false;

            _timerLB.Visible = true;
            _progress.Visible = true;
        }
    }
}