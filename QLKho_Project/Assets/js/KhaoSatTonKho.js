$(document).ready(function () {
    $("#table_field").on('click', '#remove', function () {
        var string_tong_Remove = $("#tong").closest('td').html(); // lay text cua the td vua click bo 
       
        $(this).closest('tr').remove(); // xoa di cot vua click 

    });
    function getSlDat(id) {
        var table = $("#table_field");
        var totalSLDat = 0;
        for (var i = 1; i < $(".add_row").length + 1; i++) {
            var MaSp = parseFloat(table.find("tr:eq(" + i + ")").find("td:eq(0)").html());
            if (MaSp == id) {
                totalSLDat += parseFloat(table.find("tr:eq(" + i + ")").find("td:eq(3)").html())
            }


        }
        return totalSLDat;
    }
    $('#add_pro').click(function () {
        var selectSp = $('#selectSp');
        var spid = selectSp.val();
        $.ajax({
            url: '/HangTonKho/AddSp',
            // pass product id 
            data: { id: spid },
            dataType: "json",
            type: "POST",
            success: function (data) {
                var result = JSON.parse(data.sanpham); // lấy value sp vừa chọn
                var slHong = parseFloat( $("#sl_hong").val());
                if (slHong > 0 && slHong + getSlDat(parseFloat(result.Ma)) <= parseFloat(result.SLTon)) {
                    var xuli = false;
                    if ($("#check_xu_li").prop("checked") == true) {
                        xuli = true;
                    }
                    else if ($("#check_xu_li").prop("checked") == false) {
                        xuli = false;
                    }

                    var html = '<tr class="add_row"><td>' + result.Ma + '</td><td>' + result.Ten + '</td ><td class="text-center">' + result.MoTa + '</td><td class="text-center">' + slHong + ' bao </td><td class="text-center">' + xuli + '</td><td id="tong" class="text-center">' + $("#ghi_Chu").val() + ' </td><td class="text-center"><button  id="remove" type="button" class="btn btn-danger" width="50%"><i class="fas fa-trash"></i> Bỏ Sản Phẩm</button></td></tr>';
                    $("#table_field").append(html);
                   
                } else {
                    
                    alert("Số lượng hỏng không được âm hoặc lớn hơn số lượng tồn trong kho :" + result.SLTon);   
                }
                
               

            },
            fail: function () {
                alert("Không tìm thấy Sản phẩm ");

            }
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

        $("#them_du_lieu").click(function () {
            // muốn tìm và lấy dữ liệu trong table undefined thì phải dùng hàm find 
            // không thể dùng rows[i].cells[j].html() (chỉ có dữu liệu đã fix cứng )
            if ($(".add_row").length > 0) {
                var table = $("#table_field");
                var model = {};
                var chiTietKhaoSats = new Array();
                var chiTietKhaoSatsNotFormat = new Array();
                // bỏ hàng tiêu đề đầu tiên và số hàng đc tính bằng số class add_row cộng thêm 1(bởi vì tính thêm hàng tiêu đề) ;

                for (var i = 1; i < $(".add_row").length + 1; i++) {
                    var MaSp = table.find("tr:eq(" + i + ")").find("td:eq(0)").html();
  
                    var soLuong = parseInt(table.find("tr:eq(" + i + ")").find("td:eq(3)").html());
                    var duocXuli = table.find("tr:eq(" + i + ")").find("td:eq(4)").html();
                    var ghiChu = table.find("tr:eq(" + i + ")").find("td:eq(5)").html();
                    // thêm đối tượng chi tiết sản phầm map object model
                    var chiTietKhaoSat = {};
                    chiTietKhaoSat.MaSanPham = MaSp;
                    chiTietKhaoSat.SoSanPhamHong = soLuong;
                    chiTietKhaoSat.DaDuocXuLI = duocXuli;
                    chiTietKhaoSat.GhiChu = ghiChu;
                    chiTietKhaoSatsNotFormat.push(chiTietKhaoSat);
                }
                chiTietKhaoSats = getListChiTietHoaDon(chiTietKhaoSatsNotFormat);
               
                model.chiTietKhaoSats = chiTietKhaoSats;
                model.TenNguoi = $("#ten_nguoi").val();
                //cắt chuỗi bỏ chữ đ
               
                $.ajax({
                    url: '/HangTonKho/addKhaoSat',
                    data: model, // truyền thế này thì còn đc chứ truyền như trên thì vứt 
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        if (data.status == true) {
                            alert("Thêm mới khảo sát  thành công !");
                        } else {
                            alert(data.errorMessage);
                        }
                    },
                    fail: function () {
                        alert(" Không thể Thêm mới  khảo sát   !");
                    }
                });
            } else {
                alert("chưa chọn sản phẩm");
            }
        });
    });
});