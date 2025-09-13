# RSS Reader - Ứng dụng đọc RSS Kết quả xổ số

## Mô tả

Ứng dụng Windows Forms để tải và hiển thị dữ liệu RSS từ các nguồn xổ số khác nhau bao gồm Miền Nam, Miền Trung, Miền Bắc và các tỉnh.

## Tính năng

- **Chọn nguồn RSS**: ComboBox với các tùy chọn có sẵn:
  - XS An Giang (hiện tại)
- **Phân tích dữ liệu thông minh**: 
  - Tự động trích xuất ngày từ tiêu đề RSS
  - Parse description theo pattern "Giải: Kết quả"
  - Phân tách các giải thưởng (ĐB, 1, 2, 3, 4, 5, 6, 7)
- **ComboBox ngày**: Chọn kết quả theo từng ngày cụ thể
- **Hiển thị chi tiết**: Bảng dữ liệu với các cột:
  - **Ngày**: Ngày quay số được trích xuất từ title
  - **Giải thưởng**: Loại giải (ĐB, 1, 2, 3, 4, 5, 6, 7)
  - **Kết quả**: Các số trúng thưởng
  - **Tỉnh/Thành**: Nguồn xổ số
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
