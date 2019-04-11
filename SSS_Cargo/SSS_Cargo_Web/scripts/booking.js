var tocounterid = '';
var transhipmentpoint1 = '';
var transhipmentpoint2 = '';
var sendermobile = '';
var senderid = 0;
var receivermobile = '';
var receiverid = 0;
var measurement = 'Kgs';
var gst = 5;

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
                        $('#selectgctype').val(1);
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
                                minLength: 10,
                                select: function (event, ui) {
                                    sendermobile = ui.item.label;
                                    getcustomerdetailsbymobilenumber(sendermobile, true);
                                }
                            });
                            $('#textreceivermobile').autocomplete({
                                source: customers,
                                minLength: 10,
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
        gst = 5;
        $('#spangstperc').html('5%');
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
    $('#divfinalcalculation').css("display", "block");
    $('#divpriceentry').css("display", "none");
    claculateweightinfo();
}

function claculateweightinfo() {
    var trs = $('#tbodyparcelitems').find('.trdynamic');
    var weightinto = '';

    if (trs.length > 0) {
        for (var i = 0; i < trs.length; i++) {
            var tr = $(trs[i]).closest('tr')[0];
            var actualweight = $(tr).find('#spanactualweight').html();
            var numberofpieces = $(tr).find('#spannumberofpieces').html();
            var piecestypename = $(tr).find('#spanpiecestypenameview').html();
            var totalweight = $(tr).find('#spantotalweight').html();
            var weight = '';

            weight = '' + actualweight + ' ' + measurement + ' * ' + numberofpieces + ' ' + piecestypename + ' = ' + totalweight + ' ' + measurement + '';
            weightinto = (weightinto == "") ? weight : weightinto + ', ' + weight;
        }
    }
    $('#spanweightlist').html(weightinto);
}

function editpricedetails() {
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

    if (isvalid) {
        $('#divpriceentry').css("display", "block");
    }
    else {
        $('#divpriceentry').css("display", "none");
    }
}

var priceeditremarks = '';

function updateprice() {
    priceeditremarks = '';
    var basicamount = $('#textbasicamount').val().trim();
    var supcharges = $('#textsupcharges').val().trim();
    var withpasscharges = $('#textwithpasscharges').val();
    var docketcharges = $('#textdocketcharges').val();
    var valuesrcharges = $('#textvaluesrcharges').val();
    var collection = $('#textcollection').val();
    var hamalicharges = $('#texthamalicharges').val();
    var aoccharges = $('#textaoccharges').val();
    var transhipmentcharges = $('#texttranshipmentcharges').val();
    var pickupcharges = $('#textpickupcharges').val();
    var locationpickupcharges = $('#textlocationpickupcharges').val();
    var locationdeliverycharges = $('#textlocationdeliverycharges').val();
    var doordeliverycharges = $('#textdoordeliverycharges').val();
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
    if (supcharges == "") {
        isvalid = false;
        $('#spansupcharges').html("Please enter SUP Charges");
    }
    else {
        totalamount = totalamount + parseFloat(supcharges);
        $('#spancalcsupcharges').html(supcharges);
    }
    if (withpasscharges == "") {
        isvalid = false;
        $('#spanwithpasscharges').html("Please enter With Pass Charges");
    }
    else {
        totalamount = totalamount + parseFloat(withpasscharges);
        $('#spancalcwithpasscharges').html(withpasscharges);
    }
    if (docketcharges == "") {
        isvalid = false;
        $('#spandocketcharges').html("Please enter Docket Charges");
    }
    else {
        totalamount = totalamount + parseFloat(docketcharges);
        $('#spancalcdocketcharges').html(docketcharges);
    }
    if (valuesrcharges == "") {
        isvalid = false;
        $('#spanvaluesrcharges').html("Please enter Value SR Charges");
    }
    else {
        totalamount = totalamount + parseFloat(valuesrcharges);
        $('#spancalcvaluesrcharges').html(valuesrcharges);
    }
    if (collection == "") {
        isvalid = false;
        $('#spancollection').html("Please enter Collection Charges");
    }
    else {
        totalamount = totalamount + parseFloat(collection);
        $('#spancalccollectioncharges').html(collection);
    }
    if (hamalicharges == "") {
        isvalid = false;
        $('#spanhamalicharges').html("Please enter Hamali Charges");
    }
    else {
        totalamount = totalamount + parseFloat(hamalicharges);
        $('#spancalchamalicharges').html(hamalicharges);
    }
    if (aoccharges == "") {
        isvalid = false;
        $('#spanaoccharges').html("Please enter AOC Charges");
    }
    else {
        totalamount = totalamount + parseFloat(aoccharges);
        $('#spancalcaoccharges').html(aoccharges);
    }
    if (transhipmentcharges == "") {
        isvalid = false;
        $('#spantranshipmentcharges').html("Please enter Transhipment Charges");
    }
    else {
        totalamount = totalamount + parseFloat(transhipmentcharges);
        $('#spancalctranshipmentcharges').html(transhipmentcharges);
    }
    if (pickupcharges == "") {
        isvalid = false;
        $('#spanpickupcharges').html("Please enter Pickup Charges");
    }
    else {
        totalamount = totalamount + parseFloat(pickupcharges);
        $('#spancalcpickupcharges').html(pickupcharges);
    }
    if (locationpickupcharges == "") {
        isvalid = false;
        $('#spanlocationpickupcharges').html("Please enter Location Pickup Charges");
    }
    else {
        totalamount = totalamount + parseFloat(locationpickupcharges);
        $('#spancalclocationpickupcharges').html(locationpickupcharges);
    }
    if (locationdeliverycharges == "") {
        isvalid = false;
        $('#spanlocationdeliverycharges').html("Please enter Location Delivery Charges");
    }
    else {
        totalamount = totalamount + parseFloat(locationdeliverycharges);
        $('#spancalclocationdeliverycharges').html(locationdeliverycharges);
    }
    if (doordeliverycharges == "") {
        isvalid = false;
        $('#spandoordeliverycharges').html("Please enter Door Delivery Charges");
    }
    else {
        totalamount = totalamount + parseFloat(doordeliverycharges);
        $('#spancalcdoordeliverycharges').html(doordeliverycharges);
    }
    if (viewpriceeditremarks == "") {
        isvalid = false;
        $('#spanpriceeditremarks').html("Please enter Edit Price Remarks");
    }
    else {
        priceeditremarks = viewpriceeditremarks;
    }
    
    $('#spancalcsubtotal').html(totalamount);

    var totalgst = parseFloat((gst / 100) * totalamount).toFixed(2);
    $('#spancalcgst').html(totalgst);

    totalamount = totalamount + totalgst;
    $('#spancalctotalamount').html(totalamount);

    var roundedamount = 0;
    var roundoff = (totalamount % 5);
    roundedamount = (roundoff <= 2) ? -(roundoff) : (5 - roundoff);
    $('#spancalcroundoffamount').html(roundedamount);

    totalamount = totalamount + roundedamount;
    $('#spancalcgrandtotal').html(totalamount);

    $('#divpriceentry').css("display", "none");
}

function savebooking() {
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
    var basicamount = $('#spancalcbasicamount').html();
    var supcharges = $('#spancalcsupcharges').html();
    var withpasscharges = $('#spancalcwithpasscharges').html();
    var docketcharges = $('#spancalcdocketcharges').html();
    var valuesrcharges = $('#spancalcvaluesrcharges').html();
    var collectioncharges = $('#spancalccollectioncharges').html();
    var hamalicharges = $('#spancalchamalicharges').html();
    var aoccharges = $('#spancalcaoccharges').html();
    var transhipmentcharges = $('#spancalctranshipmentcharges').html();
    var pickupcharges = $('#spancalcpickupcharges').html();
    var calcgst = $('#spancalcgst').html();
    var calctotalamount = $('#spancalctotalamount').html();
    var totalkms = $('#spankms').html();

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
            AOCCharges: aoccharges,
            BasicAmount: basicamount,
            BookId: bookid,
            BookingTypeId: gctype,
            CollectionCharges: collectioncharges,
            DocketCharges: docketcharges,
            CounterId: counterid,
            GCTypeId: gctype,
            GSTCharges: calcgst,
            HamaliCharges: hamalicharges,
            ProductTypeId: producttype,
            ReceiverAddress: receiveraddress,
            ReceiverEmailId: '',
            ReceiverId: receiverid,
            ReceiverMobileNumber: receivermobile,
            ReceiverName: receivername,
            SenderAddress: senderaddress,
            SenderEmailId: '',
            SenderId: senderid,
            SenderMobileNumber: sendermobile,
            SenderName: sendername,
            ShipmentDescription: shipmentdescription,
            ShipmentValue: shipmentvalue,
            SUPCharges: supcharges,
            ToCounter: receivercounter,
            TotalAmount: calctotalamount,
            TranshipmentCharges: transhipmentcharges,
            PickupCharges: pickupcharges,
            TranshipmentPoints: transhipmentpoints,
            LoginId: loginid,
            ValueSCCharges: valuesrcharges,
            WithPASSCharges: withpasscharges,
            TotalKms: totalkms,
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
                        window.location = "/cargo/bookingsuccess/" + data.BookingSerialNumber;
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