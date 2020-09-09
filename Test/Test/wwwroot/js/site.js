var site = {} || site;
var TinhThanhId = 0;
var QuanHuyenId = 0;
site.TinhThanhChange = function ( id) {
    TinhThanhId = $('#ProvinceId').val();
    $.ajax({
        url: `/Huyen/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $('#DistrictId').empty();
            $.each(data.districtsById, function (i, v) {
                $('#DistrictId').append(
                    `
                       <option value="${v.districtId}">${v.name}</option>        	
                     `
                );
            });
        }
    });
};
site.QuanHuyenChange = function (id) {
    QuanHuyenId = $('#DistrictId').val();
    $.ajax({
        url: `/Xa/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $('#WardId').empty();
            $.each(data.wardsById, function (i, v) {
                $('#WardId').append(
                    `
                       <option value="${v.wardId}">${v.name}</option>        	
                     `
                );
            });
        }
    });
};




