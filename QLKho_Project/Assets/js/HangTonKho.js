
$(document).ready(function () {
    $(".sua_sl_Hong").click(function () {
        var maSp = $(this).closest('tr').find("td:eq(0)").html();
        $('.row_detail').remove();
        var tenSp = $(this).closest('tr').find("td:eq(1)").html();
        var motaSp = $(this).closest('tr').find("td:eq(2)").html();
        $("#ten_sp").text(tenSp);
        $("#mota_sp").text(motaSp);
        $("#ma_sp").text(maSp);
    $('#detail_model').modal('show');
    });
    //$("#table_field").on('click', '.sua_sl_Hong', function () {
       
    //    var maSp = $(".sua_sl_Hong").closest('tr').find("td:eq(0)").html();
    //    $('.row_detail').remove();
    //    var tenSp = $(".sua_sl_Hong").closest('tr').find("td:eq(1)").html();
    //    var motaSp = $(".sua_sl_Hong").closest('tr').find("td:eq(2)").html();
    //    $("#ten_sp").text(tenSp);
    //    $("#mota_sp").text(motaSp);
    //    $("#ma_sp").text(maSp);
    //$('#detail_model').modal('show');


    //});

    $("#save_slhong").on('click', function () {
        var slXuLi = $("#change_sl_hong").val();
        var slHong = 0;
        var maSp = $("#ma_sp").text();
         $.ajax({
        url: '/HangTonKho/getSlHongByID',
        // pass product id 
        data: { spid: maSp },
        dataType: "json",
        type: "GET",
             success: function (res) {
                
                 slHong = res.data;
                 if (slHong > 0 && slXuLi <= slHong) {
                     window.location = '/HangTonKho/doSomthing?spid=' + maSp + '&slXuLi=' + slXuLi;
                 } else {
                     alert("Số lượng xử lí ko đc âm hoặc lớn hơn số lượng hỏng !");
                 }
        },
        fail: function () {
            alert("Không tìm thấy khách hàng ");

             }
        
         });
      
    });



});