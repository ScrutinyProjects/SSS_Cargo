var lstBookingStatus = [];
function getBookingReportsData() {

    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();


        $.ajax({
            type: "GET",
            url: apiurl + "api/reports/" + loginid + "/" + counterid + "/getnotloadedreportsdata?requesttype=" + $("#hiddenrequesttype").val(),
            dataType: "json",
            success: function (data) {
                if (data) {
                    if (data.Locations && data.Locations.length > 0) {
                        for (var i = 0; i < data.Locations.length; i++) {
                            var option = '<option value="' + data.Locations[i].CounterId + '">' + data.Locations[i].CounterName + '</option>';
                            $('#ddlLocation').append(option);
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
function GetNotLoadedReport() {
    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();
        var fromDate = $("#txtFromDate").val();
        var toDate = $("#txtToDate").val();
        var locationId = $("#ddlLocation").val();

        var isvalid = true;
        if (validatetextbox(fromDate, $('#spnFromDate'), 'Please select from date') == false) {
            isvalid = false;
        }
        if (validatetextbox(toDate, $('#spnToDate'), 'Please select to date') == false) {
            isvalid = false;
        }
        if (validatedropdown(locationId, $('#spnLocation'), 'Please select location') == false) {
            isvalid = false;
        }


        if (isvalid) {

            var input = {};
            input = {
                LoginId: loginid,
                CounterId: counterid,
                FromDate: fromDate,
                ToDate: toDate,
                LocationId: locationId,
            };
            $.ajax({
                type: "POST",
                data: (input),
                url: apiurl + "api/reports/getnotloadedreport",
                dataType: "json",
                success: function (data) {
                    if (data && data.Table && data.Table.length > 0) {
                        $('#tbodynotloadedrecords').html('');
                        for (var i = 0; i < data.Table.length; i++) {
                            var tr = $('<tr class="trdynamic" />');
                            tr.append('' +
                                '<td>' + '<span id="spnSNo_' + i + '">' + (i + 1) + '</span></td>' +
                                '<td><span id="spnGCNo_' + i + '">' + data.Table[i].GC_No + '</span></td>' +
                                '<td><span id="spnGC_Type_' + i + '">' + data.Table[i].GC_Type + '</span></td>' +
                                '<td><span id="spnFromLocation_' + i + '" >' + data.Table[i].FromLocation + '</span></td>' +
                                '<td><span id="spnToLocation_' + i + '"  >' + data.Table[i].ToLocation + '</span></td>' +
                                '<td><span id="spnRouteInfo_' + i + '"  >' + data.Table[i].RouteInfo + '</span></td>' +
                                '<td><span id="spnProductType_' + i + '"  >' + data.Table[i].ProductType + '</span></td>' +
                                '<td><span id="spnPieces_' + i + '" >' + data.Table[i].Pieces + '</span></td>' +
                                '<td><span id="spnWeightInfo_' + i + '"  >' + data.Table[i].WeightInfo + '</span></td>' +
                                '<td><span id="spnCurrentStatus_' + i + '"  >' + data.Table[i].CurrentStatus + '</span></td>' +
                                '<td><span id="spnBookedBy_' + i + '"  >' + data.Table[i].BookedBy + '</span></td>' +
                                 '<td><span id="spnBookedDateTime_' + i + '"  >' + moment(data.Table[i].BookedDateTime).format("YYYY-MM-DD HH:mm") + '</span></td>'); // +

                                //'<td><a href="javascript:void(0)" onclick="viewbillbreakup(' + data[i].BookingId + ')"><i class="fa fa-remove"></i> View Bill Breakup</a> <a href="javascript:void(0)" onclick="updatestatus(' + data[i].BookingId + ', ' + i + ')"><i class="fa fa-remove"></i> Update Status</a></td>');
                            //<a href="javascript:void(0)" onclick="editparcelitem(this)"><i class="fa fa-pencil"></i> Edit</a>&nbsp;&nbsp;
                            $('#tbodynotloadedrecords').append(tr);
                        }

                    }
                    else {
                        var tr = $('<tr class="trdynamic" />');
                        tr.append('<td colspan = "13" No Reocrds Found </td>');
                        $('#tbodynotloadedrecords').append(tr);
                    }
                    hideloading();
                },
                error: function (xhr) {
                    hideloading();
                    showerroralert(xhr.responseText);
                }
            });
        }
    }
    else {
        return false;
    }
}

function validatetextbox(value, span, message) {
    $(span).html('');
    if (value == "") {
        $(span).html(message);
        return false;
    }
}

function validatedropdown(value, span, message) {
    $(span).html('');
    if (value == "-1" || value == "") {
        $(span).html(message);
        return false;
    }
}
