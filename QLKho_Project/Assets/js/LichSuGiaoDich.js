$(document).ready(function () {
    $('#so_ghi_no').on('click', function () {
        var IsCus = $('#is_cus').val();
        var khId = $('#id_cus').val();
        window.location = '/SoGhiNo/Index?id=' + khId + '&isCus=' + IsCus;
    });

});