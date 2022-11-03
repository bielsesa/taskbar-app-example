namespace PruebaTaskBarIcon
{
    public partial class Form1 : Form
    {
        internal enum Vegetables
        {
            Potato = 0,
            Chili = 1,
            Carrot = 2,
            Asparagus = 3,
            Mushroom = 4,
            Garlic = 5
        }

        private Random _random;
        private ContextMenuStrip notifyIconMenuStrip;

        public Form1()
        {
            _random = new Random();

            InitializeComponent();

            // Context Menu Strip
            notifyIconMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip = notifyIconMenuStrip;
            NotifyIconContextMenuInitialization();
            //notifyIconMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(cms_Opening);

            backgroundWorker.RunWorkerAsync();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private async void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // Create a timer that fires every 5 seconds
            using (var timer = new PeriodicTimer(TimeSpan.FromSeconds(10)))
            {
                // Wire it to fire an event after the specified period
                while (await timer.WaitForNextTickAsync())
                {
                    // Change notifyicon ico file
                    ChangeIcoFileRandom();
                    if (progressBar.Value == progressBar.Maximum)
                    {
                        this.Invoke(new MethodInvoker(delegate {
                            // Execute the following code on the GUI thread.
                            progressBar.Value = 0;
                        }));
                        
                    } 
                    else
                    {
                        this.Invoke(new MethodInvoker(delegate {
                            // Execute the following code on the GUI thread.
                            progressBar.Value += 5;
                        }));
                    }
                }
            }
        }

        private void ChangeIcoFileRandom()
        {
            int ico = _random.Next(6);

            switch (ico)
            {
                case (int)Vegetables.Potato:
                    notifyIcon.Icon = new Icon(Path.Combine(Environment.CurrentDirectory, @"res\veggies\potato.ico"));
                    break;

                case (int)Vegetables.Chili:
                    notifyIcon.Icon = new Icon(Path.Combine(Environment.CurrentDirectory, @"res\veggies\green-chili-pepper.ico"));
                    break;

                case (int)Vegetables.Carrot:
                    notifyIcon.Icon = new Icon(Path.Combine(Environment.CurrentDirectory, @"res\veggies\carrot.ico"));
                    break;

                case (int)Vegetables.Asparagus:
                    notifyIcon.Icon = new Icon(Path.Combine(Environment.CurrentDirectory, @"res\veggies\asparagus.ico"));
                    break;

                case (int)Vegetables.Mushroom:
                    notifyIcon.Icon = new Icon(Path.Combine(Environment.CurrentDirectory, @"res\veggies\champaignon.ico"));
                    break;

                case (int)Vegetables.Garlic:
                    notifyIcon.Icon = new Icon(Path.Combine(Environment.CurrentDirectory, @"res\veggies\garlic.ico"));
                    break;

                default:
                    break;
            }
        }

        private void NotifyIconContextMenuInitialization()
        {
            // Clear the ContextMenuStrip control's Items collection.
            notifyIconMenuStrip.Items.Clear();

            // Populate the ContextMenuStrip control with its default items.
            notifyIconMenuStrip.Items.Add("-");
            notifyIconMenuStrip.Items.Add("Potato", new Bitmap(GetVeggieIcon("potato")));
            notifyIconMenuStrip.Items.Add("Chili", new Bitmap(GetVeggieIcon("green-chili-pepper")));
            notifyIconMenuStrip.Items.Add("-");
            notifyIconMenuStrip.Items.Add("Carrot", new Bitmap(GetVeggieIcon("carrot")));
            notifyIconMenuStrip.Items.Add("Asparagus", new Bitmap(GetVeggieIcon("asparagus")));
            notifyIconMenuStrip.Items.Add("Mushroom", new Bitmap(GetVeggieIcon("champignon")));
            notifyIconMenuStrip.Items.Add("Garlic", new Bitmap(GetVeggieIcon("garlic")));

            notifyIconMenuStrip.ItemClicked += NotifyIconMenuStrip_ItemClicked;
        }

        private void NotifyIconMenuStrip_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Potato":
                    notifyIcon.Icon = new Icon(GetVeggieIcon("potato"));
                    break;

                case "Chili":
                    notifyIcon.Icon = new Icon(GetVeggieIcon("green-chili-pepper"));
                    break;

                case "Carrot":
                    notifyIcon.Icon = new Icon(GetVeggieIcon("carrot"));
                    break;

                case "Asparagus":
                    notifyIcon.Icon = new Icon(GetVeggieIcon("asparagus"));
                    break;

                case "Mushroom":
                    notifyIcon.Icon = new Icon(GetVeggieIcon("champignon"));
                    break;

                case "Garlic":
                    notifyIcon.Icon = new Icon(GetVeggieIcon("garlic"));
                    break;

                default:
                    break;
            }
        }

        private string GetVeggieIcon(string veggie)
        {
            return Path.Combine(Environment.CurrentDirectory, @$"res\veggies\{veggie}.ico");
        }

        //private void cms_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    // Acquire references to the owning control and item.
        //    Control c = notifyIconMenuStrip.SourceControl as Control;

        //    // Clear the ContextMenuStrip control's Items collection.
        //    notifyIconMenuStrip.Items.Clear();

        //    // Populate the ContextMenuStrip control with its default items.
        //    notifyIconMenuStrip.Items.Add("-");
        //    notifyIconMenuStrip.Items.Add("Potato");
        //    notifyIconMenuStrip.Items.Add("Chili Pepper");
        //    notifyIconMenuStrip.Items.Add("-");
        //    notifyIconMenuStrip.Items.Add("Carrot");
        //    notifyIconMenuStrip.Items.Add("Asparagus");
        //    notifyIconMenuStrip.Items.Add("Mushroom");
        //    notifyIconMenuStrip.Items.Add("Garlic");

        //    // Set Cancel to false. 
        //    // It is optimized to true based on empty entry.
        //    e.Cancel = false;
        //}
    }
}