$(document).ready(function () {
    var tongtien = 0;
    $("#table_field").on('click', '#remove', function () {
        var string_tong_Remove = $("#tong").closest('td').html(); // lay text cua the td vua click bo 
        tong_remove = parseFloat(string_tong_Remove.substring(0, string_tong_Remove.length));// parse float cho chuoi bỏ kí tự đ 
        tongtien -= tong_remove;
        setTongTienHang();
        setConNo();
        $(this).closest('tr').remove(); // xoa di cot vua click 

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
    //set lại id cho nếu người dùng sửa hoặc thêm mới thì là -1
    $('#info_customer').on('change', '.input_cus', function () {
        alert("Bạn đã thay đổi khách hàng mới ");
            $('#id_cus').val(-1);
       
    });
    
    function setTongTienHang() {
        var textTongTienHang = $('#tong_tien_hang');
        textTongTienHang.text(tongtien + 'đ');

    }
    function setConNo() {
        conNo = parseFloat($('#tong_tien_hang').html()) - parseFloat($('#tien_thanh_toan').val());
        $('#con_no_value').val(conNo);
        if (conNo < 0) {
            $('#text_con_no').text("Trả Dư");
            $('#con_no').text(-conNo);
        } else {
            $('#text_con_no').text("Còn Nợ ");
            $('#con_no').text(conNo);
        } 
    }
    $('#txtKeyword').on('focus', function () {
        $('#info_customer').css('opacity', '0.1');
    });
    $('#txtKeyword').on('focusout', function () {
        $('#info_customer').css('opacity', '1');
    });
    // sk auto complete
   
    $(function () {
    $("#txtKeyword").autocomplete({
            minLength: 1,
        source: function (request, response) {
            $.ajax({
                url: "/HDBanHang/SearchCustomer",
                dataType: "json",
                data: {
                   q : request.term
                },
                success: function (res) {
                    response(res.data);
                }
            });
        },
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            }
        })
        .autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li class='item_li'>")
                    .append("<div class='item_div'>" + item.label + "</div>")
                    .appendTo(ul);
            };
    });
    $('#btn_search').click(function () {
        var result = $("#txtKeyword").val().split("_");
        var cusid = result[result.length - 1];
      
        if ($.isNumeric(cusid)) {
            $.ajax({
                url: '/HDBanHang/getCusInfo',
                // pass product id 
                data: { id: cusid },
                dataType: "json",
                type: "POST",
                success: function (data) {
                    // parse dữ liệu ra để đọc 
                    var cus = JSON.parse(data.result);
                    $('#name_cus').val(cus.Ten);  
                    $('#address_cus').val(cus.DiaChi);  
                    $('#stk_cus').val(cus.STK);  
                    $('#phone_cus').val(cus.SoDienThoai);  
                    $('#id_cus').val(cus.Ma);
                },
                fail: function () {
                    alert("Không tìm thấy khách hàng ");

                }
            });
        } else {
            alert("Không tìm thấy khách hàng ");
        }
       
    });
    function getSlDat(id) {
        var table = $("#table_field");
        var totalSLDat=0;
        for (var i = 1; i < $(".add_row").length + 1; i++) {
            var MaSp = parseFloat( table.find("tr:eq(" + i + ")").find("td:eq(0)").html());
            if (MaSp == id) {
                totalSLDat += parseFloat(table.find("tr:eq(" + i + ")").find("td:eq(3)").html())
            }


        }
        return totalSLDat;
    }
    // function add more product
    $('#add_pro').click(function () {
        var selectSp = $('#selectSp');
        var spid = selectSp.val();
        $.ajax({
            url: '/HDBanHang/AddSp',
            // pass product id 
            data: { id: spid },
            dataType: "json",
            type: "POST",
            success: function (data) {
                var result = JSON.parse(data.sanpham); // lấy value sp vừa chọn
                var gia = parseFloat(result.GiaBan);
                var slDat = parseFloat($('#slDat').val());
                if (slDat > 0 && (slDat + getSlDat(spid) )<= parseFloat(result.SLTon)) { // check đk số lượng đặt lớn hơn 0 
                    var tong = gia * slDat;
                    tongtien += tong; // cộng thêm vào tổng tiền hàng 
                    setTongTienHang();
                    setConNo();
                    // append thêm 1 hàng vào 
                    var html = '<tr class="add_row"><td>' + result.Ma + '</td><td>' + result.Ten + '</td ><td class="text-center">' + result.MoTa + '</td><td class="text-center">' + slDat + '</td><td class="text-center">' + result.KhoiLuong + ' kg </td><td class="text-center">' + gia + '</td><td id="tong" class="text-center">' + tong + 'đ </td><td class="text-center"><button  id="remove" type="button" class="btn btn-danger" width="50%"><i class="fas fa-trash"></i> Bỏ Sản Phẩm</button></td></tr>';
                    $("#table_field").append(html);
                } else {
                    alert("Số lượng đặt không được âm hoặc lớn hơn số lượng tồn trong kho :" + result.SLTon);
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
            var index = checkMaHangExisted(temp, list[i].MaSanPham);
            if (index > -1) {
                temp[index].SL += list[i].SL;
                temp[index].ThanhTien += list[i].ThanhTien;
            } else {
                temp.push(list[i]);
            }
        }
        return temp;
    }
    function checkEmptyCustomer() {
        var inputs = $(".input_cus");
        for (var i = 0; i < inputs.length; i++) {
           if($(inputs[i]).val()=="") return false ;
        }
        return true;
    }
    $("#them_du_lieu").click(function () {
        // muốn tìm và lấy dữ liệu trong table undefined thì phải dùng hàm find 
        // không thể dùng rows[i].cells[j].html() (chỉ có dữu liệu đã fix cứng )
        if ($(".add_row").length > 0 && checkEmptyCustomer()) {
            var table = $("#table_field");
            var model = {};
            var chiTietHDBans = new Array();
            var chiTietHDBansNotFormat = new Array();
            // bỏ hàng tiêu đề đầu tiên và số hàng đc tính bằng số class add_row cộng thêm 1(bởi vì tính thêm hàng tiêu đề) ;

            for (var i = 1; i < $(".add_row").length + 1; i++) {
                var MaSp = table.find("tr:eq(" + i + ")").find("td:eq(0)").html();
                var TenSp = table.find("tr:eq(" + i + ")").find("td:eq(1)").html();
                var soLuong = parseInt(table.find("tr:eq(" + i + ")").find("td:eq(3)").html());
                var donGia = parseFloat(table.find("tr:eq(" + i + ")").find("td:eq(5)").html());
                var thanhTien = parseFloat(table.find("tr:eq(" + i + ")").find("td:eq(6)").html());
                // thêm đối tượng chi tiết sản phầm map object model
                var chiTietHDBan = {};
                chiTietHDBan.MaSanPham = MaSp;
                chiTietHDBan.SL = soLuong;
                chiTietHDBan.DonGia = donGia;
                chiTietHDBan.ThanhTien = thanhTien;
                chiTietHDBansNotFormat.push(chiTietHDBan);
            }
            chiTietHDBans = getListChiTietHoaDon(chiTietHDBansNotFormat);
            model.chiTietHDBans = chiTietHDBans;
            model.MaKhachHang = $("#id_cus").val();
            model.Ten = $("#name_cus").val();
            model.DiaChi = $("#address_cus").val();
            model.STK = $("#stk_cus").val();
            model.SoDienThoai = $("#phone_cus").val();
            //cắt chuỗi bỏ chữ đ
            model.TongTien = tongtien;
            model.ThanhToan = parseFloat($("#tien_thanh_toan").val());
            model.ConNo = $('#con_no_value').val();
            $.ajax({
                url: '/HDBanHang/addHDBan',
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
            alert("bạn  chưa chọn sản phẩm hoặc khách hàng !");
        }
    });
});
