using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private List<RssSource> allRssSources;
        private List<RssSource> filteredRssSources;

        public Form1()
        {
            InitializeComponent();
            rssItems = new List<RssItem>();
            lotteryResults = new List<LotteryResult>();
            httpClient = new HttpClient();
            
            // Thiết lập các danh sách RSS sources
            SetupRssSourcesData();
            
            // Thiết lập ComboBox với các RSS sources
            SetupRssComboBox();
            
            // Thiết lập DataGridView
            SetupDataGridView();
        }
        
        private void SetupRssSourcesData()
        {
            try
            {
                // Thử đọc từ file XML trước
                LoadRssSourcesFromXml();
            }
            catch
            {
                // Nếu lỗi, sử dụng dữ liệu hardcode
                LoadRssSourcesHardcode();
            }
        }
        
        private void LoadRssSourcesFromXml()
        {
            string xmlPath = Path.Combine(Application.StartupPath, "rss-sources.xml");
            if (!File.Exists(xmlPath))
            {
                LoadRssSourcesHardcode();
                return;
            }
            
            allRssSources = new List<RssSource>();
            XDocument doc = XDocument.Load(xmlPath);
            
            foreach (var regionElement in doc.Descendants("Region"))
            {
                string regionName = regionElement.Attribute("name")?.Value ?? "";
                
                foreach (var sourceElement in regionElement.Descendants("RssSource"))
                {
                    string displayName = sourceElement.Attribute("displayName")?.Value ?? "";
                    string province = sourceElement.Attribute("province")?.Value ?? "";
                    string url = sourceElement.Attribute("url")?.Value ?? "";
                    
                    allRssSources.Add(new RssSource($"[{regionName}] {displayName}", url, regionName, province));
                }
            }
        }
        
        private void LoadRssSourcesHardcode()
        {
            allRssSources = new List<RssSource>
            {
                // Miền Nam
                new RssSource("[Miền Nam] XS An Giang", "https://xskt.com.vn/rss-feed/an-giang-xsag.rss", "Miền Nam", "An Giang"),
                new RssSource("[Miền Nam] XS Bạc Liêu", "https://xskt.com.vn/rss-feed/bac-lieu-xsbl.rss", "Miền Nam", "Bạc Liêu"),
                new RssSource("[Miền Nam] XS Bến Tre", "https://xskt.com.vn/rss-feed/ben-tre-xsbt.rss", "Miền Nam", "Bến Tre"),
                new RssSource("[Miền Nam] XS Cà Mau", "https://xskt.com.vn/rss-feed/ca-mau-xscm.rss", "Miền Nam", "Cà Mau"),
                new RssSource("[Miền Nam] XS Cần Thơ", "https://xskt.com.vn/rss-feed/can-tho-xsct.rss", "Miền Nam", "Cần Thơ"),
                new RssSource("[Miền Nam] XS Đồng Tháp", "https://xskt.com.vn/rss-feed/dong-thap-xsdt.rss", "Miền Nam", "Đồng Tháp"),
                new RssSource("[Miền Nam] XS Hậu Giang", "https://xskt.com.vn/rss-feed/hau-giang-xshg.rss", "Miền Nam", "Hậu Giang"),
                new RssSource("[Miền Nam] XS Kiên Giang", "https://xskt.com.vn/rss-feed/kien-giang-xskg.rss", "Miền Nam", "Kiên Giang"),
                new RssSource("[Miền Nam] XS Long An", "https://xskt.com.vn/rss-feed/long-an-xsla.rss", "Miền Nam", "Long An"),
                new RssSource("[Miền Nam] XS Sóc Trăng", "https://xskt.com.vn/rss-feed/soc-trang-xsst.rss", "Miền Nam", "Sóc Trăng"),
                new RssSource("[Miền Nam] XS Tây Ninh", "https://xskt.com.vn/rss-feed/tay-ninh-xstn.rss", "Miền Nam", "Tây Ninh"),
                new RssSource("[Miền Nam] XS Tiền Giang", "https://xskt.com.vn/rss-feed/tien-giang-xstg.rss", "Miền Nam", "Tiền Giang"),
                new RssSource("[Miền Nam] XS Trà Vinh", "https://xskt.com.vn/rss-feed/tra-vinh-xstv.rss", "Miền Nam", "Trà Vinh"),
                new RssSource("[Miền Nam] XS Vĩnh Long", "https://xskt.com.vn/rss-feed/vinh-long-xsvl.rss", "Miền Nam", "Vĩnh Long"),
                new RssSource("[Miền Nam] XS TP.HCM", "https://xskt.com.vn/rss-feed/tp-hcm-xshcm.rss", "Miền Nam", "TP.HCM"),
                new RssSource("[Miền Nam] XS Đồng Nai", "https://xskt.com.vn/rss-feed/dong-nai-xsdn.rss", "Miền Nam", "Đồng Nai"),
                new RssSource("[Miền Nam] XS Bình Dương", "https://xskt.com.vn/rss-feed/binh-duong-xsbd.rss", "Miền Nam", "Bình Dương"),
                new RssSource("[Miền Nam] XS Vũng Tàu", "https://xskt.com.vn/rss-feed/vung-tau-xsvt.rss", "Miền Nam", "Vũng Tàu"),
                
                // Miền Trung
                new RssSource("[Miền Trung] XS Đà Nẵng", "https://xskt.com.vn/rss-feed/da-nang-xsdng.rss", "Miền Trung", "Đà Nẵng"),
                new RssSource("[Miền Trung] XS Khánh Hòa", "https://xskt.com.vn/rss-feed/khanh-hoa-xskh.rss", "Miền Trung", "Khánh Hòa"),
                new RssSource("[Miền Trung] XS Phú Yên", "https://xskt.com.vn/rss-feed/phu-yen-xspy.rss", "Miền Trung", "Phú Yên"),
                new RssSource("[Miền Trung] XS Đắk Lắk", "https://xskt.com.vn/rss-feed/dac-lac-xsdl.rss", "Miền Trung", "Đắk Lắk"),
                new RssSource("[Miền Trung] XS Quảng Nam", "https://xskt.com.vn/rss-feed/quang-nam-xsqnm.rss", "Miền Trung", "Quảng Nam"),
                new RssSource("[Miền Trung] XS Quảng Ngãi", "https://xskt.com.vn/rss-feed/quang-ngai-xsqng.rss", "Miền Trung", "Quảng Ngãi"),
                new RssSource("[Miền Trung] XS Bình Định", "https://xskt.com.vn/rss-feed/binh-dinh-xsbdi.rss", "Miền Trung", "Bình Định"),
                new RssSource("[Miền Trung] XS Quảng Bình", "https://xskt.com.vn/rss-feed/quang-binh-xsqb.rss", "Miền Trung", "Quảng Bình"),
                new RssSource("[Miền Trung] XS Quảng Trị", "https://xskt.com.vn/rss-feed/quang-tri-xsqt.rss", "Miền Trung", "Quảng Trị"),
                new RssSource("[Miền Trung] XS Thừa T.Huế", "https://xskt.com.vn/rss-feed/thua-thien-hue-xstth.rss", "Miền Trung", "Thừa Thiên Huế"),
                new RssSource("[Miền Trung] XS Đắk Nông", "https://xskt.com.vn/rss-feed/dac-nong-xsdn.rss", "Miền Trung", "Đắk Nông"),
                new RssSource("[Miền Trung] XS Lâm Đồng", "https://xskt.com.vn/rss-feed/lam-dong-xsld.rss", "Miền Trung", "Lâm Đồng"),
                new RssSource("[Miền Trung] XS Bình Thuận", "https://xskt.com.vn/rss-feed/binh-thuan-xsbth.rss", "Miền Trung", "Bình Thuận"),
                new RssSource("[Miền Trung] XS Ninh Thuận", "https://xskt.com.vn/rss-feed/ninh-thuan-xsnt.rss", "Miền Trung", "Ninh Thuận"),
                new RssSource("[Miền Trung] XS Gia Lai", "https://xskt.com.vn/rss-feed/gia-lai-xsgl.rss", "Miền Trung", "Gia Lai"),
                new RssSource("[Miền Trung] XS Kon Tum", "https://xskt.com.vn/rss-feed/kon-tum-xskt.rss", "Miền Trung", "Kon Tum"),
                
                // Miền Bắc
                new RssSource("[Miền Bắc] XS Hà Nội", "https://xskt.com.vn/rss-feed/ha-noi-xshn.rss", "Miền Bắc", "Hà Nội"),
                new RssSource("[Miền Bắc] XS Quảng Ninh", "https://xskt.com.vn/rss-feed/quang-ninh-xsqn.rss", "Miền Bắc", "Quảng Ninh"),
                new RssSource("[Miền Bắc] XS Bắc Ninh", "https://xskt.com.vn/rss-feed/bac-ninh-xsbn.rss", "Miền Bắc", "Bắc Ninh"),
                new RssSource("[Miền Bắc] XS Hải Phòng", "https://xskt.com.vn/rss-feed/hai-phong-xshp.rss", "Miền Bắc", "Hải Phòng"),
                new RssSource("[Miền Bắc] XS Nam Định", "https://xskt.com.vn/rss-feed/nam-dinh-xsnd.rss", "Miền Bắc", "Nam Định"),
                new RssSource("[Miền Bắc] XS Thái Bình", "https://xskt.com.vn/rss-feed/thai-binh-xstb.rss", "Miền Bắc", "Thái Bình"),
                new RssSource("[Miền Bắc] XS Vĩnh Phúc", "https://xskt.com.vn/rss-feed/vinh-phuc-xsvp.rss", "Miền Bắc", "Vĩnh Phúc"),
                new RssSource("[Miền Bắc] XS Hà Nam", "https://xskt.com.vn/rss-feed/ha-nam-xshnm.rss", "Miền Bắc", "Hà Nam"),
                new RssSource("[Miền Bắc] XS Hưng Yên", "https://xskt.com.vn/rss-feed/hung-yen-xshy.rss", "Miền Bắc", "Hưng Yên"),
                new RssSource("[Miền Bắc] XS Ninh Bình", "https://xskt.com.vn/rss-feed/ninh-binh-xsnb.rss", "Miền Bắc", "Ninh Bình"),
                new RssSource("[Miền Bắc] XS Thanh Hóa", "https://xskt.com.vn/rss-feed/thanh-hoa-xsth.rss", "Miền Bắc", "Thanh Hóa"),
                new RssSource("[Miền Bắc] XS Nghệ An", "https://xskt.com.vn/rss-feed/nghe-an-xsna.rss", "Miền Bắc", "Nghệ An"),
                new RssSource("[Miền Bắc] XS Hà Tĩnh", "https://xskt.com.vn/rss-feed/ha-tinh-xsht.rss", "Miền Bắc", "Hà Tĩnh"),
                new RssSource("[Miền Bắc] XS Hòa Bình", "https://xskt.com.vn/rss-feed/hoa-binh-xshb.rss", "Miền Bắc", "Hòa Bình"),
                new RssSource("[Miền Bắc] XS Sơn La", "https://xskt.com.vn/rss-feed/son-la-xssl.rss", "Miền Bắc", "Sơn La"),
                new RssSource("[Miền Bắc] XS Điện Biên", "https://xskt.com.vn/rss-feed/dien-bien-xsdb.rss", "Miền Bắc", "Điện Biên"),
                new RssSource("[Miền Bắc] XS Lai Châu", "https://xskt.com.vn/rss-feed/lai-chau-xslc.rss", "Miền Bắc", "Lai Châu"),
                new RssSource("[Miền Bắc] XS Lào Cai", "https://xskt.com.vn/rss-feed/lao-cai-xslca.rss", "Miền Bắc", "Lào Cai"),
                new RssSource("[Miền Bắc] XS Yên Bái", "https://xskt.com.vn/rss-feed/yen-bai-xsyb.rss", "Miền Bắc", "Yên Bái"),
                new RssSource("[Miền Bắc] XS Tuyên Quang", "https://xskt.com.vn/rss-feed/tuyen-quang-xstq.rss", "Miền Bắc", "Tuyên Quang"),
                new RssSource("[Miền Bắc] XS Hà Giang", "https://xskt.com.vn/rss-feed/ha-giang-xshg.rss", "Miền Bắc", "Hà Giang"),
                new RssSource("[Miền Bắc] XS Cao Bằng", "https://xskt.com.vn/rss-feed/cao-bang-xscb.rss", "Miền Bắc", "Cao Bằng"),
                new RssSource("[Miền Bắc] XS Bắc Kạn", "https://xskt.com.vn/rss-feed/bac-kan-xsbk.rss", "Miền Bắc", "Bắc Kạn"),
                new RssSource("[Miền Bắc] XS Lạng Sơn", "https://xskt.com.vn/rss-feed/lang-son-xsls.rss", "Miền Bắc", "Lạng Sơn"),
                new RssSource("[Miền Bắc] XS Thái Nguyên", "https://xskt.com.vn/rss-feed/thai-nguyen-xstn.rss", "Miền Bắc", "Thái Nguyên"),
                new RssSource("[Miền Bắc] XS Phú Thọ", "https://xskt.com.vn/rss-feed/phu-tho-xspt.rss", "Miền Bắc", "Phú Thọ"),
                new RssSource("[Miền Bắc] XS Bắc Giang", "https://xskt.com.vn/rss-feed/bac-giang-xsbg.rss", "Miền Bắc", "Bắc Giang")
            };
            
            filteredRssSources = allRssSources;
        }
        
        private void SetupRssComboBox()
        {
            cboRssUrl.DisplayMember = "DisplayName";
            cboRssUrl.ValueMember = "Url";
            cboRssUrl.DataSource = filteredRssSources;
            if (filteredRssSources.Count > 0)
            {
                cboRssUrl.SelectedIndex = 0; // Chọn mặc định
            }
        }

        private void SetupDataGridView()
        {
            dataGridViewRss.AutoGenerateColumns = false;
            dataGridViewRss.AllowUserToAddRows = false;
            dataGridViewRss.ReadOnly = true;
            dataGridViewRss.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewRss.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Thêm các cột cho kết quả xổ số chi tiết
            dataGridViewRss.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Date",
                HeaderText = "Ngày",
                DataPropertyName = "Date",
                Width = 80
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
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Consolas", 10, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dataGridViewRss.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Province",
                HeaderText = "Tỉnh/Thành (Miền)",
                DataPropertyName = "Province",
                Width = 180,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }
        
        public void FilterRssSourcesByRegion(string region)
        {
            if (string.IsNullOrEmpty(region) || region == "Tất cả")
            {
                filteredRssSources = allRssSources;
            }
            else
            {
                filteredRssSources = allRssSources.Where(r => r.Region == region).ToList();
            }
            
            // Cập nhật ComboBox
            cboRssUrl.DataSource = null;
            cboRssUrl.DataSource = filteredRssSources;
            if (filteredRssSources.Count > 0)
            {
                cboRssUrl.SelectedIndex = 0;
            }
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
                lblStatus.Text = $"Đang tải dữ liệu từ {selectedSource.Province} ({selectedSource.Region})...";
                lblStatus.ForeColor = Color.Blue;
                Application.DoEvents(); // Cập nhật UI ngay lập tức

                await LoadRssDataAsync(url);

                lblStatus.Text = $"Đã tải thành công {rssItems.Count} mục từ {selectedSource.Province} ({selectedSource.Region})";
                lblStatus.ForeColor = Color.Green;
            }
            catch (HttpRequestException httpEx)
            {
                lblStatus.Text = "Lỗi kết nối mạng";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show($"Không thể kết nối đến nguồn RSS:\n{httpEx.Message}\n\nVui lòng kiểm tra kết nối mạng và thử lại.", 
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (XmlException xmlEx)
            {
                lblStatus.Text = "Lỗi định dạng dữ liệu RSS";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show($"Dữ liệu RSS không hợp lệ:\n{xmlEx.Message}", 
                    "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            
            // Lấy thông tin nguồn RSS đang chọn
            RssSource selectedSource = (RssSource)cboRssUrl.SelectedItem;
            string sourceProvince = selectedSource?.Province ?? "";
            string sourceRegion = selectedSource?.Region ?? "";
            
            // Trích xuất ngày từ title
            string date = ExtractDateFromTitle(rssItem.Title);
            
            // Trích xuất tỉnh/thành từ title (ưu tiên từ source nếu có)
            string province = !string.IsNullOrEmpty(sourceProvince) ? sourceProvince : ExtractProvinceFromTitle(rssItem.Title);
            
            // Parse description theo pattern "X: Y"
            string cleanDesc = CleanDescription(rssItem.Description);
            
            // Tìm các pattern giải thưởng với nhiều định dạng khác nhau
            var matches = System.Text.RegularExpressions.Regex.Matches(cleanDesc, @"(\w+|ĐB):\s*([0-9\s\-]+)");
            
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                string prize = match.Groups[1].Value;
                string result = match.Groups[2].Value.Trim();
                
                // Chuẩn hóa tên giải thưởng
                prize = NormalizePrizeName(prize);
                
                // Xử lý kết quả có nhiều số (cách nhau bởi dấu -)
                var numbers = result.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                
                foreach (var number in numbers)
                {
                    string cleanNumber = number.Trim();
                    if (!string.IsNullOrEmpty(cleanNumber) && cleanNumber.All(char.IsDigit))
                    {
                        results.Add(new LotteryResult(date, prize, cleanNumber, $"{province} ({sourceRegion})"));
                    }
                }
            }
            
            return results;
        }
        
        private string NormalizePrizeName(string prizeName)
        {
            // Chuẩn hóa tên giải thưởng
            switch (prizeName.ToUpper())
            {
                case "ĐB":
                case "DB":
                    return "Giải ĐB";
                case "1":
                    return "Giải nhất";
                case "2":
                    return "Giải nhì";
                case "3":
                    return "Giải ba";
                case "4":
                    return "Giải tư";
                case "5":
                    return "Giải năm";
                case "6":
                    return "Giải sáu";
                case "7":
                    return "Giải bảy";
                case "8":
                    return "Giải tám";
                default:
                    return $"Giải {prizeName}";
            }
        }
        
        private string ExtractDateFromTitle(string title)
        {
            // Tìm nhiều pattern ngày tháng khác nhau
            var patterns = new string[]
            {
                @"NGÀY\s+(\d{1,2}/\d{1,2})",  // NGÀY DD/MM
                @"(\d{1,2}/\d{1,2}/\d{4})",   // DD/MM/YYYY
                @"(\d{1,2}-\d{1,2}-\d{4})",   // DD-MM-YYYY
                @"(\d{1,2}/\d{1,2})"          // DD/MM
            };
            
            foreach (var pattern in patterns)
            {
                var match = System.Text.RegularExpressions.Regex.Match(title, pattern);
                if (match.Success)
                {
                    return match.Groups[1].Value;
                }
            }
            
            return DateTime.Now.ToString("dd/MM");
        }
        
        private string ExtractProvinceFromTitle(string title)
        {
            // Tìm tên tỉnh/thành trong title với nhiều pattern
            var patterns = new string[]
            {
                @"XỔ SỐ\s+([A-ZẮẰẲẴẶẤẦẨẪẬÁÀẢÃẠÊỀỂỄỆÉÈẺẼẸÔỒỐỔỖỘƠỜỞỠỢÓÒỎÕỌƯỪỨỬỮỰ\.]+(?:\s+[A-ZẮẰẲẴẶẤẦẨẪẬÁÀẢÃẠÊỀỂỄỆÉÈẺẼẸÔỒỐỔỖỘƠỜỞỠỢÓÒỎÕỌƯỪỨỬỮỰ\.]+)*)",
                @"XS\s+([A-ZẮẰẲẴẶẤẦẨẪẬÁÀẢÃẠÊỀỂỄỆÉÈẺẼẸÔỒỐỔỖỘƠỜỞỠỢÓÒỎÕỌƯỪỨỬỮỰ\.]+(?:\s+[A-ZẮẰẲẴẶẤẦẨẪẬÁÀẢÃẠÊỀỂỄỆÉÈẺẼẸÔỒỐỔỖỘƠỜỞỠỢÓÒỎÕỌƯỪỨỬỮỰ\.]+)*)",
                @"KẾT QUẢ.*?([A-ZẮẰẲẴẶẤẦẨẪẬÁÀẢÃẠÊỀỂỄỆÉÈẺẼẸÔỒỐỔỖỘƠỜỞỠỢÓÒỎÕỌƯỪỨỬỮỰ\.]+(?:\s+[A-ZẮẰẲẴẶẤẦẨẪẬÁÀẢÃẠÊỀỂỄỆÉÈẺẼẸÔỒỐỔỖỘƠỜỞỠỢÓÒỎÕỌƯỪỨỬỮỰ\.]+)*)\s+NGÀY"
            };
            
            foreach (var pattern in patterns)
            {
                var match = System.Text.RegularExpressions.Regex.Match(title, pattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string province = match.Groups[1].Value.Trim();
                    // Loại bỏ một số từ không cần thiết
                    province = province.Replace("KẾT QUẢ", "").Replace("XỔ SỐ", "").Trim();
                    return province;
                }
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
