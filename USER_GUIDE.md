# Hướng dẫn sử dụng XoSoApp

## Cài đặt và khởi động

1. **Yêu cầu hệ thống**:

   - Windows 7 hoặc mới hơn
   - .NET Framework 4.7.2+
   - Kết nối Internet

2. **Chạy ứng dụng**:
   - Build từ Visual Studio hoặc
   - Chạy file `WindowsFormsApp1.exe` từ thư mục `bin/Debug/`

## Hướng dẫn sử dụng từng bước

### Bước 1: Chọn nguồn RSS

- Mở dropdown "Nguồn RSS"
- Chọn tỉnh/thành phố mong muốn
- Các nguồn được sắp xếp theo miền: [Miền Nam], [Miền Trung], [Miền Bắc]

### Bước 2: Tải dữ liệu

- Nhấn nút "Tải RSS"
- Chờ thông báo tải thành công (màu xanh lá) hoặc lỗi (màu đỏ)
- Thanh trạng thái sẽ hiển thị tiến trình

### Bước 3: Xem kết quả

- Sau khi tải thành công, dropdown "Ngày" sẽ được điền dữ liệu
- Chọn ngày muốn xem từ dropdown
- Kết quả sẽ hiển thị trong bảng bên dưới

### Bước 4: Phân tích kết quả

- **Cột Ngày**: Ngày quay số
- **Cột Giải thưởng**: Loại giải (Giải ĐB, Giải nhất, Giải nhì, v.v.)
- **Cột Kết quả**: Số trúng thưởng (hiển thị bằng font đặc biệt)
- **Cột Tỉnh/Thành (Miền)**: Nguồn dữ liệu với thông tin miền

## Tính năng nâng cao

### Lọc theo miền

- Code đã hỗ trợ phương thức `FilterRssSourcesByRegion(string region)`
- Có thể mở rộng để thêm ComboBox lọc theo miền

### Cấu hình nguồn RSS

- File `rss-sources.xml` chứa cấu hình tất cả nguồn RSS
- Có thể thêm/sửa/xóa nguồn RSS mà không cần sửa code
- Nếu file không tồn tại, ứng dụng sẽ sử dụng cấu hình mặc định

### Xử lý lỗi

- **Lỗi mạng**: Thông báo khi không kết nối được Internet
- **Lỗi RSS**: Thông báo khi dữ liệu RSS không hợp lệ
- **Lỗi chung**: Thông báo lỗi khác với chi tiết

## Khắc phục sự cố

### Không tải được dữ liệu

1. Kiểm tra kết nối Internet
2. Thử nguồn RSS khác
3. Đợi một lúc và thử lại (có thể do server bận)

### Dữ liệu hiển thị không đúng

1. Kiểm tra định dạng dữ liệu RSS nguồn
2. Một số nguồn có thể có định dạng khác nhau
3. Code đã hỗ trợ nhiều pattern, nhưng có thể cần cập nhật

### Lỗi ứng dụng

1. Đảm bảo .NET Framework được cài đặt
2. Chạy với quyền administrator nếu cần
3. Kiểm tra log lỗi trong Windows Event Viewer

## Mở rộng và tùy chỉnh

### Thêm nguồn RSS mới

1. Mở file `rss-sources.xml`
2. Thêm element `<RssSource>` mới trong region phù hợp
3. Cung cấp đầy đủ thuộc tính: displayName, province, url

### Tùy chỉnh giao diện

1. Sửa trong `Form1.Designer.cs` để thay đổi layout
2. Sửa `SetupDataGridView()` để thay đổi màu sắc, font chữ
3. Thêm controls mới nếu cần

### Cải tiến parsing

1. Sửa `ParseLotteryResults()` để xử lý format mới
2. Cập nhật regex pattern trong `ExtractDateFromTitle()` và `ExtractProvinceFromTitle()`
3. Thêm logic chuẩn hóa trong `NormalizePrizeName()`

## Liên hệ và hỗ trợ

Nếu cần hỗ trợ hoặc có góp ý cải tiến, vui lòng tạo issue trên repository hoặc liên hệ trực tiếp.
