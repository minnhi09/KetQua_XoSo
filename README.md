# XoSoApp - Ứng dụng Kết quả Xổ số

## Mô tả

Ứng dụng Windows Forms để tải và hiển thị kết quả xổ số từ các nguồn RSS của tất cả các tỉnh thành trong cả 3 miền Bắc, Trung, Nam. Ứng dụng đã được nâng cấp để hỗ trợ đầy đủ 63 tỉnh thành với giao diện thân thiện và tính năng phân tích dữ liệu thông minh.

## Tính năng chính

### 🌍 **Hỗ trợ đa miền đầy đủ**

- **Miền Nam** (18 tỉnh): An Giang, Bạc Liêu, Bến Tre, Cà Mau, Cần Thơ, Đồng Tháp, Hậu Giang, Kiên Giang, Long An, Sóc Trăng, Tây Ninh, Tiền Giang, Trà Vinh, Vĩnh Long, TP.HCM, Đồng Nai, Bình Dương, Vũng Tàu
- **Miền Trung** (16 tỉnh): Đà Nẵng, Khánh Hòa, Phú Yên, Đắk Lắk, Quảng Nam, Quảng Ngãi, Bình Định, Quảng Bình, Quảng Trị, Thừa Thiên Huế, Đắk Nông, Lâm Đồng, Bình Thuận, Ninh Thuận, Gia Lai, Kon Tum
- **Miền Bắc** (27 tỉnh): Hà Nội, Quảng Ninh, Bắc Ninh, Hải Phòng, Nam Định, Thái Bình, Vĩnh Phúc, Hà Nam, Hưng Yên, Ninh Bình, Thanh Hóa, Nghệ An, Hà Tĩnh, Hòa Bình, Sơn La, Điện Biên, Lai Châu, Lào Cai, Yên Bái, Tuyên Quang, Hà Giang, Cao Bằng, Bắc Kạn, Lạng Sơn, Thái Nguyên, Phú Thọ, Bắc Giang

### 🔄 **Chức năng tải và hiển thị**

- **Tải RSS thời gian thực**: Kết nối và tải dữ liệu mới nhất từ các nguồn RSS
- **Chọn nguồn linh hoạt**: ComboBox phân loại rõ ràng theo miền và tỉnh
- **Hiển thị theo ngày**: ComboBox chọn ngày để xem kết quả cụ thể

### 🧠 **Phân tích dữ liệu thông minh**

- **Trích xuất ngày tự động**: Hỗ trợ nhiều định dạng ngày tháng khác nhau
- **Parse description đa dạng**: Xử lý các pattern "Giải: Kết quả" với nhiều biến thể
- **Chuẩn hóa giải thưởng**: Tự động chuyển đổi ĐB, 1, 2, 3... thành tên giải đầy đủ
- **Tách số thông minh**: Xử lý các số được phân tách bởi dấu gạch ngang

### 📊 **Giao diện và hiển thị**

- **DataGridView cải tiến**: Màu sắc phân biệt hàng, font chữ dễ đọc
- **Cột thông tin đầy đủ**:
  - **Ngày**: Ngày quay số
  - **Giải thưởng**: Tên giải đã chuẩn hóa
  - **Kết quả**: Số trúng thưởng (font Consolas, bold)
  - **Tỉnh/Thành (Miền)**: Nguồn với thông tin miền
- **Giao diện trực quan**: Hai ComboBox và bảng dữ liệu dễ theo dõi
- **Xử lý lỗi**: Thông báo trạng thái và xử lý lỗi

## Cách sử dụng

1. **Chạy ứng dụng**: Mở file `WindowsFormsApp1.exe` trong thư mục `bin\Debug\`
2. **Chọn nguồn RSS**: Sử dụng dropdown đầu tiên để chọn nguồn xổ số (hiện tại: XS An Giang)
3. **Tải dữ liệu**: Nhấn nút "Tải RSS" để tải dữ liệu mới nhất từ nguồn đã chọn
4. **Chọn ngày**: Sử dụng dropdown thứ hai để chọn ngày cụ thể muốn xem kết quả
5. **Xem chi tiết**: Bảng dưới sẽ hiển thị tất cả các giải thưởng của ngày đã chọn với từng số trúng riêng biệt

## Cấu trúc dự án

- `Form1.cs` - Logic chính của ứng dụng
- `Form1.Designer.cs` - Thiết kế giao diện người dùng
- `RssItem.cs` - Class model để lưu trữ dữ liệu RSS item
- `RssSource.cs` - Class model để lưu trữ thông tin nguồn RSS
- `Program.cs` - Entry point của ứng dụng
- `rss-links.md` - Danh sách các liên kết RSS có sẵn

## Công nghệ sử dụng

- .NET Framework 4.7.2
- Windows Forms
- System.Net.Http cho việc tải dữ liệu từ web
- System.Xml.Linq cho việc parse XML RSS
- System.Text.RegularExpressions để làm sạch dữ liệu

## Build và Run

```bash
# Build project
msbuild WindowsFormsApp1.csproj

# Hoặc sử dụng Visual Studio
# File -> Open -> Project/Solution -> chọn WindowsFormsApp1.sln
# Build -> Build Solution (Ctrl+Shift+B)
# Debug -> Start Debugging (F5)
```

## Lưu ý

- Cần kết nối Internet để tải dữ liệu RSS
- Dữ liệu hiển thị phụ thuộc vào cấu trúc RSS từ nguồn
- URL RSS có thể thay đổi theo thời gian, có thể cập nhật trong textbox
