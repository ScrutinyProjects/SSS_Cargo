var tocounterid = '';
var transhipmentpoint1 = '';
var transhipmentpoint2 = '';
var sendermobile = '';
var senderid = 0;
var receivermobile = '';
var receiverid = 0;
var measurement = 'Kgs';
var gst = 12;

function getmasters() {
    hideallalerts();

    var counterid = $("#hiddencounterid").val();
    var loginid = $("#hiddenloginid").val();

    if (counterid != "" && loginid != "") {
        showloading();

        var input = [];
        input = {
            CounterId: counterid,
            LoginId: loginid
        };

        $.ajax({
            type: "POST",
            data: (input),
            url: apiurl + "api/booking/getmasters",
            dataType: "json",
            success: function (data) {
                if (data.StatusId == 1) {
                    if (data.GCTypes != null) {
                        for (var i = 0; i < data.GCTypes.length; i++) {
                            var option = '<option value="' + data.GCTypes[i].GCTypeId + '">' + data.GCTypes[i].GCType + '</option>';
                            $('#selectgctype').append(option);
                        }
                        $('#selectgctype').val(0);
                    }
                    if (data.ParcelTypes != null) {
                        for (var i = 0; i < data.ParcelTypes.length; i++) {
                            var option = '<option value="' + data.ParcelTypes[i].ParcelTypeId + '">' + data.ParcelTypes[i].ParcelType + '</option>';
                            $('#selectpiecestype').append(option);
                        }
                    }
                    if (data.ProductTypes != null) {
                        for (var i = 0; i < data.ProductTypes.length; i++) {
                            var option = '<option value="' + data.ProductTypes[i].ProductTypeId + '">' + data.ProductTypes[i].ProductType + '</option>';
                            $('#selectproducttype').append(option);
                        }
                        $('#selectproducttype').val(1);
                    }
                    if (data.Books != null) {
                        if (data.Books.length > 0) {
                            $('#hiddenbookid').val(data.Books[0].BookId);
                        }
                    }

                    if (data.Customers != null) {
                        if (data.Customers.length > 0) {
                            var customers = [];

                            for (var i = 0; i < data.Customers.length; i++) {
                                customers.push({ label: data.Customers[i].MobileNumber });//value: data.Customers[i].CustomerId, 
                            }

                            $('#textsendermobile').autocomplete({
                                source: customers,
                                minLength: 4,
                                select: function (event, ui) {
                                    sendermobile = ui.item.label;
                                    getcustomerdetailsbymobilenumber(sendermobile, true);
                                }
                            });
                            $('#textreceivermobile').autocomplete({
                                source: customers,
                                minLength: 4,
                                select: function (event, ui) {
                                    receivermobile = ui.item.label;
                                    getcustomerdetailsbymobilenumber(receivermobile, false);
                                }
                            });
                        }
                    }

                    getreceivinglocations();
                }
                else {
                    showwarningalert(data.StatusMessage);
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

function getreceivinglocations() {
    hideallalerts();

    var counterid = $("#hiddencounterid").val();
    var loginid = $("#hiddenloginid").val();

    if (counterid != "" && loginid != "") {
        showloading();

        var input = [];
        input = {
            CounterId: counterid,
            LoginId: loginid
        };

        $.ajax({
            type: "POST",
            data: (input),
            url: apiurl + "api/booking/getreceivinglocations",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    if (data.length > 0) {
                        var counters = [];

                        for (var i = 0; i < data.length; i++) {
                            counters.push({ label: data[i].CounterName });//, value: data.Counters[i].CounterId
                        }

                        $('#textreceivercounter').autocomplete({
                            minLength: 2,
                            source: counters,
                            select: function (event, ui) {
                                tocounterid = ui.item.label;
                                $('#textreceivercounter').val(ui.item.label);
                                getroutedetails();
                            }
                        });
                        $('#textpoint1').autocomplete({
                            minLength: 2,
                            source: counters,
                            select: function (event, ui) {
                                transhipmentpoint1 = ui.item.label;
                                $('#textpoint1').val(ui.item.label);
                                getroutedetails();
                            }
                        });
                        $('#textpoint2').autocomplete({
                            minLength: 2,
                            source: counters,
                            select: function (event, ui) {
                                transhipmentpoint2 = ui.item.label;
                                $('#textpoint2').val(ui.item.label);
                                getroutedetails();
                            }
                        });
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

function getcustomerdetailsbymobilenumber(mobilenumber, issender) {
    if (mobilenumber != "") {
        showloading();

        var input = [];
        input = {
            MobileNumber: mobilenumber
        };

        $.ajax({
            type: "POST",
            data: (input),
            url: apiurl + "api/booking/getcustomerdetailsbymobilenumber",
            dataType: "json",
            success: function (data) {
                var isvaliddata = true;
                if (data.StatusId != null) {
                    if (data.StatusId == 1) {
                        if (issender) {
                            $('#textsendername').val(data.CustomerName);
                            $('#textsenderaddress').val(data.Address);
                            senderid = data.CustomerId;
                        }
                        else {
                            $('#textreceivername').val(data.CustomerName);
                            $('#textreceiveraddress').val(data.Address);
                            receiverid = data.CustomerId;
                        }
                    }
                    else {
                        isvaliddata = false;
                        showwarningalert(data.StatusMessage);
                    }
                }
                else {
                    isvaliddata = false;
                }

                if (!isvaliddata) {
                    if (issender) {
                        $('#textsendername').val('');
                        $('#textsenderaddress').val('');
                        senderid = 0;
                    }
                    else {
                        $('#textreceivername').val('');
                        $('#textreceiveraddress').val('');
                        receiverid = 0;
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

function getroutedetails() {
    var sender = $('#textsendercounter').val();
    var receiver = $('#textreceivercounter').val();
    var point1 = $('#textpoint1').val();
    var point2 = $('#textpoint2').val();
    var route = "";
    var isvalid = true;

    if (sender != "" && receiver != "") {
        route = sender
        if (point1 != "") {
            if (point1 == receiver) {
                showwarningalert("Transhipment Point 1 and Receiving Location should not be same");
            }
            else {
                route = route + " --> " + point1;
            }
        }
        if (point2 != "") {
            if (point2 == receiver) {
                showwarningalert("Transhipment Point 2 and Receiving Location should not be same");
            }
            else if (point2 == point1) {
                showwarningalert("Transhipment Point 1 and Transhipment Point 2 should not be same");
            }
            else {
                route = route + " --> " + point2;
            }
        }
        route = route + " --> " + receiver;
    }
    else {
        route = "";
    }
    $('#spanroute').html(route);
}

function showmeasurement(obj) {
    if ($(obj).val() == "4") {
        $('.labelactualweightin').html('Gms');
        measurement = 'Gms';
        gst = 18;
        $('#spangstperc').html('18%');
    }
    else {
        $('.labelactualweightin').html('Kgs');
        measurement = 'Kgs';
        gst = 12;
        $('#spangstperc').html('12%');
    }
}

function addparcelitems() {
    var actualweight = $('#textactualweight').val().trim();
    var pieces = $('#textpieces').val().trim();
    var piecestype = $('#selectpiecestype').val();
    var piecestypename = $('#selectpiecestype :selected').text();

    var isvalid = true;

    if (actualweight == "") {
        isvalid = false;
        $('#spanactualweight').html("Please enter actual weight");
    }
    else if (parseFloat(actualweight) == 0) {
        isvalid = false;
        $('#spanactualweight').html("Please enter valid actual weight");
    }
    else {
        $('#spanactualweight').html("");
    }

    if (pieces == "") {
        isvalid = false;
        $('#spanpieces').html("Please enter pieces");
    }
    else if (parseInt(pieces) == 0) {
        isvalid = false;
        $('#spanpieces').html("Please enter valid pieces");
    }
    else {
        $('#spanpieces').html("");
    }

    if (parseInt(piecestype) == 0) {
        isvalid = false;
        $('#spanpiecestype').html("Please select pieces type");
    }

    if (isvalid) {
        //var actualweightin = 'gms';
        //var totalweightin = 'gms';

        //if (actualweight >= 1000) {
        //    actualweight = (parseFloat(actualweight) / 1000).toFixed(2);
        //    actualweightin = 'kgs';
        //}
        //else {
        //    actualweight = parseFloat(actualweight);
        //}

        var totalweight = parseFloat(actualweight) * parseInt(pieces);

        //if (totalweight >= 1000 && actualweightin == 'gms') {
        //    totalweight = (parseFloat(totalweight) / 1000).toFixed(2);
        //    totalweightin = 'kgs';
        //}
        //else if (actualweightin == 'kgs') {
        //    totalweight = parseFloat(totalweight).toFixed(2);
        //    totalweightin = 'kgs';
        //}
        //else {
        //    totalweight = parseFloat(totalweight);
        //}

        if (edittrid == '') {
            if ($('#tbodyparcelitems').find('.trdynamic').length == 0) {
                $('#tbodyparcelitems tr').remove();
            }

            var tr = $('<tr class="trdynamic" />');
            tr.append('<td class="sno"></td>' +
                '<td><span id="spanactualweight" style="display:none">' + actualweight + '</span><span id="spanactualweightview">' + actualweight + '</span> <span id="spanactualweightinview">' + measurement + '</span></td>' +
                '<td><span id="spannumberofpieces">' + pieces + '</span></td>' +
                '<td><span id="spanpiecestypeid" style="display:none">' + piecestype + '</span><span id="spanpiecestypenameview">' + piecestypename + '</span></td>' +
                '<td><span id="spantotalweight" style="display:none">' + totalweight + '</span><span id="spantotalweightview">' + totalweight + '</span> <span id="spantotalweightinview">' + measurement + '</span></td>' +
                '<td><a href="javascript:void(0)" onclick="deleteparcelitem(this)"><i class="fa fa-remove"></i> Remove</a></td>');
            //<a href="javascript:void(0)" onclick="editparcelitem(this)"><i class="fa fa-pencil"></i> Edit</a>&nbsp;&nbsp;
            $('#tbodyparcelitems').append(tr);

            updatetableserialnumbers($('#tbodyparcelitems'));
        }
        else {
            var tr = $('#tbodyparcelitems').find('#' + edittrid);

            $(tr).find('#spanactualweight').html(actualweight);
            $(tr).find('#spanactualweightview').html(actualweight + ' ' + measurement);
            $(tr).find('#spannumberofpieces').html(pieces);
            $(tr).find('#spanpiecestypeid').html(piecestype);
            $(tr).find('#spanpiecestypenameview').html(piecestypename);
            $(tr).find('#spantotalweight').html(totalweight);
            $(tr).find('#spantotalweightview').html(totalweight + ' ' + measurement);

            edittrid = '';
        }

        $('#textactualweight').val('')
        $('#textpieces').val('')
        $('#selectpiecestype').val('0')
        $('#textdisplayactualweight').val('');
        $('#textcalculatedweight').val('');
        claculateweightinfo();
    }
    else {
        $('#textdisplayactualweight').val('');
        $('#textcalculatedweight').val('');
    }
}

var edittrid = '';

function editparcelitem(obj) {
    var tr = $(obj).closest('tr')[0];
    edittrid = $(tr).attr('id');
    var actualweight = $(tr).find('#spanactualweight').html();
    var numberofpieces = $(tr).find('#spannumberofpieces').html();
    var piecestypeid = $(tr).find('#spanpiecestypeid').html();
    var actualweightin = $(tr).find('#spanactualweightinview').html();
    var totalweightin = $(tr).find('#spantotalweightinview').html();

    if (actualweightin == "kgs") {
        actualweight = actualweight * 1000;
    }

    $('#textactualweight').val(actualweight);
    $('#textpieces').val(numberofpieces);
    $('#selectpiecestype').val(piecestypeid);

    updatetotalweight();
}

function deleteparcelitem(obj) {
    var tr = $(obj).closest('tr')[0];
    edittrid = $(tr).attr('id');
    $('#modaldeleteparcelitem').modal();
}

function deleteitemconfirm() {
    if (edittrid != '') {
        var tr = $('#tbodyparcelitems').find('#' + edittrid);
        $(tr).remove();
        edittrid = '';

        if ($('#tbodyparcelitems').find('.trdynamic').length == 0) {
            var tr = $('<tr />');
            tr.append('<td colspan="6" style="text-align:center">No parcel items added</td>');
            $('#tbodyparcelitems').append(tr);
        }

        updatetableserialnumbers($('#tbodyparcelitems'));
    }
    $('#closemodaldeleteparcelitem').click();
}

function updatetotalweight() {
    var actualweight = $('#textactualweight').val().trim();
    var pieces = $('#textpieces').val().trim();

    var isvalid = true;

    if (actualweight == "") {
        isvalid = false;
    }
    else if (parseFloat(actualweight) == 0) {
        isvalid = false;
    }
    else {
        $('#spanactualweight').html("");
    }

    if (pieces == "") {
        isvalid = false;
    }
    else if (parseInt(pieces) == 0) {
        isvalid = false;
    }
    else {
        $('#spanpieces').html("");
    }

    if (isvalid) {
        actualweight = parseFloat(actualweight)
        var totalweight = (parseFloat(actualweight) * parseInt(pieces));

        $('#textdisplayactualweight').val(actualweight);
        $('#textcalculatedweight').val(totalweight);
    }
    else {
        $('#textdisplayactualweight').val("0");
        $('#textcalculatedweight').val("0");
    }
}

function updatetableserialnumbers(tbody) {
    var trs = $(tbody).find('.trdynamic');

    for (var i = 0; i < trs.length; i++) {
        var td = $(trs[i]).find('.sno')[0];
        $(td).html((i + 1));
        $(trs[i]).attr("id", "trparcel" + (i + 1));
    }
}

function calculateprice() {
    hideallalerts();
    var isvalid = true;

    var counterid = $('#hiddencounterid').val().trim();
    var loginid = $('#hiddenloginid').val().trim();
    var gctype = $('#selectgctype').val().trim();
    var sendercounter = $('#textsendercounter').val().trim();
    var sendermobile = $('#textsendermobile').val().trim();
    var sendername = $('#textsendername').val().trim();
    var senderaddress = $('#textsenderaddress').val().trim();
    var receivercounter = $('#textreceivercounter').val().trim();
    var receivermobile = $('#textreceivermobile').val().trim();
    var receivername = $('#textreceivername').val().trim();
    var receiveraddress = $('#textreceiveraddress').val().trim();
    var point1 = $('#textpoint1').val().trim();
    var point2 = $('#textpoint2').val().trim();
    var producttype = $('#selectproducttype').val().trim();
    var shipmentvalue = $('#textshipmentvalue').val().trim();
    var shipmentdescription = $('#textshipmentdescription').val().trim();

    if (validatedropdown(gctype, $('#spangctypehelper'), 'Please select GC Type') == false) {
        isvalid = false;
        alert("Please select GC Type");
    }
    if (validatetextbox(sendercounter, $('#spansendercounter'), 'Please select Sending From') == false) {
        isvalid = false;
    }
    if (validatetextbox(sendermobile, $('#spansendermobile'), 'Please enter Sender Mobile Number') == false) {
        isvalid = false;
    }
    if (validatetextbox(sendername, $('#spansendername'), 'Please enter Sender Name') == false) {
        isvalid = false;
    }
    if (validatetextbox(senderaddress, $('#spansenderaddress'), 'Please enter Sender Address') == false) {
        isvalid = false;
    }
    if (validatetextbox(receivercounter, $('#spanreceivercounter'), 'Please select Receive To') == false) {
        isvalid = false;
    }
    if (validatetextbox(receivermobile, $('#spanreceivermobile'), 'Please enter Receiver Mobile Number') == false) {
        isvalid = false;
    }
    if (validatetextbox(receivername, $('#spanreceivername'), 'Please enter Receiver Name') == false) {
        isvalid = false;
    }
    if (validatetextbox(receiveraddress, $('#spanreceiveraddress'), 'Please enter Receiver Address') == false) {
        isvalid = false;
    }
    if (validatedropdown(producttype, $('#spanproducttype'), 'Please select Product Type') == false) {
        isvalid = false;
    }
    if (validatetextbox(shipmentvalue, $('#spanshipmentvalue'), 'Please enter Shipment Value') == false) {
        isvalid = false;
    }
    if (validatetextbox(shipmentdescription, $('#spanshipmentdescription'), 'Please enter Description') == false) {
        isvalid = false;
    }
    if ($('#tbodyparcelitems').find('.trdynamic').length == 0) {
        isvalid = false;
        alert("Please enter atleast one parcel");
    }

    if (sendercounter != "" && receivercounter != "") {
        if (point1 != "") {
            if (point1 == receivercounter) {
                isvalid = false;
                showwarningalert("Transhipment Point 1 and Receiving Location should not be same");
            }
        }
        if (point2 != "") {
            if (point2 == receivercounter) {
                isvalid = false;
                showwarningalert("Transhipment Point 2 and Receiving Location should not be same");
            }
            else if (point2 == point1) {
                isvalid = false;
                showwarningalert("Transhipment Point 1 and Transhipment Point 2 should not be same");
            }
        }
    }

    if (isvalid) {
        showloading();

        var transhipmentpoints = '';

        if (point1 != "") {
            transhipmentpoints = point1;
        }
        if (point2 != "") {
            transhipmentpoints = (transhipmentpoints == "") ? point2 : transhipmentpoints + "," + point2;
        }

        var parcelitems = [];

        var trs = $('#tbodyparcelitems').find('.trdynamic');

        for (var i = 0; i < trs.length; i++) {
            var actualweight = $(trs[i]).find('#spanactualweight').html();
            var numberofpieces = $(trs[i]).find('#spannumberofpieces').html();
            var piecestypeid = $(trs[i]).find('#spanpiecestypeid').html();
            var totalweight = $(trs[i]).find('#spantotalweight').html();

            parcelitems.push({
                ParcelType: piecestypeid,
                CalculationType: 0,
                NumberOfPieces: numberofpieces,
                ActualWeight: actualweight,
                TotalWeight: totalweight
            })
        }

        var input = [];
        input = {
            CounterId: counterid,
            GCTypeId: gctype,
            ProductTypeId: producttype,
            ShipmentValue: shipmentvalue,
            ToCounter: receivercounter,
            TranshipmentPoints: transhipmentpoints,
            LoginId: loginid,
            ParcelItems: parcelitems
        };

        $.ajax({
            type: "POST",
            data: (input),
            url: apiurl + "api/booking/getcalculatedpriceforbooking",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    $('#divfinalcalculation').css("display", "block");
                    $('#divpriceentry').css("display", "none");
                    //$('#aeditprice').css("display", "none");
                    //$('#aeditprice').removeAttr("onclick");
                    claculateweightinfo();

                    var totalamount = 0;

                    totalamount = totalamount + data.BasicAmount;
                    $('#spancalcbasicamount').html(data.BasicAmount);
                    $('#textbasicamount').val(data.BasicAmount);

                    totalamount = totalamount + data.DriverCharges;
                    $('#spancalcdrivercharges').html(data.DriverCharges);
                    $('#textdrivercharges').val(data.DriverCharges);

                    totalamount = totalamount + data.HamaliCharges;
                    $('#spancalchamalicharges').html(data.HamaliCharges);
                    $('#texthamalicharges').val(data.HamaliCharges);

                    totalamount = totalamount + data.WithPassCharges;
                    $('#spancalcwithpasscharges').html(data.WithPassCharges);
                    $('#textwithpasscharges').val(data.WithPassCharges);

                    totalamount = totalamount + data.SUPCharges;
                    $('#spancalcsupcharges').html(data.SUPCharges);
                    $('#textsupcharges').val(data.SUPCharges);

                    totalamount = totalamount + data.LRCharges;
                    $('#spancalclrcharges').html(data.LRCharges);
                    $('#textdocketcharges').val(data.LRCharges);

                    totalamount = totalamount + data.CollectionCharges;
                    $('#spancalccollectioncharges').html(data.CollectionCharges);
                    $('#textcollection').val(data.CollectionCharges);

                    totalamount = totalamount + data.BookingOffCharges;
                    $('#spancalcbookingoffcharges').html(data.BookingOffCharges);
                    $('#textaoccharges').val(data.BookingOffCharges);

                    //totalamount = totalamount + data.TranshipmentCharges;
                    //$('#spancalctranshipmentcharges').html(data.TranshipmentCharges);
                    //$('#texttranshipmentcharges').val(data.TranshipmentCharges);

                    totalamount = totalamount + data.PickupCharges;
                    $('#spancalcpickupcharges').html(data.PickupCharges);
                    $('#textpickupcharges').val(data.PickupCharges);

                    //totalamount = totalamount + data.LocationPickupCharges;
                    //$('#spancalclocationpickupcharges').html(data.LocationPickupCharges);
                    //$('#textlocationpickupcharges').val(data.LocationPickupCharges);

                    totalamount = totalamount + data.DeliveryCharges;
                    $('#spancalclocationdeliverycharges').html(data.DeliveryCharges);
                    $('#textlocationdeliverycharges').val(data.DeliveryCharges);

                    //totalamount = totalamount + data.DoorDeliveryCharges;
                    //$('#spancalcdoordeliverycharges').html(data.DoorDeliveryCharges);
                    //$('#textdoordeliverycharges').val(data.DoorDeliveryCharges);

                    //totalamount = totalamount + data.ToPayCharges;
                    //$('#spancalctopaycharges').html(data.ToPayCharges);
                    //$('#texttopaycharges').val(data.ToPayCharges);

                    $('#spancalcsubtotal').html(totalamount);

                    var totalgst = parseFloat((gst / 100) * data.BasicAmount).toFixed(2);
                    $('#spancalcgst').html(totalgst);

                    totalamount = totalamount + parseFloat(totalgst);
                    $('#spancalctotalamount').html(totalamount);

                    if (totalamount > 0) {
                        if (data.DiscountPercentage > 0) {
                            discountpercentage = data.DiscountPercentage;
                            var discount = (discountpercentage / 100) * totalamount;
                            totalamount = totalamount - discount;
                            $('#spancalcdiscountamount').html(discount);
                            $('#spancalctotalafterdiscount').html(totalamount);
                            $('#spancalcdiscountremarks').html(data.DiscountRemarks);
                            discountremarks = data.DiscountRemarks;
                        }
                    }

                    var roundedamount = 0;
                    var roundoff = (totalamount % 5);
                    roundedamount = (roundoff <= 2) ? -(roundoff) : (5 - roundoff);
                    $('#spancalcroundoffamount').html(roundedamount);

                    totalamount = totalamount + roundedamount;
                    $('#spancalcgrandtotal').html(totalamount);

                    $('#spankms').html(data.TotalKms.toFixed(1));

                    if (totalamount == 0) {
                        $('#divpriceentry').css("display", "block");
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
}

function claculateweightinfo() {
    var trs = $('#tbodyparcelitems').find('.trdynamic');
    var weightinto = '';
    var totalweightlabel = 0;
    var totalpieces = 0;

    if (trs.length > 0) {
        for (var i = 0; i < trs.length; i++) {
            var tr = $(trs[i]).closest('tr')[0];
            var actualweight = $(tr).find('#spanactualweight').html();
            var numberofpieces = $(tr).find('#spannumberofpieces').html();
            var piecestypename = $(tr).find('#spanpiecestypenameview').html();
            var totalweight = $(tr).find('#spantotalweight').html();
            var weight = '';

            totalpieces = totalpieces + parseFloat(numberofpieces);
            totalweightlabel = totalweightlabel + parseFloat(totalweight);
            weight = '' + actualweight + ' ' + measurement + ' * ' + numberofpieces + ' ' + piecestypename + ' = ' + totalweight + ' ' + measurement + '';
            weightinto = (weightinto == "") ? weight : weightinto + ', ' + weight;
        }
    }
    $('#spancalctotalpieces').html(totalpieces);
    $('#spancalctotalweight').html(totalweightlabel);
    $('#spanweightlist').html(weightinto);
}

function editpricedetails() {
    $('#divpriceentry').css("display", "block");
}

var priceeditremarks = '';

function updateprice() {
    priceeditremarks = '';
    var basicamount = $('#textbasicamount').val().trim();
    var drivercharges = $('#textdrivercharges').val();
    var hamalicharges = $('#texthamalicharges').val();
    var withpasscharges = $('#textwithpasscharges').val();
    var supcharges = $('#textsupcharges').val().trim();
    var lrcharges = $('#textlrcharges').val();
    var collection = $('#textcollection').val();
    var bookingoffcharges = $('#textbookingoffcharges').val();
    var pickupcharges = $('#textpickupcharges').val();
    var deliverycharges = $('#textlocationdeliverycharges').val();
    //var transhipmentcharges = $('#texttranshipmentcharges').val();
    //var locationpickupcharges = $('#textlocationpickupcharges').val();
    //var locationdeliverycharges = $('#textlocationdeliverycharges').val();
    //var doordeliverycharges = $('#textdoordeliverycharges').val();
    //var topaycharges = $('#texttopaycharges').val();
    var viewpriceeditremarks = $('#textpriceeditremarks').val();

    var isvalid = true;
    var totalamount = 0;

    if (basicamount == "") {
        isvalid = false;
        $('#spanbasicamount').html("Please enter Basic Amount");
    }
    else {
        totalamount = totalamount + parseFloat(basicamount);
        $('#spancalcbasicamount').html(basicamount);
    }
    if (drivercharges == "") {
        isvalid = false;
        $('#spandrivercharges').html("Please enter Driver Charges");
    }
    else {
        totalamount = totalamount + parseFloat(drivercharges);
        $('#spancalcdrivercharges').html(drivercharges);
    }
    if (hamalicharges == "") {
        isvalid = false;
        $('#spanhamalicharges').html("Please enter Hamali Charges");
    }
    else {
        totalamount = totalamount + parseFloat(hamalicharges);
        $('#spancalchamalicharges').html(hamalicharges);
    }
    if (withpasscharges == "") {
        isvalid = false;
        $('#spanwithpasscharges').html("Please enter With Pass Charges");
    }
    else {
        totalamount = totalamount + parseFloat(withpasscharges);
        $('#spancalcwithpasscharges').html(withpasscharges);
    }
    if (supcharges == "") {
        isvalid = false;
        $('#spansupcharges').html("Please enter SurCharges");
    }
    else {
        totalamount = totalamount + parseFloat(supcharges);
        $('#spancalcsupcharges').html(supcharges);
    }
    if (lrcharges == "") {
        isvalid = false;
        $('#spanlrcharges').html("Please enter LR Charges");
    }
    else {
        totalamount = totalamount + parseFloat(lrcharges);
        $('#spancalclrcharges').html(lrcharges);
    }
    if (collection == "") {
        isvalid = false;
        $('#spancollection').html("Please enter Collection Charges");
    }
    else {
        totalamount = totalamount + parseFloat(collection);
        $('#spancalccollectioncharges').html(collection);
    }
    if (bookingoffcharges == "") {
        isvalid = false;
        $('#spancalcbookingoffcharges').html("Please enter Booking Off Charges");
    }
    else {
        totalamount = totalamount + parseFloat(bookingoffcharges);
        $('#spancalcbookingoffcharges').html(bookingoffcharges);
    }

    if ($('#textpoint1').val() != '' || $('#textpoint2').val() != '') {
        if (transhipmentcharges == "0" || transhipmentcharges == "") {
            isvalid = false;
            $('#spantranshipmentcharges').html("Please enter Transhipment Charges");
        }
        else {
            totalamount = totalamount + parseFloat(transhipmentcharges);
            $('#spancalctranshipmentcharges').html(transhipmentcharges);
        }
    }
    if (pickupcharges == "") {
        isvalid = false;
        $('#spanpickupcharges').html("Please enter Pickup Charges");
    }
    else {
        totalamount = totalamount + parseFloat(pickupcharges);
        $('#spancalcpickupcharges').html(pickupcharges);
    }
    if (deliverycharges == "") {
        isvalid = false;
        $('#spanlocationdeliverycharges').html("Please enter Delivery Charges");
    }
    else {
        totalamount = totalamount + parseFloat(deliverycharges);
        $('#spancalclocationdeliverycharges').html(deliverycharges);
    }
    if (viewpriceeditremarks == "") {
        isvalid = false;
        $('#spanpriceeditremarks').html("Please enter Edit Price Remarks");
    }
    else {
        priceeditremarks = viewpriceeditremarks;
    }

    if (isvalid && totalamount > 0) {
        $('#spancalcsubtotal').html(totalamount);

        var totalgst = parseFloat((gst / 100) * parseFloat(basicamount)).toFixed(2);
        $('#spancalcgst').html(totalgst);

        totalamount = totalamount + parseFloat(totalgst);
        $('#spancalctotalamount').html(totalamount);

        if (discountpercentage > 0) {
            var discount = (discountpercentage / 100) * totalamount;
            totalamount = totalamount - discount;
            $('#spancalcdiscountamount').html(discount);
            $('#spancalctotalafterdiscount').html(totalamount);
            $('#spancalcdiscountremarks').html(discountremarks);
        }

        var roundedamount = 0;
        var roundoff = (totalamount % 5);
        roundedamount = (roundoff <= 2) ? -(roundoff) : (5 - roundoff);

        totalamount = totalamount + roundedamount;
        $('#spancalcroundoffamount').html(roundedamount.toFixed(2));
        $('#spancalcgrandtotal').html(totalamount.toFixed(2));

        $('#divpriceentry').css("display", "none");
    }
    else {
        $('#spanbasicamount').html("Please enter Basic Amount");
    }
}

function openapplydiscountmodal() {
    $('#spandiscounttotalamount').html($('#spancalctotalamount').html());
    $('#spantotalamountafterdiscount').html($('#spancalctotalamount').html());
    $('#textdiscountamount').val('');
    $('#textdiscountremarks').val('');
    $('#modalapplydiscount').modal();
}

var discountremarks = '';
var discountpercentage = 0;

function validatediscountamount() {
    var discountamount = $('#textdiscountamount').val();
    var totalamount = parseFloat($('#spandiscounttotalamount').html());
    discountpercentage = 0;

    if (discountamount == "") {
        $('#spandiscountamount').html("Please enter Discount Amount");
        return false;
    }
    else if (parseFloat(discountamount) > totalamount) {
        $('#spandiscountamount').html("Please enter Discount Amount less than Total Amount");
        return false;
    }
    else {
        totalamount = totalamount - discountamount;
        $('#spancalcdiscountamount').html(discountamount);
        $('#spantotalamountafterdiscount').html(totalamount);
    }
}

function applydiscount() {
    var discountamount = $('#textdiscountamount').val();
    var remarks = $('#textdiscountremarks').val();

    var isvalid = true;
    var totalamount = parseFloat($('#spancalctotalamount').html());

    if (discountamount == "") {
        isvalid = false;
        $('#spandiscountamount').html("Please enter Discount Amount");
    }
    else if (parseFloat(discountamount) > totalamount) {
        isvalid = false;
        $('#spandiscountamount').html("Please enter Discount Amount less than Total Amount");
    }
    if (remarks == "") {
        isvalid = false;
        $('#spandiscountremarks').html("Please enter Discount Remarks");
    }
    if (isvalid) {
        discountremarks = remarks;
        $('#spancalcdiscountremarks').html(remarks);
        totalamount = totalamount - discountamount;
        $('#spancalcdiscountamount').html(discountamount);
        $('#spancalctotalafterdiscount').html(totalamount);

        var roundedamount = 0;
        var roundoff = (totalamount % 5);
        roundedamount = (roundoff <= 2) ? -(roundoff) : (5 - roundoff);
        $('#spancalcroundoffamount').html(roundedamount);

        totalamount = totalamount + roundedamount;
        $('#spancalcgrandtotal').html(totalamount);

        $('#textdiscountamount').val('');
        $('#textdiscountremarks').val('');
        $('#closemodalapplydiscount').click();
    }
}

function bookingconfirm() {
    hideallalerts();
    var isvalid = true;

    var bookid = $('#hiddenbookid').val().trim();
    var counterid = $('#hiddencounterid').val().trim();
    var loginid = $('#hiddenloginid').val().trim();
    var gctype = $('#selectgctype').val().trim();
    var sendercounter = $('#textsendercounter').val().trim();
    var sendermobile = $('#textsendermobile').val().trim();
    var sendername = $('#textsendername').val().trim();
    var senderaddress = $('#textsenderaddress').val().trim();
    var receivercounter = $('#textreceivercounter').val().trim();
    var receivermobile = $('#textreceivermobile').val().trim();
    var receivername = $('#textreceivername').val().trim();
    var receiveraddress = $('#textreceiveraddress').val().trim();
    var point1 = $('#textpoint1').val().trim();
    var point2 = $('#textpoint2').val().trim();
    var producttype = $('#selectproducttype').val().trim();
    var shipmentvalue = $('#textshipmentvalue').val().trim();
    var shipmentdescription = $('#textshipmentdescription').val().trim();
    var totalpieces = $('#spancalctotalpieces').html();
    var totalweight = $('#spancalctotalweight').html();
    var route = $('#spanroute').html();
    var totalkms = $('#spankms').html();
    var weightlist = $('#spanweightlist').html();
    var basicamount = $('#spancalcbasicamount').html();
    var drivercharges = $('#spancalcdrivercharges').html();
    var hamalicharges = $('#spancalchamalicharges').html();
    var withpasscharges = $('#spancalcwithpasscharges').html();
    var supcharges = $('#spancalcsupcharges').html();
    var lrcharges = $('#spancalclrcharges').html();
    var collectioncharges = $('#spancalccollectioncharges').html();
    var bookingoffcharges = $('#spancalcbookingoffcharges').html();
    var transhipmentcharges = 0;
    var pickupcharges = $('#spancalcpickupcharges').html();
    var locationpickupcharges = 0;
    var locationdeliverycharges = 0;
    var deliverycharges = $('#spancalcdoordeliverycharges').html();
    var doordeliverycharges = 0;
    var topaycharges = 0;
    var subtotal = $('#spancalcsubtotal').html();
    var calcgst = $('#spancalcgst').html();
    var calctotalamount = $('#spancalctotalamount').html();
    var discountamount = $('#spancalcdiscountamount').html();
    var totalafterdiscount = $('#spancalctotalafterdiscount').html();
    var roundoffamount = $('#spancalcroundoffamount').html();
    var grandtotal = $('#spancalcgrandtotal').html();

    if (validatedropdown(gctype, $('#spangctypehelper'), 'Please select GC Type') == false) {
        isvalid = false;
    }
    if (validatetextbox(sendercounter, $('#spansendercounter'), 'Please select Sending From') == false) {
        isvalid = false;
    }
    if (validatetextbox(sendermobile, $('#spansendermobile'), 'Please enter Sender Mobile Number') == false) {
        isvalid = false;
    }
    if (validatetextbox(sendername, $('#spansendername'), 'Please enter Sender Name') == false) {
        isvalid = false;
    }
    if (validatetextbox(senderaddress, $('#spansenderaddress'), 'Please enter Sender Address') == false) {
        isvalid = false;
    }
    if (validatetextbox(receivercounter, $('#spanreceivercounter'), 'Please select Receive To') == false) {
        isvalid = false;
    }
    if (validatetextbox(receivermobile, $('#spanreceivermobile'), 'Please enter Receiver Mobile Number') == false) {
        isvalid = false;
    }
    if (validatetextbox(receivername, $('#spanreceivername'), 'Please enter Receiver Name') == false) {
        isvalid = false;
    }
    if (validatetextbox(receiveraddress, $('#spanreceiveraddress'), 'Please enter Receiver Address') == false) {
        isvalid = false;
    }
    if (validatedropdown(producttype, $('#spanproducttype'), 'Please select Product Type') == false) {
        isvalid = false;
    }
    if (validatetextbox(shipmentvalue, $('#spanshipmentvalue'), 'Please enter Shipment Value') == false) {
        isvalid = false;
    }
    if (validatetextbox(shipmentdescription, $('#spanshipmentdescription'), 'Please enter Description') == false) {
        isvalid = false;
    }
    if ($('#tbodyparcelitems').find('.trdynamic').length == 0) {
        isvalid = false;
        alert("Please enter atleast one parcel");
    }
    if (parseFloat(calctotalamount) == 0) {
        isvalid = false;
        alert("Please enter valid Price");
    }

    if (sendercounter != "" && receivercounter != "") {
        if (point1 != "") {
            if (point1 == receivercounter) {
                isvalid = false;
                showwarningalert("Transhipment Point 1 and Receiving Location should not be same");
            }
        }
        if (point2 != "") {
            if (point2 == receivercounter) {
                isvalid = false;
                showwarningalert("Transhipment Point 2 and Receiving Location should not be same");
            }
            else if (point2 == point1) {
                isvalid = false;
                showwarningalert("Transhipment Point 1 and Transhipment Point 2 should not be same");
            }
        }
    }

    if (isvalid) {
        showloading();

        var transhipmentpoints = '';

        if (point1 != "") {
            transhipmentpoints = point1;
        }
        if (point2 != "") {
            transhipmentpoints = (transhipmentpoints == "") ? point2 : transhipmentpoints + "," + point2;
        }

        var parcelitems = [];

        var trs = $('#tbodyparcelitems').find('.trdynamic');

        for (var i = 0; i < trs.length; i++) {
            var actualweight = $(trs[i]).find('#spanactualweight').html();
            var numberofpieces = $(trs[i]).find('#spannumberofpieces').html();
            var piecestypeid = $(trs[i]).find('#spanpiecestypeid').html();
            var trtotalweight = $(trs[i]).find('#spantotalweight').html();

            parcelitems.push({
                ParcelType: piecestypeid,
                CalculationType: 0,
                NumberOfPieces: numberofpieces,
                ActualWeight: actualweight,
                TotalWeight: trtotalweight
            })
        }

        var input = [];
        input = {
            LoginId: loginid,
            CounterId: counterid,
            FromCounter: sendercounter,
            ToCounter: receivercounter,
            GCTypeId: gctype,
            BookingTypeId: gctype,
            ProductTypeId: producttype,
            BookId: bookid,
            SenderId: senderid,
            SenderName: sendername,
            SenderEmailId: '',
            SenderMobileNumber: sendermobile,
            SenderAddress: senderaddress,
            ReceiverId: receiverid,
            ReceiverName: receivername,
            ReceiverEmailId: '',
            ReceiverMobileNumber: receivermobile,
            ReceiverAddress: receiveraddress,
            TranshipmentPoint1: point1,
            TranshipmentPoint2: point2,
            ShipmentValue: shipmentvalue,
            ShipmentDescription: shipmentdescription,
            BasicAmount: basicamount,
            SUPCharges: supcharges,
            WithPASSCharges: withpasscharges,
            DocketCharges: 0,
            ValueSCCharges: 0,
            CollectionCharges: collectioncharges,
            HamaliCharges: hamalicharges,
            AOCCharges: 0,
            TranshipmentCharges: transhipmentcharges,
            PickupCharges: pickupcharges,
            LocationPickupCharges: locationpickupcharges,
            LocationDeliveryCharges: locationdeliverycharges,
            DoorDeliveryCharges: doordeliverycharges,
            DriverCharges: drivercharges,
            ToPayCharges: topaycharges,
            LRCharges: lrcharges,
            BookingOffCharges: bookingoffcharges,
            DeliveryCharges: deliverycharges,
            SubTotal: subtotal,
            GSTCharges: calcgst,
            TotalAmount: calctotalamount,
            DiscountAmount: discountamount,
            TotalAmountAfterDiscount: totalafterdiscount,
            RoundOffAmount: roundoffamount,
            GrandTotal: grandtotal,
            TotalKms: totalkms,
            DiscountRemarks: discountremarks,
            EditPriceRemarks: priceeditremarks,
            TotalPieces: totalpieces,
            TotalWeight: totalweight,
            WeightInfo: weightlist,
            RouteInfo: route,
            ParcelItems: parcelitems
        };

        $.ajax({
            type: "POST",
            data: (input),
            url: apiurl + "api/booking/savebookingdetails",
            dataType: "json",
            success: function (data) {
                if (data.StatusId != null) {
                    if (data.StatusId == 1) {
                        showsuccessalert(data.StatusMessage);
                        window.location = "/cargo/bookingsuccess/" + data.BookingId + "/" + data.BookingSerialNumber;
                    }
                    else {
                        showwarningalert(data.StatusMessage);
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
}

function validatetextbox(value, span, message) {
    if (value == "") {
        $(span).html(message);
        return false;
    }
}

function validatedropdown(value, span, message) {
    if (value == "0" || value == "") {
        $(span).html(message);
        return false;
    }
}

function savebooking() {
    var gctype = $('#selectgctype').val().trim();
    var totalamount = $('#spancalcgrandtotal').html();

    if (validatedropdown(gctype, $('#spangctypehelper'), 'Please select GC Type') == false) {
        alert("Please select GC Type");
    }
    else if (totalamount == "") {
        alert("Amount should not be '0'");
    }
    else if (parseFloat(totalamount) == 0) {
        alert("Amount should not be '0'");
    }
    else {
        $('#labelmodalgctype').html($("#selectgctype option:selected").text());
        $('#labelmodaltotalamount').html($("#spancalcgrandtotal").html());

        $('#modalbooking').modal();
    }
}