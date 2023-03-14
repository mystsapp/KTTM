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
            url: "/KVCTPTCs/TinhSoTien",
            data: { soTienNT: soTienNT, tyGia: tyGia },
            dataType: "json",
            success: function (response) {
                if (response.status) {
                    soTien = response.soTien;
                    $('#txtSoTien').val(numeral(soTien).format('0,0')); // lam tron -> theo anh SonKT
                    //$('#txtSoTien').val(numeral(soTien).format('0,0.00')); // so le -> theo thao, chau
                    
                    //$('#txtSoTien').val(numeral(soTien).format('0.0[0000]'));
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
            url: '/KVCTPTCs/Get_DienGiai_By_TkNo_TkCo',
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
    AutoSgtcode: function (param) {
        $.get('/KVCTPTCs/AutoSgtcode', { param: param }, function (response) {
            if (response.status) {
                $('#txtSgtcode').val(response.data);
            }
        })
    },
    DdlTkNo: function (tkNo) {

        $.ajax({
            url: '/KVCTPTCs/Get_TenTk_By_Tk',
            type: 'GET',
            data: {
                tk: tkNo
            },
            dataType: 'json',
            success: function (response) {
                //var data = JSON.parse(response.data);
                // console.log(data);
                $('#txtTenTkNo').val(response.data);
            }
        });

    },
    DdlTkCo: function (tkCo) {

        $.ajax({
            url: '/KVCTPTCs/Get_TenTk_By_Tk',
            type: 'GET',
            data: {
                tk: tkCo
            },
            dataType: 'json',
            success: function (response) {
                //var data = JSON.parse(response.data);
                //console.log(data);
                $('#txtTenTkCo').val(response.data);
            }
        });

    },
    DdlLoaiTien: function (loaiTien, loaiPhieu) {
        if (loaiTien == 'VND') {
            if (loaiPhieu == 'T') {
                $('#ddlTkNo').val("1111000000");
                $('#ddlTkNo').trigger('change.select2');
                
            }
            else {
                $('#ddlTkCo').val("1111000000");
                $('#ddlTkCo').trigger('change.select2');
                
            }
        }
        
    },
    TxtVAT_Blur: function (vat, soTien) {

        $.ajax({
            type: "GET",
            url: "/KVCTPTCs/TinhDsKhongVat",
            data: { vat: vat, soTien: soTien },
            dataType: "json",
            success: function (response) {
                if (response.status) {
                    soTien = response.soTien;
                    $('#txtDSKhongVAT').val(numeral(soTien).format('0,0'));
                }
                else {
                    toastr.error(response.message);

                }
            }
        });

    }

};
serviceController.init();