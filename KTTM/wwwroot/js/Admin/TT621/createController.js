////function addCommas(x) {
////    var parts = x.toString().split(".");
////    parts[0] = parts[0].replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
////    return parts.join(".");
////}

var createController = {
    init: function () {

        //toastr.options = { // toastr options
        //    "closeButton": false,
        //    "debug": false,
        //    "newestOnTop": false,
        //    "progressBar": true,
        //    "positionClass": "toast-top-right",
        //    "preventDuplicates": false,
        //    "onclick": null,
        //    "showDuration": "300",
        //    "hideDuration": "2000",
        //    "timeOut": "2000",
        //    "extendedTimeOut": "1000",
        //    "showEasing": "swing",
        //    "hideEasing": "linear",
        //    "showMethod": "fadeIn",
        //    "hideMethod": "fadeOut"
        //};

        createController.registerEvent();
    },

    registerEvent: function () {

        //// format .numbers
        //$('input.numbers').keyup(function (event) {

        //    // Chỉ cho nhập số
        //    if (event.which >= 37 && event.which <= 40) return;

        //    $(this).val(function (index, value) {
        //        return addCommas(value);
        //    });
        //});

        $('.tamUngTr').click(function () {
            debugger
            tamUngId = $(this).data('id');
            createController.GetTT621s_By_TamUng(tamUngId);
        })

        // giu trang thai CT TT va lay tamungid
        $('.tamUngTr').off('click').on('click', function () {
            
            if ($(this).hasClass("hoverClass"))
                $(this).removeClass("hoverClass");
            else {
                $('.tamUngTr').removeClass("hoverClass");
                $(this).addClass("hoverClass");
            }

            $('#btnThemMoiCT').attr('disabled', false);
            tamUngId = $(this).data('id');
            $('#hidTamUngId').val(tamUngId);

        });
        // giu trang thai CT TT va lay tamungid

        // btnThemMoiCT
        $('#btnThemMoiCT').off('click').on('click', function () {

            tamUngId = $('#hidTamUngId').val();
            var url = '/TT621s/ThemMoiCT_TT_Partial';

            $.get(url, { tamUngId: tamUngId }, function (data) {
                
                $('.ThemMoiCT_TT_Body').html(data);
                $('#ThemMoiCT_TT_Modal').modal('show');
            })
        })
        // btnThemMoiCT
    },

    GetTT621s_By_TamUng: function (tamUngId) {

        $.ajax({
            url: '/TT621s/GetTT621s_By_TamUng',
            type: 'GET',
            data: {
                tamUngId: tamUngId
            },
            dataType: 'json',
            success: function (response) {
                console.log(response);
                if (response.status) {
                    var data = response.data; 
                    var html = '';
                    var template = $('#data-template').html();

                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            DienGiai: item.dienGiai,
                            DienGiaiP: item.dienGiaiP,
                            SoTienNT: numeral(item.soTienNT).format('0,0'),
                            LoaiTien: item.loaiTien,
                            TyGia: numeral(item.tyGia).format('0,0'),
                            SoTien: numeral(item.soTien).format('0,0'),
                            TKNo: item.tKNo,
                            MaKhNo: item.maKhNo,
                        });

                    });

                    $('#ctThanhToanBody').html(html);

                }
            }
        });
    }
    
};
createController.init();