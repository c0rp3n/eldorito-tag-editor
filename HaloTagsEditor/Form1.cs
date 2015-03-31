using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaloTagsEditor
{
    public partial class MainForm : Form
    {
        public Dictionary<string, string> weapons, projectiles, grenade;
        public Stream tagsFile;
        public string fileName;
        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tagsFile = null;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = false;
            comboBox3.Enabled = true;
            OpenFileDialog openTagsDialog = new OpenFileDialog();
            openTagsDialog.Filter = "Dat Files (.dat)|*.dat|All Files (*.*)|*.*";
            openTagsDialog.FilterIndex = 1;
            if(openTagsDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileName = openTagsDialog.FileName;
                    if ((tagsFile = openTagsDialog.OpenFile()) != null)
                    {
                        using (tagsFile)
                        {
                            byte[] startingWeapon = new byte[4];   
                            byte[] barrierCheck = new byte[4];
                            byte[] grenadeItem = new byte[4];
                            // Get Deathless Player Status
                            tagsFile.Position = 0x1F952F0;
                            if (tagsFile.ReadByte().ToString() == "131")
                            {
                                checkBox1.Checked = true;
                            }
                            else
                            {
                                checkBox1.Checked = false;
                            }
                            // Get Remove Barriers Status
                            tagsFile.Position = 0x3D4FA3C;
                            tagsFile.Read(barrierCheck, 0, 4);
                            string barrierHex= BitConverter.ToString(barrierCheck).Replace("-","");
                            if(barrierHex == "FFFFFFFF")
                            {
                                checkBox2.Checked = true;
                            }
                            else
                            {
                                checkBox2.Checked = false;
                            }
                            // Get Starting Weapon Status
                            tagsFile.Position = 0x1214880;
                            tagsFile.Read(startingWeapon, 0, 4);
                            string weapHex = BitConverter.ToString(startingWeapon).Replace("-", "");
                            foreach (KeyValuePair<string, string> weap in weapons)
                            {
                                if (weap.Value == weapHex)
                                {
                                    for (int i = 0; i < comboBox1.Items.Count; i++)
                                    {
                                        if (weap.Key == comboBox1.GetItemText(comboBox1.Items[i]))
                                        {
                                            comboBox1.SelectedIndex = i;
                                        }
                                    }
                                }
                            }
                            // Get Get Grenade Status
                            tagsFile.Position = 0x10DCD1C;
                            tagsFile.Read(grenadeItem, 0, 4);
                            string projHex = BitConverter.ToString(grenadeItem).Replace("-","");
                            foreach (KeyValuePair<string, string> proj in grenade)
                            {
                                if (proj.Value == projHex)
                                {
                                    for (int o = 0; o < comboBox3.Items.Count; o++)
                                    {
                                        if (proj.Key == comboBox3.GetItemText(comboBox3.Items[o]))
                                        {
                                            comboBox3.SelectedIndex = o;
                                        }
                                    }
                                }
                            }
                            tagsFile.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            weapons = new Dictionary<string, string> {
                {"Spiker", "00150000"},
                {"Beam Rifle", "09150000"},
                {"Gravity Hammer", "0C150000"},
                {"Plasma Turret", "0e150000"},
                {"Assault Rifle (Default)", "1e150000"},
                {"Assault Rifle (Red)", "81150000"},
                {"Assault Rifle (Yellow)", "82150000"},
                {"Assault Rifle (Green)", "83150000"},
                {"Assault Rifle (Gold)", "84150000"},
                {"SMG (Default)", "7D150000"},
                {"SMG (Red)", "8E150000"},
                {"SMG (Yellow)", "8C150000"},
                {"SMG (Green)", "8D150000"},
                {"SMG (Gold)", "8F150000"},
                {"Battle Rifle (Default)", "7C150000"},
                {"Battle Rifle (Red)", "86150000"},
                {"Battle Rifle (Yellow)", "89150000"},
                {"Battle Rifle (Green)", "88150000"},
                {"Battle Rifle (Blue)", "85150000"},
                {"Battle Rifle (Purple)", "BF150000"},
                {"Battle Rifle (Gold)", "87150000"},
                {"DMR (Default)", "80150000"},
                {"DMR (Red)", "8A150000"},
                {"DMR (Gold)", "8B150000"},
                {"DMR (Yellow)", "8C150000"},
                {"DMR (Green)", "8D150000"},
                {"DMR (Blue)", "BE150000"},
                {"DMR (Purple)", "BF150000"},
                {"Magnum (Default)", "7E150000"},
                {"Magnum (Red)", "93150000"},
                {"Magnum (Gold)", "94150000"},
                {"Carbine (Default)", "FE140000"},
                {"Carbine (Gold)", "91150000"},
                {"Carbine (Yellow)", "C0150000"},
                {"Carbine (Blue)", "C1150000"},
                {"Carbine (purple)", "C2150000"},
                {"Carbine (red)", "C3150000"},
                {"Carbine (green)", "C4150000"},
                {"Mauler (Default)", "04150000"},
                {"Mauler (Gold)", "92150000"},
                {"Energy Sword", "9E150000"},
                {"Useless Energy Sword", "7F150000"},
                {"Plasma Rifle", "25150000"},
                {"Plasma Pistol", "F7140000"},
                {"Plasma Pistol (Gold)", "95150000"},
                {"Flag", "A2150000"},
                {"Skull", "A3150000"},
                {"Bomb", "A4150000"},
                {"Sniper Rifle", "B1150000"},
                {"Rocket Launcher", "B3150000"},
                {"Shotgun", "85150000"},
                {"Spartan Laser", "B2150000"},
                {"Sentinel Beam", "561A0000"},
                {"Needler", "F8140000"},
                {"Fuel Rod", "F9140000"},
                {"Brute Shot", "FF140000"}
            };
            projectiles = new Dictionary<string, string> {
                {"Frag Grenade", "AD010000"},
                {"Plasma Grenade", "AD010000"},
                {"Spike Grenade", "AD010000"},
                {"Incidiary Grenade", "AD010000"},
                {"Spike Round (Spike Grenade)", "0000"},
                {"Rocket", "C9150000"},
                {"Missile Pod Rocket", "CA150000"},
                {"Wraith Shot", "CB150000"},
                {"Brute Shot Round", "CD150000"},
                {"Hornet Rocket", "CE150000"},
                {"Assault Rifle Round", "901B0000"},
                {"Battle Rifle Round", "881D0000"},
                {"Carbine Round", "BC150000"},
                {"Shoutgun Pellet", "001F0000"},
                {"Sniper Round", "921F0000"},
                {"Spiker Round", "58200000"},
                {"Mauler Round", "8F200000"},
                {"Needler Round", "EF200000"},
                {"Plasma Pistol", "5C220000"},
                {"Plasma Pistol (Charged)", "5E220000"}
            };
            grenade = new Dictionary<string, string> {
                {"Frag Grenade", "AD010000"},
                {"Plasma Grenade", "B0010000"},
                {"Spike Grenade", "B3010000"},
                {"Incidiary Grenade", "B6010000"},
                {"Banshee", "1A150000"},
                {"Ghost", "17150000"},
                {"Mongoose", "96150000"},
                {"Chopper", "18150000"},
                {"Missle Pod (Mounted)", "183A0000"},
                {"Machine Gun Turret (Mounted)", "4C280000"},
                {"Plasma Cannon (Mounted)", "FB140000"},
                {"Shade", "16150000"},
                {"Auto Turret", "C5150000"},
                {"Scorpion", "20150000"},
                {"Scorpion Cannon", "C6280000"},
                {"Scorpion Machine Gun", "C7280000"},
                {"Warthog", "1F150000"},
                {"Warthog Chaingun", "EC290000"},
                {"Warthog Gauss Rifle", "ED290000"},
                {"Warthog (Troop ?)", "EE290000"},
                {"Warthog (Snow)", "99150000"},
                {"Warthog Chaingun (Snow)", "3A430000"},
                {"Warthog Gauss Rifle(Snow)", "3B430000"},
                {"Hornet", "98150000"},
                {"Hornet Lite", "9B150000"},
                {"Hornet (Main Menu)", "D8270000"},
                {"Wraith", "19150000"},
                {"Wraith (Mortar)", "42370000"},
                {"Wraith (Anti Infantry)", "43370000"},
                {"Wraith (Anti Air)", "44370000"},
                {"Wraith (Anti Infantry + Air)", "45370000"},
                {"Pelican Rocket Pod", "44A10000"},
                {"Pelican Rocket Pod #2", "4B280000"},
                {"Headless Spartan", "DD010000"},
                {"Headless Spartan Neutral", "130C0000"},
                {"Spartan Yellow and Grey", "48150000"},
                {"Spartan Double Eyed Grey", "39150000"},
                {"Spartan Narrow Visor", "3D150000"},
                {"Monitor", "A0150000"},
                {"Headless Spartan, less animation", "E3010000"},
                {"Headless Spartan, walks then respawns", "E7010000"},
                {"Headless Spartan, hologram", "130C0000"}
            };
            foreach(KeyValuePair<string,string> proj in projectiles)
            {
                comboBox2.Items.Add(proj.Key);
            }
            foreach(KeyValuePair<string,string> weap in weapons)
            {
                comboBox1.Items.Add(weap.Key);
            }
            foreach (KeyValuePair<string, string> proj in grenade)
            {
                comboBox3.Items.Add(proj.Key);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tagsFile = null;
            tagsFile = File.OpenWrite(fileName);
            byte[] deathlessPlayer = new byte[1];
            byte[] barrierCheck = new byte[4];
            byte[] startingWeap = new byte[4];
            byte[] grenadeItem = new byte[4];
            // Set Deathless Player
            tagsFile.Position = 0x1F952F0;
            if (checkBox1.Checked == true)
            {
                deathlessPlayer = StringToByteArray("83");
            }
            else
            {
                deathlessPlayer = StringToByteArray("03");
            }
            tagsFile.Write(deathlessPlayer, 0, 1);
            // Set Remove Barriers

            if (checkBox2.Checked == true)
            {
                barrierCheck = StringToByteArray("FFFFFFFF");
                tagsFile.Position = 0x3D4FA3C;
                tagsFile.Write(barrierCheck, 0, 4);
                tagsFile.Position = 0x447006C;
                tagsFile.Write(barrierCheck, 0, 4);
                tagsFile.Position = 0x4C0BB2C;
                tagsFile.Write(barrierCheck, 0, 4);
                tagsFile.Position = 0x513C20C;
                tagsFile.Write(barrierCheck, 0, 4);
                tagsFile.Position = 0x556E8EC;
                tagsFile.Write(barrierCheck, 0, 4);
                tagsFile.Position = 0x59D288C;
                tagsFile.Write(barrierCheck, 0, 4);
            }
            tagsFile.Position = 0x1214880;
            startingWeap = StringToByteArray("87150000");
            foreach (KeyValuePair<string, string> weap in weapons)
            {
                if (weap.Key == comboBox1.SelectedItem)
                {
                    startingWeap = StringToByteArray(weap.Value);
                }
            }
            tagsFile.Write(startingWeap, 0, 4);
            tagsFile.Position = 0x10DCD1C;
            foreach (KeyValuePair<string, string> proj in grenade)
            {
                if (proj.Key == comboBox3.SelectedText)
                {
                    grenadeItem = StringToByteArray(proj.Value);
                }
            }
            foreach (KeyValuePair<string, string> vehi in grenade)
            {
                if (vehi.Key == comboBox3.SelectedItem)
                {
                    grenadeItem = StringToByteArray(vehi.Value);
                }
            }
            tagsFile.Write(grenadeItem, 0, 4);
            tagsFile.Dispose();
            MessageBox.Show("Save Completed!");
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void automaticExePatcherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExePatcherForm patch = new ExePatcherForm();
            patch.Show();
        }
    }

    public class tagsSettings
    {
        private bool deathlessPlayer;
        private bool barriersRemoved;

        public tagsSettings()
        {

        }

        public void setDeathlessPlayer(bool value)
        {
            this.deathlessPlayer = value;
        }

        public void setBarriersRemoved(bool value)
        {
            this.barriersRemoved = value;
        }

        public bool getDeathlessPlayer()
        {
            return this.deathlessPlayer;
        }

        public bool getBarriersRemoved()
        {
            return this.barriersRemoved;
        }
    }

}
