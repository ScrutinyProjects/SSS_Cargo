var lstBookingStatus = [];
function getBookingReportsData() {

    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();


        $.ajax({
            type: "GET",
            url: apiurl + "api/reports/" + loginid + "/" + counterid + "/getbookingreportsdata?requesttype=" + $("#hiddenrequesttype").val(),
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
                        lstBookingStatus = data.BookingStatus;
                        lstBookingStatus.splice(0, 1);
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
function GetBookingReport() {
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


        if (isvalid) {

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
                                '<td>' + '<span id="spnSNo_' + i + '">' + (i + 1) + '</span></td>' +
                                '<td><span id="spnGCNo_' + i + '">' + data[i].GC_No + '</span></td>' +
                                '<td><span id="spnGC_Type_' + i + '">' + data[i].GC_Type + '</span></td>' +
                                '<td><span id="spnFromLocation_' + i + '" >' + data[i].FromLocation + '</span></td>' +
                                '<td><span id="spnToLocation_' + i + '"  >' + data[i].ToLocation + '</span></td>' +
                                '<td><span id="spnRouteInfo_' + i + '"  >' + data[i].RouteInfo + '</span></td>' +
                                '<td><span id="spnProductType_' + i + '"  >' + data[i].ProductType + '</span></td>' +
                                '<td><span id="spnPieces_' + i + '" >' + data[i].Pieces + '</span></td>' +
                                '<td><span id="spnWeightInfo_' + i + '"  >' + data[i].WeightInfo + '</span></td>' +
                                 '<td><span id="spnSenderName_' + i + '"  >' + data[i].SenderName + '</span></td>' +
                                '<td><span id="spnSenderMobileNumber_' + i + '"  >' + data[i].SenderMobileNumber + '</span></td>' +
                                '<td><span id="spnReceiverName_' + i + '"  >' + data[i].ReceiverName + '</span></td>' +
                                '<td><span id="spnReceiverMobileNumber_' + i + '"  >' + data[i].ReceiverMobileNumber + '</span></td>' +
                                '<td><span id="spnCurrentStatus_' + i + '"  >' + data[i].CurrentStatus + '</span></td>' +
                                '<td><span id="spnBillAmount_' + i + '"  >' + data[i].BillAmount + '</span></td>' +
                                '<td><span id="spnBookedBy_' + i + '"  >' + data[i].BookedBy + '</span></td>' +
                                 '<td><span id="spnBookedDateTime_' + i + '"  >' + moment(data[i].BookedDateTime).format("YYYY-MM-DD HH:mm") + '</span></td>' +

                                '<td><a href="javascript:void(0)" onclick="viewbillbreakup(' + data[i].BookingId + ')"><i class="fa fa-remove"></i> View Bill Breakup</a> <a href="javascript:void(0)" onclick="updatestatus(' + data[i].BookingId + ', '+ i +')"><i class="fa fa-remove"></i> Update Status</a></td>');
                            //<a href="javascript:void(0)" onclick="editparcelitem(this)"><i class="fa fa-pencil"></i> Edit</a>&nbsp;&nbsp;
                            $('#tbodybookingrecords').append(tr);
                        }

                    }
                    else {
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

function viewbillbreakup(bookingId) {
    debugger;
    $.ajax({
        type: "GET",
        url: apiurl + "api/reports/bookingpricedetails?bookingId=" + bookingId,
        dataType: "json",
        success: function (data) {
            if (data) {
                debugger;
                $("#spnBasicfright").html(data.BasicFrieght);
                $("#spnHamali").html(data.Hamali);
                $("#spnSC60").html(data.SC60);
                $("#spnValueSC").html(data.ValueSC);
                $("#spnStatCharges").html(data.StatCharges);
                $("#spnTranshipmentCharges").html(data.TranshipmentCharges);
                $("#spnAOC").html(data.AOC);
                $("#spnCollectionCharges").html(data.CollectionCharges);
                $("#spnDeliveryCharges").html(data.DeliveryCharges);
                $("#spnWithPASS").html(data.WithPASS);
                $("#spnGST5").html(data.GST5);
                $("#spnTotalAmount").html(data.TotalAmount);
                $("#spnDocketCharges").html(data.DocketCharges);
                $("#spnPickupCharges").html(data.PickupCharges);
                $("#spnLocationPickupCharges").html(data.LocationPickupCharges);
                $("#spnLocationDeliveryCharges").html(data.LocationDeliveryCharges);
                $("#spnDoorDeliveryCharges").html(data.DoorDeliveryCharges);
                $("#spnSubTotal").html(data.SubTotal);
                $("#spnDiscountAmount").html(data.DiscountAmount);
                $("#spnTotalAmountAfterDiscount").html(data.TotalAmountAfterDiscount);
                $("#spnRoundOffAmount").html(data.RoundOffAmount);
                $("#spnGrandTotal").html(data.GrandTotal);
                $("#spnDriverCharges").html(data.DriverCharges);
                $("#spnToPayCharges").html(data.ToPayCharges);

                $("#BillBreakUpModal").modal('show');

                $("#btnCloseBillBreakUpModal, #btnBillBreakUpModalCancel").unbind().click(function () {
                    $("#BillBreakUpModal").modal('hide');
                });
            }
            hideloading();
        },
        error: function (xhr) {
            hideloading();
            showerroralert(xhr.responseText);
        }
    });

   
}
function updatestatus(bookingId, index) {
    debugger;
    if (lstBookingStatus && lstBookingStatus.length > 0 && $('#ddlLatestBookingStatus option').length == 1) {
        for (var i = 0; i < lstBookingStatus.length; i++) {
            var option = '<option value="' + lstBookingStatus[i].BookingStatusId + '">' + lstBookingStatus[i].BookingStatus + '</option>';
            $('#ddlLatestBookingStatus').append(option);
        }
    }
    $("#btnUpdateStatus").attr('data-Id', bookingId + "~" + index);
    $("#UpdateStatusModal").modal('show');
    $("#btnUpdateStatusCancel, #btnCloseUpdateStatusModal").unbind().click(function () {
        $("#UpdateStatusModal").modal('hide');
    });
}
function UpdateBookingStatus() {

    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();
        
        var bookingId = $("#btnUpdateStatus").attr('data-Id').split('~')[0];
        var index =parseInt($("#btnUpdateStatus").attr('data-Id').split('~')[1]);
        var bookingStatusId = $("#ddlLatestBookingStatus").val();
        var latestRemarks = $("#txtEditRemarks").val();
        var isvalid = true;
        if (validatedropdown(bookingStatusId, $('#spnLatestBookingStatus'), 'Please select Booking Status') == false) {
            isvalid = false;
        }
        if (validatetextbox(latestRemarks, $('#spnEditRemarks'), 'Please enter edit remarks') == false) {
            isvalid = false;
        }

        if (isvalid) {
            var input = {
                LoginId: loginid,
                CounterId: counterid,
                BookingId: bookingId,
                BookingStatusId: bookingStatusId,
                LatestRemarks: latestRemarks,
            }
            $.ajax({
                type: "POST",
                data: (input),
                url: apiurl + "api/reports/updatebookingstatus",
                dataType: "json",
                success: function (data) {
                    if (data) {
                        $("#spnMessage").html('Booking Status Updated Successfully');
                        $("#spnMessage").css("display", "");
                        $("#spnMessage").css('color', 'green');
                        $("#ddlLatestBookingStatus").val(-1);
                        $("#txtEditRemarks").val('');
                        $("#spnCurrentStatus_" + index).html(lstBookingStatus.filter(k=>k.BookingStatusId == bookingStatusId)[0].BookingStatus);
                    }
                    else {
                        $("#spnMessage").html('Failed to update status');
                        $("#spnMessage").css('color', 'red');
                        $("#spnMessage").css("display", "");
                    }
                    $("#btnUpdateStatusCancel").click();
                    hideloading();
                },
                error: function (xhr) {
                    hideloading();
                    showerroralert(xhr.responseText);
                }
            });
        }
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
