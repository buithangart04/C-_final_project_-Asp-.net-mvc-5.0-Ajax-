var homeConfig = {
    pageSize: 3,
    pageIndex: 1
}

var firstPageClick = true;
$(document).ready(function () {
    $("#table_field").on('click', '#detail_invoice', function () {
        // lay text cua the ma hdban  vua click 
        var MaHD = $(this).closest('tr').find("td:eq(1)").html(); 
        var isCus = $("#is_cus").val();
        var tenKH = "";
       
        var ngay = $(this).closest('tr').find("td:eq(7)").html();
        $('#ngay_detail').text('Ngày: ' + ngay);
        var tongTien = parseFloat($(this).closest('tr').find("td:eq(2)").html());
        $('#tongTien_detail').text('Tổng tiền : ' + tongTien);
        var ThanhToan = $(this).closest('tr').find("td:eq(3)").html();
        $('#ThanhToan_detail').text('Thanh Toán  : ' + ThanhToan);
       
        tenKH = $('#name_Cus').val();
     
        $('#TenKh_detail').text(tenKH);
        $('.row_detail').remove();
        $.ajax({
            url: '/SoGhiNo/getChiTietHD',
            data: { MaHd: MaHD, isCus: isCus },
            dataType: "json",
            type: "GET",
            success: function (res) {
                var tongTienHang = 0;
                var result = JSON.parse(res.data);
                for (var i = 0; i < result.length; i++){
                    var html = '<tr class="row_detail"><td>' + result[i].MaSp + '</td><td>' + result[i].TenSp + '</td ><td class="text-center">' + result[i].MoTa + '</td><td class="text-center">' + result[i].SL + '</td><td class="text-center">' + result[i].GiaSP + '  </td><td class="text-center">' + result[i].Tien + '</td>';
                    $("#table_detail").append(html);
                    tongTienHang = tongTienHang + parseFloat( result[i].Tien );
                }
                var tienChietKhau = tongTienHang - tongTien;
                if (tienChietKhau > 0) {
                    $('#tongTienHang_detail').text('Tổng tiền hàng : ' + tongTienHang);
                    $('#chietKhau_detail').text('Tiền chiết khấu : ' + tienChietKhau);
                } else {
                    $('#tongTienHang_detail').text('');
                    $('#chietKhau_detail').text('');
                }

            }
        });
      
        $('#detail_model').modal('show');


    });
    $("#tao_tra_no").on('click', function () {
        $('#ten_Kh_TraNo').text($('#ten_Kh').text());
        $('#diachi_No').text($('#dia_chi_Kh').text());
        $('#sotaikhoan_No').text($('#stk_Kh').text());
        $('#sodienthoai_No').text($('#sdt_tenKh').text());
        $('#sono_No').text($('#so_no_Kh').text());
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = today.getFullYear();

        today = mm + '/' + dd + '/' + yyyy;
        $('#ngay_No').text(today);

        $('#TraNo_model').modal('show');
    });
    // click button tra no 
    $("#save_tra_no").on('click', function () {
        var IsCus = $('#is_cus').val();
        var khId = $('#id_cus').val();
        var tien = parseFloat( $('#tien_tra').val());
        window.location = '/SoGhiNo/TraNo?khid=' + khId + '&isCus=' + IsCus + "&tien=" + tien;
    });
    $("#lich_su_giao_dich").on('click', function () {

        var IsCus = $('#is_cus').val();
        var khId = $('#id_cus').val();
        window.location = '/LichSuGiaoDich/Index?id=' + khId + '&isCus=' + IsCus;
    });
    
    $('#SoGhiNo_CongTY').click(function () {
        var btn = $('#btn');
        var selectKhachHang = $('#select_khachHang');
        var selectCongTy = $('#select_CongTy');
        selectCongTy.css('left', '0px');
        selectKhachHang.css('left', '-500px');
        btn.css('left', '300px');
        $("#txtKeyword").val('');
        $("#is_cus").val(-1);
        $('.add_row').remove();
        $('#paginationholder').html('');

    });
    $('#SoGhiNo_KhachHang').click(function () {
        var btn = $('#btn');
        homeConfig.pageIndex = 1;
        var selectKhachHang = $('#select_khachHang');
        var selectCongTy = $('#select_CongTy');
        selectCongTy.css('left', '500px');
        selectKhachHang.css('left', '0px'); 
        btn.css('left', '0px');
        $("#is_cus").val(1);
        $('.add_row').remove();
        $('#paginationholder').html('');
    });
    $("#listCT").change(function () {
        homeConfig.pageIndex = 1;
        var CtId = $('#listCT').val();
        var address = $('#address');
        var stk = $('#stk');
        var phone = $('#phone');
        $.ajax({
            url: '/SoGhiNo/ChangeCT',
            data: { id: CtId },
            dataType: "json",
            type: "POST",
            success: function (data) {
                address.text(data.DiaChi);
                stk.text(data.STK);
                phone.text(data.SoDienThoai);
       
            }
        });
    });
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
                    url: "/SoGhiNo/SearchCustomer",
                    dataType: "json",
                    data: {
                        q: request.term
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
        $('#paginationholder').html('');
        $('#paginationholder').html('<ul id="pagination" class="pagination-sm"></ul>');
        loadData();
    });

    $("#listCT").change(function () {
        $('#paginationholder').html('');
        $('#paginationholder').html('<ul id="pagination" class="pagination-sm"></ul>');
        loadData1();
    });
    $('#exampleModal').on('shown.bs.modal', function () {
        $('#myInput').trigger('focus')
    })

});

