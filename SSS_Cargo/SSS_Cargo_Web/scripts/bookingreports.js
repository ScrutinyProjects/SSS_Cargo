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
function GetBookingReport()
{
    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();
        var fromDate = $("#txtFromDate").val();
        var toDate = $("#txtToDate").val();
        var locationId = $("#ddlLocation").val();
        var gcTypeId = $("#ddlGCType").val();
        var bookingStatusId = $("#ddlBookingStatus").val();


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
        if (validatedropdown(gcTypeId, $('#spnGCType'), 'Please select GC type') == false) {
            isvalid = false;
        }
        if (validatedropdown(bookingStatusId, $('#spnBookingStatus'), 'Please select booking status') == false) {
            isvalid = false;
        }
       

        if(isvalid)
        {

            var input = {};
            input = {
                LoginId: loginid,
                CounterId: counterid,
                FromDate: fromDate,
                ToDate: toDate,
                LocationId: locationId,
                GCTypeId: gcTypeId,
                BookingStatusId: bookingStatusId,
            };
            $.ajax({
                type: "POST",
                data: (input),
                url: apiurl + "api/reports/getbookingreport",
                dataType: "json",
                success: function (data) {
                    if (data && data.length > 0) {
                        $('#tbodybookingrecords').html('');
                        for (var i = 0; i < data.length; i++) {
                            var tr = $('<tr class="trdynamic" />');
                            tr.append('' +
                                '<td>' + '<span id="spnSNo">' + i+1 + '</span></td>' +
                                '<td><span id="spnGCNo">' + data[i].GC_No + '</span></td>' +
                                '<td><span id="spnGC_Type">' + data[i].GC_Type + '</span></td>' +
                                '<td><span id="spnFromLocation" >' + data[i].FromLocation + '</span></td>' +
                                '<td><span id="spnToLocation"  >' + data[i].ToLocation + '</span></td>' +
                                '<td><span id="spnRouteInfo"  >' + data[i].RouteInfo + '</span></td>' +
                                '<td><span id="spnProductType"  >' + data[i].ProductType + '</span></td>' +
                                '<td><span id="spnPieces"  >' + data[i].Pieces + '</span></td>' +
                                '<td><span id="spnWeightInfo"  >' + data[i].WeightInfo + '</span></td>' +
                                 '<td><span id="spnSenderName"  >' + data[i].SenderName + '</span></td>' +
                                '<td><span id="spnSenderMobileNumber"  >' + data[i].SenderMobileNumber + '</span></td>' +
                                '<td><span id="spnReceiverName"  >' + data[i].ReceiverName + '</span></td>' +
                                '<td><span id="spnReceiverMobileNumber"  >' + data[i].ReceiverMobileNumber + '</span></td>' +
                                '<td><span id="spnCurrentStatus"  >' + data[i].CurrentStatus + '</span></td>' +
                                '<td><span id="spnBillAmount"  >' + data[i].BillAmount + '</span></td>' +
                                '<td><span id="spnBookedBy"  >' + data[i].BookedBy + '</span></td>' +
                                 '<td><span id="spnBookedDateTime"  >' + moment(data[i].BookedDateTime).format("YYYY-MM-DD HH:mm") + '</span></td>' +
                               
                                '<td><a href="javascript:void(0)" onclick="viewbillbreakup(' + data[i].BookingId + ')"><i class="fa fa-remove"></i> View Bill Breakup</a> <a href="javascript:void(0)" onclick="updatestatus(' + data[i].BookingId + ')"><i class="fa fa-remove"></i> Update Status</a></td>');
                            //<a href="javascript:void(0)" onclick="editparcelitem(this)"><i class="fa fa-pencil"></i> Edit</a>&nbsp;&nbsp;
                            $('#tbodybookingrecords').append(tr);
                        }
                       
                    }
                    else
                    {
                        var tr = $('<tr class="trdynamic" />');
                        tr.append('<td colspan = "18" No Reocrds Found </td>');
                        $('#tbodybookingrecords').append(tr);
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

function viewbillbreakup(BookingId)
{
    debugger;
    $("#BillBreakUpModal").modal('show');
    $("#btnCloseBillBreakUpModal, #btnBillBreakUpModalCancel").unbind().click(function () {
        $("#BillBreakUpModal").modal('hide');
    });
}
function updatestatus(BookingId)
{
    debugger;
    $("#UpdateStatusModal").modal('show');
    $("#btnUpdateStatusCancel, #btnCloseUpdateStatusModal").unbind().click(function () {
        $("#UpdateStatusModal").modal('hide');
    });
}
function UpdateBookingStatus(BookingId)
{

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