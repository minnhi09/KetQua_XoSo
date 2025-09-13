using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<RssItem> rssItems;
        private List<LotteryResult> lotteryResults;
        private HttpClient httpClient;

        public Form1()
        {
            InitializeComponent();
            rssItems = new List<RssItem>();
            lotteryResults = new List<LotteryResult>();
            httpClient = new HttpClient();
            
            // Thiết lập ComboBox với các RSS sources
            SetupRssComboBox();
            
            // Thiết lập DataGridView
            SetupDataGridView();
        }
        
        private void SetupRssComboBox()
        {
            var rssSources = new List<RssSource>
            {
                new RssSource("XS An Giang", "https://xskt.com.vn/rss-feed/an-giang-xsag.rss")
            };
            
            cboRssUrl.DisplayMember = "DisplayName";
            cboRssUrl.ValueMember = "Url";
            cboRssUrl.DataSource = rssSources;
            cboRssUrl.SelectedIndex = 0; // Chọn mặc định Miền Nam
        }

        private void SetupDataGridView()
        {
            dataGridViewRss.AutoGenerateColumns = false;
            dataGridViewRss.AllowUserToAddRows = false;
            dataGridViewRss.ReadOnly = true;
            dataGridViewRss.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Thêm các cột cho kết quả xổ số chi tiết
            dataGridViewRss.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Date",
                HeaderText = "Ngày",
                DataPropertyName = "Date",
                Width = 100
            });

            dataGridViewRss.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Prize",
                HeaderText = "Giải thưởng",
                DataPropertyName = "Prize",
                Width = 100
            });

            dataGridViewRss.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Result",
                HeaderText = "Kết quả",
                DataPropertyName = "Result",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridViewRss.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Province",
                HeaderText = "Tỉnh/Thành",
                DataPropertyName = "Province",
                Width = 120
            });
        }

        private async void btnLoadRss_Click(object sender, EventArgs e)
        {
            if (cboRssUrl.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn nguồn RSS!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            RssSource selectedSource = (RssSource)cboRssUrl.SelectedItem;
            string url = selectedSource.Url;

            try
            {
                btnLoadRss.Enabled = false;
                lblStatus.Text = "Đang tải dữ liệu RSS...";
                lblStatus.ForeColor = Color.Blue;

                await LoadRssDataAsync(url);

                lblStatus.Text = $"Đã tải thành công {rssItems.Count} mục RSS";
                lblStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Lỗi: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show($"Có lỗi xảy ra khi tải RSS:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLoadRss.Enabled = true;
            }
        }

        private async Task LoadRssDataAsync(string url)
        {
            // Tải nội dung RSS
            string rssContent = await httpClient.GetStringAsync(url);

            // Parse XML
            XDocument doc = XDocument.Parse(rssContent);

            // Clear danh sách cũ
            rssItems.Clear();

            // Lấy tất cả item từ RSS
            var items = doc.Descendants("item");

            foreach (var item in items)
            {
                string title = item.Element("title")?.Value ?? "";
                string description = item.Element("description")?.Value ?? "";
                string link = item.Element("link")?.Value ?? "";
                string pubDate = item.Element("pubDate")?.Value ?? "";

                // Làm sạch description (loại bỏ ký tự xuống dòng thừa)
                description = CleanDescription(description);

                rssItems.Add(new RssItem(title, description, link, pubDate));
            }

            // Populate ComboBox với danh sách ngày
            cboDate.DisplayMember = "Title";
            cboDate.ValueMember = "Title";
            cboDate.DataSource = null;
            cboDate.DataSource = rssItems;
            
            // Nếu có dữ liệu, chọn item đầu tiên và hiển thị
            if (rssItems.Count > 0)
            {
                cboDate.SelectedIndex = 0;
                // Event handler sẽ tự động được gọi để hiển thị dữ liệu
            }
        }

        private string CleanDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                return description;

            // Thay thế multiple spaces và newlines bằng space đơn
            description = System.Text.RegularExpressions.Regex.Replace(description, @"\s+", " ");
            
            // Trim
            return description.Trim();
        }

        private List<LotteryResult> ParseLotteryResults(RssItem rssItem)
        {
            var results = new List<LotteryResult>();
            
            // Trích xuất ngày từ title
            string date = ExtractDateFromTitle(rssItem.Title);
            
            // Trích xuất tỉnh/thành từ title (nếu có)
            string province = ExtractProvinceFromTitle(rssItem.Title);
            
            // Parse description theo pattern "X: Y"
            string cleanDesc = CleanDescription(rssItem.Description);
            
            // Tìm các pattern giải thưởng
            var matches = System.Text.RegularExpressions.Regex.Matches(cleanDesc, @"(\w+):\s*([0-9\s\-]+)");
            
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                string prize = match.Groups[1].Value;
                string result = match.Groups[2].Value.Trim();
                
                // Xử lý kết quả có nhiều số (cách nhau bởi dấu -)
                var numbers = result.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                
                foreach (var number in numbers)
                {
                    results.Add(new LotteryResult(date, prize, number.Trim(), province));
                }
            }
            
            return results;
        }
        
        private string ExtractDateFromTitle(string title)
        {
            // Tìm pattern "NGÀY DD/MM" trong title
            var match = System.Text.RegularExpressions.Regex.Match(title, @"NGÀY\s+(\d{1,2}/\d{1,2})");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return "";
        }
        
        private string ExtractProvinceFromTitle(string title)
        {
            // Tìm tên tỉnh/thành trong title
            var match = System.Text.RegularExpressions.Regex.Match(title, @"XỔ SỐ\s+([A-ZẮẰẲẴẶẤẦẨẪẬÁÀẢÃẠÊỀỂỄỆÉÈẺẼẸÔỒỐỔỖỘƠỜỞỠỢÓÒỎÕỌƯỪỨỬỮỰ]+(?:\s+[A-ZẮẰẲẴẶẤẦẨẪẬÁÀẢÃẠÊỀỂỄỆÉÈẺẼẸÔỒỐỔỖỘƠỜỞỠỢÓÒỎÕỌƯỪỨỬỮỰ]+)*)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return "";
        }

        private void cboDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDate.SelectedItem == null) return;
            
            RssItem selectedItem = (RssItem)cboDate.SelectedItem;
            
            // Parse kết quả xổ số từ RSS item được chọn
            var results = ParseLotteryResults(selectedItem);
            
            // Hiển thị lên DataGridView
            dataGridViewRss.DataSource = null;
            dataGridViewRss.DataSource = results;
            
            // Tự động điều chỉnh chiều cao hàng
            dataGridViewRss.AutoResizeRows();
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
