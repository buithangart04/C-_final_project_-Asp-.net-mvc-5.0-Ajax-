$(document).ready(function () {
    var tongtien = 0;

    $("#table_field").on('click', '#remove', function () {
        var string_tong_Remove = $("#tong").closest('td').html(); // lay text cua the td vua click bo 
        tong_remove = parseFloat(string_tong_Remove.substring(0, string_tong_Remove.length));// parse float cho chuoi bỏ kí tự đ 
        tongtien -= tong_remove;
        setTongTienThanhToan();
        setTongTienHang();
        setConNo();
        $(this).closest('tr').remove(); // xoa di cot vua click 

    });
    // change tien_chiet_khau
    $('#tien_chiet_khau').change(function () {
        if (parseFloat($('#tien_chiet_khau').val()) >= 0) {
            setTongTienThanhToan();
            setConNo();
        } else {
            alert("Tiền chiết khấu không được âm !")
            $('#tien_chiet_khau').val('0');
        }
        
    });
    // change tien_thanh_toan 
    $('#tien_thanh_toan').change(function () {
      
        if (parseFloat($('#tien_thanh_toan').val()) > 0) {
            $('#icon_Paid').css('display', 'block');
            setConNo();
        } else if (parseFloat($('#tien_thanh_toan').val()) == 0) {
            $('#icon_Paid').css('display', 'none');
            setConNo();
        } else {
            alert("tiền thanh toán không được âm !");
            $('#tien_thanh_toan').val('0');
            $('#icon_Paid').css('display', 'none');
        }
       
    });
    // function when change ct then load product of the company 
    $("#listCT").change(function () {
        var table = $("#table_field");

        if ($('.add_row').length > 0) { // check số lượng hàng ở bảng đặt sản phẩm để biết mua hàng ở cty chưa 
            var r = confirm("Bạn đang chọn mua hàng ở Công Ty này .Nếu Thay đổi chúng tôi sẽ xóa những sản phẩm bạn vừa thêm vào !")
            if (r == true) {
                $('.add_row').remove(); // remove hết hàng để mua sp khác 
                tongtien = 0;
               
                setTongTienThanhToan();
                setTongTienHang();
                setConNo();
            } else {
                return;
            }
        }
        var selectSp = $('#selectSp');
        var CtId = $('#listCT').val();

        var address = $('#address_Ct');
        var stk = $('#stk_Ct');
        var phone = $('#phone_Ct');
        selectSp.text('');
        $.ajax({
            url: '/HDNhapHang/ChangeCT',
            data: { id: CtId },
            dataType: "json",
            type: "POST",
            success: function(res) {
                var result = JSON.parse(res.data);
                address.text(result.DiaChi);
                stk.text(result.STK);
                phone.text(result.SoDienThoai);

                for (i = 0; i < result.sanPhams.length; i++) {
                    selectSp.append("<option value= '" + result.sanPhams[i].Ma + "'>" + result.sanPhams[i].Ten + "</option>")
                }
            }
        });
    });
    function setTongTienThanhToan() {

        tongTienThanhToan = tongtien - parseFloat($('#tien_chiet_khau').val());
        $('#tong_tien_thanh_toan').text(tongTienThanhToan);

    }
    function setTongTienHang() {
        var textTongTienHang = $('#tong_tien_hang');
        textTongTienHang.text(tongtien + 'đ');

    }
    function setConNo() {
        conNo = parseFloat($('#tong_tien_thanh_toan').html()) - parseFloat($('#tien_thanh_toan').val());
        $('#con_no_value').val(conNo);
        if (conNo < 0) {
            $('#text_con_no').text("Trả Dư");
            $('#con_no').text(-conNo);
        } else {
            $('#text_con_no').text("Còn Nợ ");
            $('#con_no').text(conNo);
        } 
    }
    // function add more product
    $('#add_pro').click(function () {
        var selectSp = $('#selectSp');
        var spid = selectSp.val();
        $.ajax({
            url: '/HDNhapHang/AddSp',
            // pass product id 
            data: { id: spid },
            dataType: "json",
            type: "POST",
            success: function (data) {
                var result = JSON.parse(data.sanpham); // lấy value sp vừa chọn
                var gia = parseFloat(result.GiaNhap);
                var slDat = parseFloat($('#slDat').val());
                if (slDat > 0) { // check đk số lượng đặt lớn hơn 0 
                    var tong = gia * slDat;
                    tongtien += tong; // cộng thêm vào tổng tiền hàng 
                    setTongTienThanhToan(); // thay đổi tổng tiền phải thanh toán 
                    setTongTienHang();
                    setConNo();
                    // append thêm 1 hàng vào 
                    var html = '<tr class="add_row"><td>' + result.Ma + '</td><td>' + result.Ten + '</td ><td class="text-center">' + result.MoTa + '</td><td class="text-center">' + slDat + '</td><td class="text-center">' + result.KhoiLuong + ' kg </td><td class="text-center">' + gia + '</td><td id="tong" class="text-center">' + tong + 'đ </td><td class="text-center"><button  id="remove" type="button" class="btn btn-danger" width="50%"><i class="fas fa-trash"></i> Bỏ Sản Phẩm</button></td></tr>';
                    $("#table_field").append(html);
                } else {
                    alert("Số lượng đặt không được âm");
                }

            },
            fail: function () {
                alert("Không tìm thấy Sản phẩm ");

            }
        });

    });
    function checkMaHangExisted(list, ma) {
        for (var i = 0; i < list.length; i++) {
            if (ma == list[i].MaSanPham) return i;
        }
        return -1;
    }
    function getListChiTietHoaDon(list) {
        var temp = new Array();
        for (var i = 0; i < list.length; i++) {
            var index = checkMaHangExisted(temp,list[i].MaSanPham);
            if (index > -1) {
                temp[index].SL += list[i].SL;
                temp[index].ThanhTien +=list[i].ThanhTien;
            } else{
                temp.push(list[i]);
            }
        }
        return temp;
    }
    
    $("#them_du_lieu").click(function () {
        // muốn tìm và lấy dữ liệu trong table undefined thì phải dùng hàm find 
        // không thể dùng rows[i].cells[j].html() (chỉ có dữu liệu đã fix cứng )
        if ($(".add_row").length > 0) {
            var table = $("#table_field");
            var model = {};
            var chiTietHDNhaps = new Array();
            var chiTietHDNhapsNotFormat = new Array();
            // bỏ hàng tiêu đề đầu tiên và số hàng đc tính bằng số class add_row cộng thêm 1(bởi vì tính thêm hàng tiêu đề) ;

            for (var i = 1; i < $(".add_row").length + 1; i++) {
                var MaSp = table.find("tr:eq(" + i + ")").find("td:eq(0)").html();
                var TenSp = table.find("tr:eq(" + i + ")").find("td:eq(1)").html();
                var soLuong = parseInt(table.find("tr:eq(" + i + ")").find("td:eq(3)").html());
                var donGia = parseFloat(table.find("tr:eq(" + i + ")").find("td:eq(5)").html());
                var thanhTien = parseFloat(table.find("tr:eq(" + i + ")").find("td:eq(6)").html());
                // thêm đối tượng chi tiết sản phầm map object model
                var chiTietHDNhap = {};
                chiTietHDNhap.MaSanPham = MaSp;
                chiTietHDNhap.SL = soLuong;
                chiTietHDNhap.DonGia = donGia;
                chiTietHDNhap.ThanhTien = thanhTien;
                chiTietHDNhapsNotFormat.push(chiTietHDNhap);
            }
            chiTietHDNhaps = getListChiTietHoaDon(chiTietHDNhapsNotFormat);
            var textTongTienThanhToan = $("#tong_tien_thanh_toan").html();
            model.chiTietHDNhaps = chiTietHDNhaps;
            model.MaCongTy = $("#listCT").val();
            //cắt chuỗi bỏ chữ đ
            model.TongTien = parseFloat(textTongTienThanhToan.substring(0, textTongTienThanhToan.length));
            model.ThanhToan = parseFloat( $("#tien_thanh_toan").val());
            model.ConNo = $('#con_no_value').val();
            $.ajax({
                url: '/HDNhapHang/addHDNhap',
                data: model, // truyền thế này thì còn đc chứ truyền như trên thì vứt 
                dataType: "json",
                type: "POST",
                success: function (data) {
                    if (data.status == true) {
                        alert("Thêm mới hóa đơn thành công !");
                    } else {
                        alert(data.errorMessage);
                    }
                },
                fail: function () {
                    alert(" Không thể Thêm mới hóa đơn  !");
                }
            });
        } else {
            alert("chưa chọn sản phẩm");
        }
    });
});
