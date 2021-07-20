function addCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}

var serviceController = {
    init: function () {

        toastr.options = { // toastr options
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "2000",
            "timeOut": "2000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        serviceController.registerEvent();
    },

    registerEvent: function () {

        // format .numbers
        $('input.numbers').keyup(function (event) {

            // Chỉ cho nhập số
            if (event.which >= 37 && event.which <= 40) return;

            $(this).val(function (index, value) {
                return addCommas(value);
            });
        });

    },
    TinhSoTien: function () {
        
        soTienNT = $('#txtSoTienNT').val();
        tyGia = $('#txtTyGia').val();

        $.ajax({
            type: "GET",
            url: "/KVCTPCTs/TinhSoTien",
            data: { soTienNT: soTienNT, tyGia: tyGia },
            dataType: "json",
            success: function (response) {
                if (response.status) {
                    soTien = response.soTien;
                    $('#txtSoTien').val(numeral(soTien).format('0,0'));
                }
                else {
                    toastr.error(response.message);

                }
            }
        });

    },
    Get_DienGiai_By_TkNo_TkCo: function (tkNo, tkCo) {
        $('#ddlDienGiai').html('');
        var option = '';

        $.ajax({
            url: '/KVCTPCTs/Get_DienGiai_By_TkNo_TkCo',
            type: 'GET',
            data: {
                tkNo: tkNo,
                tkCo: tkCo
            },
            dataType: 'json',
            success: function (response) {

                if (response.status) {

                    var data = response.data;

                    $.each(data, function (i, item) {
                        option = option + '<option value="' + item.dienGiai + '">' + item.dienGiai + '</option>'; //chinhanh1

                    });

                    $('#ddlDienGiai').html(option);
                }

            }
        });
    },

    KhachHangSearch: function(code) {
        $.get('/KVCTPCTs/GetKhachHangs_HDVATOB_By_Code', { code: code }, function (data) {

            $('#khachHang_Modal').modal('show');
            $('.khachHang_Modal_Body').html(data);
            $('#khachHang_Modal').draggable();
        });
        }

    
};
serviceController.init();