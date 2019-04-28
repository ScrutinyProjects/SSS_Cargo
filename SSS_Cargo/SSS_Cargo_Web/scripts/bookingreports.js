function getBookingReportsData() {

    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();
        $.ajax({
            type: "GET",
            url: apiurl + "api/reports/" + loginid + "/" + counterid + "/getbookingreportsdata?requesttype=" + null,
            dataType: "json",
            success: function (data) {
                if (data) {
                    if (data.Locations && data.Locations.length > 0) {
                        for (var i = 0; i < data.Locations.length; i++) {
                            var option = '<option value="' + data.Locations[i].CounterId + '">' + data.Locations[i].CounterName + '</option>';
                            $('#ddlLocation').append(option);
                        }
                    }
                    if (data.GCTypes && data.GCTypes.length > 0) {
                        for (var i = 0; i < data.GCTypes.length; i++) {
                            var option = '<option value="' + data.GCTypes[i].GCTypeId + '">' + data.GCTypes[i].GCType + '</option>';
                            $('#ddlGCType').append(option);
                        }
                    }
                    if (data.BookingStatus && data.BookingStatus.length > 0) {
                        for (var i = 0; i < data.BookingStatus.length; i++) {
                            var option = '<option value="' + data.BookingStatus[i].BookingStatusId + '">' + data.BookingStatus[i].BookingStatus + '</option>';
                            $('#ddlBookingStatus').append(option);
                        }
                    }
                }
                hideloading();
            },
            error: function (xhr) {
                hideloading();
                showerroralert(xhr.responseText);
            }
        });
    }
    else {
        return false;
    }
}