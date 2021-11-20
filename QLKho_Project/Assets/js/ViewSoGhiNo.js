var homeConfig = {
    pageSize: 3,
    pageIndex: 1
}

var firstPageClick = true;
$(document).ready(function () {
    $("#table_field").on('click', '#detail_user', function () {
        // lay text cua the ma hdban  vua click 
        var MaKH = $(this).closest('tr').find("td:eq(0)").html();
        var isCus = $("#is_cus").val();
        window.location = '/SoGhiNo/Index?id=' + MaKH + '&isCus=' + isCus;
    });
    $('#SoGhiNo_CongTY').click(function () {
        var btn = $('#btn');
        $("#is_cus").val(-1);
        homeConfig.pageIndex = 1;
        var selectKhachHang = $('#select_khachHang');
        var selectCongTy = $('#select_CongTy');
        selectCongTy.css('left', '0px');
        $("#form_search").css('display', 'none');
        selectKhachHang.css('left', '-500px');
        btn.css('left', '300px');
        $("#txtKeyword").val('');
        $('.add_row').remove();
        $('#paginationholder').html('');
        $('#paginationholder').html('<ul id="pagination" class="pagination-sm"></ul>');
        loadCongTyNo();
    });
    $('#SoGhiNo_KhachHang').click(function () {
        var btn = $('#btn');
        $("#is_cus").val(1);
        homeConfig.pageIndex = 1;
        var selectKhachHang = $('#select_khachHang');
        var selectCongTy = $('#select_CongTy');
        selectCongTy.css('left', '500px');
        selectKhachHang.css('left', '0px');
        $("#form_search").css('display', 'inline-block');
        btn.css('left', '0px');
        $('.add_row').remove();
        $('#paginationholder').html('');
        $('#paginationholder').html('<ul id="pagination" class="pagination-sm"></ul>');
        loadKhachHangNo();
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
        var result = $("#txtKeyword").val().split("_");
        var cusid = result[result.length - 1];
        var MaKH = cusid;
        if ($.isNumeric(cusid)) {
            var isCus = $("#is_cus").val();
            window.location = '/SoGhiNo/Index?id=' + MaKH + '&isCus=' + isCus;
        } else {
            alert("Không tìm thấy khách hàng ");
        }
     
    });
    // load khach hang no khi start
    $('#paginationholder').html('');
    $('#paginationholder').html('<ul id="pagination" class="pagination-sm"></ul>');
    loadKhachHangNo();
});
  
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
                $('#tong_so_No_cus').text('Số nợ : ' + getTextSoDu(cus.SoNo));
                $('.add_row').remove();
                for (var i = 0; i < model.length; i++) {
                    var html = '<tr class="add_row"><td>' + model[i].MaHDNo + '</td><td>' + model[i].MaHDBan + '</td ><td class="text-center">' + model[i].TongTien + '</td><td class="text-center">' + model[i].ThanhToan + '</td><td class="text-center">' + getTextSoDu(model[i].SoDu_No) + '  </td><td class="text-center">' + getTextBu(model[i].daThanhToanSau) + '</td><td id="tong" class="text-center">' + getTextSoDu(model[i].ConNo) + '</td><td id="tong" class="text-center">' + model[i].Ngay + ' </td><td class="text-center"><button  id="detail_invoice" type="button" class="btn btn-info" width="50%"><i class="fas fa-trash"></i> Xem chi tiết hóa đơn </button></td></tr>';
                    table.append(html);
                }
                paging(data.total, 1);
            },
            fail: function () {
                alert("Không tìm thấy khách hàng ");
            }
        });
    } else {
        alert("Không tìm thấy khách hàng ");
    }
}
function paging(totalRow, cus) {

    $('#pagination').twbsPagination({
        initiateStartPageClick: false,
        totalPages: Math.ceil(totalRow / homeConfig.pageSize),
        visiblePages: 10,
        onPageClick: function (event, page) {
            homeConfig.pageIndex = page;
            if (cus == 1) loadKhachHangNo();
            else if (cus == 0) loadCongTyNo();
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
function loadKhachHangNo() {
   
    var table = $("#table_field");
        $.ajax({
            url: '/SoGhiNo/LoadKhachHangNo',
            // pass product id 
            data: { page: homeConfig.pageIndex, pageSize: 3 },
            dataType: "json",
            type: "POST",
            success: function (data) {
                var model = JSON.parse(data.model);
                $('.add_row').remove();
                for (var i = 0; i < model.length; i++) {
                    var html = '<tr class="add_row"><td>' + model[i].Ma + '</td><td>' + model[i].Ten + '</td ><td class="text-center">' + model[i].DiaChi + '</td><td class="text-center">' + model[i].STK + '</td><td class="text-center">' + model[i].SoDienThoai + '  </td><td class="text-center">' + getTextSoDu(model[i].SoNo) + '</td><td class="text-center"><button  id="detail_user" type="button" class="btn btn-info" width="50%"><i class="fas fa-info-circle"></i> Xem thông tin chi tiết</button></td></tr>';
                    table.append(html);
                }
                paging(data.total, 1);
            },
            fail: function () {
                alert("Không tìm thấy khách hàng ");
            }
        });
}
function loadCongTyNo() {

    var table = $("#table_field");
    $.ajax({
        url: '/SoGhiNo/LoadCongTyNo',
  
        data: { page: homeConfig.pageIndex, pageSize: 3 },
        dataType: "json",
        type: "POST",
        success: function (data) {
            var model = JSON.parse(data.model);
            $('.add_row').remove();
            for (var i = 0; i < model.length; i++) {
                var html = '<tr class="add_row"><td>' + model[i].Ma + '</td><td>' + model[i].Ten + '</td ><td class="text-center">' + model[i].DiaChi + '</td><td class="text-center">' + model[i].STK + '</td><td class="text-center">' + model[i].SoDienThoai + '  </td><td class="text-center">' + getTextSoDu(model[i].SoNo) + '</td><td class="text-center"><button  id="detail_user" type="button" class="btn btn-info" width="50%"><i class="fas fa-info-circle"></i> Xem thông tin chi tiết  </button></td></tr>';
                table.append(html);
            }
            paging(data.total, 0);
        },
        fail: function () {
            alert("Không tìm thấy công ty ");
        }
    });
}

