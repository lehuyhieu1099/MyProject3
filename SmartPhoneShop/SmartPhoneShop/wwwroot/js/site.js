var site = {} || site;
var CategoryId = 0;
site.Change = function (id) {
    $.ajax({
        url: `/Brand/${id}`,
        method: "GET",
        dataType: "json",
        success: function (data) {
            $('#BrandId').empty();
            $.each(data.brandbyId, function (i, v) {
                $('#BrandId').append(
                    `
                       <option value="${v.brandId}">${v.brandName
                    }</option>        	
                     `
                );
            });
        }
    });
};
