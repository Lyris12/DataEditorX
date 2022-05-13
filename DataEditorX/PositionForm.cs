namespace DataEditorX
{
    public partial class LocationForm : Form
    {
        public LocationForm()
        {
            InitializeComponent();
        }

        private void checkLocationMZone0x4_CheckedChanged(object sender, EventArgs e)
        {
            if (!noCheck)
            {
                CheckOnField();
            }
        }

        private void CheckOnField()
        {
            checkMZoneAndSZone.Checked = checkLocationMZone0x4.Checked && checkLocationSZone0x8.Checked;
        }

        private void checkLocationSZone0x8_CheckedChanged(object sender, EventArgs e)
        {
            if (!noCheck)
            {
                CheckOnField();
            }
        }
        bool noCheck = false;
        private void checkMZoneAndSZone_CheckedChanged(object sender, EventArgs e)
        {
            if (checkMZoneAndSZone.Checked)
            {
                noCheck = true;
                checkLocationMZone0x4.Checked = true;
                checkLocationSZone0x8.Checked = true;
                noCheck = false;
            }
        }
    }
    public class Location
    {
        public int LocationInt;
        enum LocationStrings
        {
            LOCATION_DECK = 0x01,
            LOCATION_HAND = 0x02,
            LOCATION_MZONE = 0x04,
            LOCATION_SZONE = 0x08,
            LOCATION_GRAVE = 0x10,
            LOCATION_REMOVED = 0x20,
            LOCATION_EXTRA = 0x40,
            LOCATION_OVERLAY = 0x80,
            LOCATION_ONFIELD = 0x0c,
            LOCATION_DECKBOT = 0x10001,
            LOCATION_DECKSHF = 0x20001

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
