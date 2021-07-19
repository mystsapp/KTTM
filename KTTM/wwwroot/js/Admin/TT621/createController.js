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

        // giu trang thai CT TT va lay tamungid (GetTT621s_By_TamUng)
        $('.tamUngTr').off('click').on('click', function () {

            if ($(this).hasClass("hoverClass"))
                $(this).removeClass("hoverClass");
            else {
                $('.tamUngTr').removeClass("hoverClass");
                $(this).addClass("hoverClass");
            }

            $('#btnThemMoiCT').attr('disabled', false);
            tamUngId = $(this).data('id');
            $('#hidTamUngId').val(tamUngId); // for TT141

            // gang' commentText khi lick tamung
            soTienNT = $('#txtSoTienNT_Create').val(); // TT621Create_View
            $.get('/TT621s/GetCommentText_By_TamUng', { tamUngId: tamUngId, soTienNT: soTienNT }, function (response) {
                $('#txtCommentText').val(response);
            })

            // Check_KetChuyenBtnStatus
            createController.Check_KetChuyenBtnStatus(tamUngId, soTienNT);

            // gang' ds TT141 theo tamung
            createController.GetTT621s_By_TamUng(tamUngId);
        });
        // giu trang thai CT TT va lay tamungid (GetTT621s_By_TamUng)

        // btnThemMoiCT
        $('#btnThemMoiCT').off('click').on('click', function () {

            tamUngId = $('#hidTamUngId').val();
            kVCTPCTId_PhieuTC = $('#hidKVCTPCTId').val();
            var url = '/TT621s/ThemMoiCT_TT_Partial';

            $.get(url, { tamUngId: tamUngId, kVCTPCTId_PhieuTC: kVCTPCTId_PhieuTC }, function (data) {

                $('.ThemMoiCT_TT_Body').html(data);
                $('#ThemMoiCT_TT_Modal').modal('show');
                $('#ThemMoiCT_TT_Modal').draggable();
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
                var data = response.data;
                var html = '';
                var template = $('#data-template').html();

                if (response.status) {

                    $.each(data, function (i, item) {

                        html += Mustache.render(template, {
                            DienGiai: item.dienGiai,
                            DienGiaiP: item.dienGiaiP,
                            SoTienNT: numeral(item.soTienNT).format('0,0'),
                            LoaiTien: item.loaiTien,
                            TyGia: numeral(item.tyGia).format('0,0'),
                            SoTien: numeral(item.soTien).format('0,0'),
                            TKNo: item.tKNo,
                            MaKhNo: item.maKhNo
                        });

                    });

                    $('#ctThanhToanBody').html(html);

                }
                else {

                    //html = Mustache.render(template, {
                    //    DienGiai: '',
                    //    DienGiaiP: '',
                    //    SoTienNT: '',
                    //    LoaiTien: '',
                    //    TyGia: '',
                    //    SoTien: '',
                    //    TKNo: '',
                    //    MaKhNo: '',
                    //});

                    $('#ctThanhToanBody').html('');

                }
            }
        });
    },

    Check_KetChuyenBtnStatus: function (tamUngId, soTienNT) {
        $.post('/TT621s/Check_KetChuyenBtnStatus', { tamUngId: tamUngId, soTienNT_Tren_TT621Create: soTienNT }, function (status) {

            $('#btnKetChuyen').attr('disabled', status)

        })
    }

};
createController.init();