function loadData1() {
    var table = $("#table_field");
    var selectSp = $('#selectSp');
    var CtId = $('#listCT').val();
    var address = $('#address');
    var stk = $('#stk');
    var phone = $('#phone');
    $.ajax({
        url: '/SoGhiNo/ChangeCT',
        data: { id: CtId ,page: homeConfig.pageIndex, pageSize: 3 },
        dataType: "json",
        type: "GET",
        success: function (data) {
            var model = JSON.parse(data.model);
            address.text(data.DiaChi);
            stk.text(data.STK);
            phone.text(data.SoDienThoai);
            $('#tong_so_No_ct').text('Số nợ : ' + getTextSoDu(data.SoNoCongTy));
            $('.add_row').remove();
            for (var i = 0; i < model.length; i++) {
                var html = '<tr class="add_row"><td>' + model[i].MaHDNo + '</td><td>' + model[i].MaHDBan + '</td ><td class="text-center">' + model[i].TongTien + '</td><td class="text-center">' + model[i].ThanhToan + '</td><td class="text-center">' + getTextSoDu(model[i].SoDu_No) + '  </td><td class="text-center">' + getTextBu(model[i].daThanhToanSau) + '</td><td id="tong" class="text-center">' + getTextSoDu( model[i].ConNo) + 'đ </td><td id="tong" class="text-center">' + model[i].Ngay + ' </td><td class="text-center"><button  id="detail_invoice" type="button" class="btn btn-info" width="50%"><i class="fas fa-trash"></i> Xem chi tiết hóa đơn </button></td></tr>';
                table.append(html);
            }
            paging(data.total,0);
        }
    });

}
function loadData() {
    var result = $("#txtKeyword").val().split("_");
    var cusid = result[result.length - 1];
    var table = $("#table_field");
    if ($.isNumeric(cusid)) {
        $.ajax({
            url: '/SoGhiNo/getCusInfo',
            // pass product id 
            data: { id: cusid, page: homeConfig.pageIndex, pageSize: 3 },
            dataType: "json",
            type: "GET",
            success: function (data) {
                // parse dữ liệu ra để đọc 
                var cus = JSON.parse(data.result);
                var model = JSON.parse(data.model);
                $('#name_cus').val(cus.Ten);
                $('#address_cus').val(cus.DiaChi);
                $('#stk_cus').val(cus.STK);
                $('#phone_cus').val(cus.SoDienThoai);
                $('#tong_so_No_cus').text('Số nợ : '+getTextSoDu(cus.SoNo));
                $('.add_row').remove();
                for (var i = 0; i < model.length; i++) {
                    var html = '<tr class="add_row"><td>' + model[i].MaHDNo + '</td><td>' + model[i].MaHDBan + '</td ><td class="text-center">' + model[i].TongTien + '</td><td class="text-center">' + model[i].ThanhToan + '</td><td class="text-center">' + getTextSoDu(model[i].SoDu_No) + '  </td><td class="text-center">' + getTextBu( model[i].daThanhToanSau ) + '</td><td id="tong" class="text-center">' + getTextSoDu( model[i].ConNo )+ '</td><td id="tong" class="text-center">' + model[i].Ngay + ' </td><td class="text-center"><button  id="detail_invoice" type="button" class="btn btn-info" width="50%"><i class="fas fa-trash"></i> Xem chi tiết hóa đơn </button></td></tr>';
                    table.append(html);
                }
                paging(data.total,1);
            },
            fail: function () {
                alert("Không tìm thấy khách hàng ");
            }
        });
    } else {
        alert("Không tìm thấy khách hàng ");
    }
}
function paging(totalRow,cus) {
 
    $('#pagination').twbsPagination({
        initiateStartPageClick:false,
       totalPages : Math.ceil(totalRow / homeConfig.pageSize),
       visiblePages: 10,
        onPageClick: function (event, page) {
            homeConfig.pageIndex = page;
            if (cus == 1) loadData();
            else if(cus==0) loadData1();
        }
    });
}
function getTextSoDu(txt) {
    if (parseFloat(txt) < 0) {
        return (-parseFloat(txt)) + "(dư)";
    } else {
        return txt;
    }
}
function getTextBu(txt) {
    if (parseFloat(txt) < 0) {
        return (-parseFloat(txt)) + "(bù)";
    } else {
        return txt + "(được bù)";
    }
}
