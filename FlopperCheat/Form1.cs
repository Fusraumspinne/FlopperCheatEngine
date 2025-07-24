using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlopperCheat
{
    public partial class Form1 : Form
    {
        private MemoryScanner memoryScanner;

        private List<(nint hWnd, string title, uint processId)> windows;
        private uint processId;

        private static List<nint> foundAddresses = [];
        private List<nint> savedAddresses = [];
        private static nint selectedAddress = 0;

        private int currentPage = 1;
        private const int pageSize = 10;
        private int totalPages = 0;

        private int savedCurrentPage = 1;
        private const int savedPageSize = 5;
        private int savedTotalPages = 0;

        public Form1()
        {
            InitializeComponent();
            memoryScanner = new MemoryScanner();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnPrevPage.Enabled = (currentPage > 1);
            btnNextPage.Enabled = (currentPage < totalPages);

            btnPrevPageSavedAddresses.Enabled = (savedCurrentPage > 1);
            btnNextPageSavedAddresses.Enabled = (savedCurrentPage < savedTotalPages);

            btnNextScan.Enabled = false;

            listAdresses.View = View.Details;
            listAdresses.Columns.Add("Adresse", listAdresses.ClientSize.Width);
            listAdresses.Scrollable = false;

            listSavedAdresses.View = View.Details;
            listSavedAdresses.Columns.Add("Gespeicherte Adresse", listSavedAdresses.ClientSize.Width);
            listSavedAdresses.Scrollable = false;

            LoadProcessesDropDown();
        }

        private void LoadProcessesDropDown()
        {
            comboBoxProcessesList.DropDownStyle = ComboBoxStyle.DropDownList;

            windows = MemoryScanner.GetAllWindows();

            var titles = windows
                .Select(w => w.title)
                .ToList();

            comboBoxProcessesList.DataSource = titles;
        }

        private void comboBoxProcessesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = comboBoxProcessesList.SelectedIndex;
            if (idx < 0 || idx >= windows.Count)
                return;

            var (handle, title, pid) = windows[idx];

            processId = pid;
        }

        private void NewScanMemory(object sender, EventArgs e)
        {
            long desiredValue;
            if (string.IsNullOrWhiteSpace(textBoxScanValue.Text))
            {
                MessageBox.Show("Das Eingabefeld darf nicht leer sein");
                return;
            }
            if (!long.TryParse(textBoxScanValue.Text, out desiredValue))
            {
                MessageBox.Show("Ungültiger Wert im Eingabefeld");
                return;
            }

            foundAddresses = MemoryScanner.MemorySearch(processId, desiredValue);

            currentPage = 1;
            totalPages = (int)Math.Ceiling(foundAddresses.Count / (double)pageSize);

            btnPrevPage.Enabled = (currentPage > 1);
            btnNextPage.Enabled = (currentPage < totalPages);

            btnNextScan.Enabled = foundAddresses.Count > 0;

            DisplayCurrentPage();
            UpdateStatusLabels();
        }

        private void btnAddScanValue_Click(object sender, EventArgs e)
        {
            if (long.TryParse(textBoxScanValue.Text, out long currentValue))
            {
                currentValue++;
                textBoxScanValue.Text = currentValue.ToString();
            }
            else
            {
                MessageBox.Show("Ungültiger Wert im Eingabefeld");
            }
        }

        private void btnSubScanValue_Click(object sender, EventArgs e)
        {
            if (long.TryParse(textBoxScanValue.Text, out long currentValue))
            {
                currentValue--;
                textBoxScanValue.Text = currentValue.ToString();
            }
            else
            {
                MessageBox.Show("Ungültiger Wert im Eingabefeld");
            }
        }

        private void DisplayCurrentPage()
        {
            listAdresses.Items.Clear();

            var pageItems = foundAddresses
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);

            foreach (var addr in pageItems)
            {
                string hex = "0x" + addr.ToInt64().ToString("X8");
                var item = new ListViewItem(hex);
                listAdresses.Items.Add(item);
            }
        }

        private void listAdresses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listAdresses.SelectedItems.Count > 0)
            {
                var selectedItem = listAdresses.SelectedItems[0];
                string hexString = selectedItem.Text.Replace("0x", "");

                if (long.TryParse(hexString, System.Globalization.NumberStyles.HexNumber, null, out long address))
                {
                    selectedAddress = new nint(address);
                }
            }
            else
            {
                selectedAddress = nint.Zero;
            }
        }

        private void UpdateStatusLabels()
        {
            labelFoundAdresses.Text = $"Gefundene Adressen: {foundAddresses.Count}";
            labelPages.Text = $"Seite {currentPage} von {Math.Max(totalPages, 1)}";
        }

        private void GetToNextPage(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                DisplayCurrentPage();
                UpdateStatusLabels();
            }

            btnPrevPage.Enabled = (currentPage > 1);
            btnNextPage.Enabled = (currentPage < totalPages);
        }

        private void GetToPrevPage(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayCurrentPage();
                UpdateStatusLabels();
            }

            btnPrevPage.Enabled = (currentPage > 1);
            btnNextPage.Enabled = (currentPage < totalPages);
        }

        private void NextScanMemory(object sender, EventArgs e)
        {
            if (foundAddresses.Count == 0)
            {
                MessageBox.Show("Keine Adressen zum Filtern vorhanden");
                return;
            }

            if (!long.TryParse(textBoxScanValue.Text, out long newValue))
            {
                MessageBox.Show("Ungültiger Wert im Eingabefeld");
                return;
            }

            foundAddresses = MemoryScanner.FilterPointers(processId, foundAddresses, newValue);

            currentPage = 1;
            totalPages = (int)Math.Ceiling(foundAddresses.Count / (double)pageSize);

            btnPrevPage.Enabled = (currentPage > 1);
            btnNextPage.Enabled = (currentPage < totalPages);
            btnNextScan.Enabled = (foundAddresses.Count > 0);

            DisplayCurrentPage();
            UpdateStatusLabels();
        }

        private void btnWriteValue_Click(object sender, EventArgs e)
        {
            if (selectedAddress == nint.Zero)
            {
                MessageBox.Show("Keine Adresse ausgewählt");
                return;
            }

            if (!long.TryParse(textBoxWriteValue.Text, out long newValue))
            {
                MessageBox.Show("Ungültiger Wert");
                return;
            }

            string result = MemoryScanner.WriteAddressValue(processId, selectedAddress, newValue);
            MessageBox.Show(result);
        }

        private void btnAddWriteValue_Click(object sender, EventArgs e)
        {
            if (long.TryParse(textBoxWriteValue.Text, out long currentValue))
            {
                currentValue++;
                textBoxWriteValue.Text = currentValue.ToString();
            }
            else
            {
                MessageBox.Show("Ungültiger Wert im Eingabefeld");
            }
        }

        private void btnSubWriteValue_Click(object sender, EventArgs e)
        {
            if (long.TryParse(textBoxWriteValue.Text, out long currentValue))
            {
                currentValue--;
                textBoxWriteValue.Text = currentValue.ToString();
            }
            else
            {
                MessageBox.Show("Ungültiger Wert im Eingabefeld");
            }
        }

        private void btnSaveAddress_Click(object sender, MouseEventArgs e)
        {
            if (selectedAddress == nint.Zero)
            {
                MessageBox.Show("Keine Adresse ausgewählt.");
                return;
            }

            if (savedAddresses.Contains(selectedAddress))
            {
                MessageBox.Show("Adresse bereits gespeichert.");
                return;
            }

            savedAddresses.Add(selectedAddress);
            DisplaySavedAddresses();
            UpdateStatusLabelsSavedAddresses();
        }

        private void DisplaySavedAddresses()
        {
            listSavedAdresses.Items.Clear();

            savedTotalPages = (int)Math.Ceiling(savedAddresses.Count / (double)savedPageSize);

            var pageItems = savedAddresses
                .Skip((savedCurrentPage - 1) * savedPageSize)
                .Take(savedPageSize);

            foreach (var addr in pageItems)
            {
                string hex = "0x" + addr.ToInt64().ToString("X8");
                var item = new ListViewItem(hex);
                listSavedAdresses.Items.Add(item);
            }

            btnPrevPageSavedAddresses.Enabled = (savedCurrentPage > 1);
            btnNextPageSavedAddresses.Enabled = (savedCurrentPage < savedTotalPages);
        }

        private void btnSavedPrevPage_Click(object sender, EventArgs e)
        {
            if (savedCurrentPage > 1)
            {
                savedCurrentPage--;
                DisplaySavedAddresses();
                UpdateStatusLabelsSavedAddresses();
            }
        }

        private void btnSavedNextPage_Click(object sender, EventArgs e)
        {
            if (savedCurrentPage < savedTotalPages)
            {
                savedCurrentPage++;
                DisplaySavedAddresses();
                UpdateStatusLabelsSavedAddresses();
            }
        }

        private void UpdateStatusLabelsSavedAddresses()
        {
            labelSavedAddresses.Text = $"Gefundene Adressen: {savedAddresses.Count}";
            labelPagesSavedAddresses.Text = $"Seite {savedCurrentPage} von {Math.Max(savedTotalPages, 1)}";
        }

        private void listSavedAdresses_DoubleClick(object sender, EventArgs e)
        {
            if (listSavedAdresses.SelectedItems.Count > 0)
            {
                string hex = listSavedAdresses.SelectedItems[0].Text.Replace("0x", "");
                if (long.TryParse(hex, System.Globalization.NumberStyles.HexNumber, null, out long addr))
                {
                    nint addressToRemove = new(addr);
                    savedAddresses.Remove(addressToRemove);

                    savedCurrentPage = Math.Min(savedCurrentPage,
                        Math.Max((int)Math.Ceiling(savedAddresses.Count / (double)savedPageSize), 1));

                    DisplaySavedAddresses();
                    UpdateStatusLabelsSavedAddresses();
                }
            }
        }

        private void listSavedAdresses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSavedAdresses.SelectedItems.Count > 0)
            {
                var selectedItem = listSavedAdresses.SelectedItems[0];
                string hexString = selectedItem.Text.Replace("0x", "");

                if (long.TryParse(hexString, System.Globalization.NumberStyles.HexNumber, null, out long address))
                {
                    selectedAddress = new nint(address);
                }
            }
            else
            {
                selectedAddress = nint.Zero;
            }
        }
    }
}