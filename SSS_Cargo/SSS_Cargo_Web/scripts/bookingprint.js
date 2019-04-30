function getbookingdetails() {
    hideallalerts();
    var bookingid = $('#hiddenbookingid').val();

    var input = [];
    input = {
        BookingId: bookingid
    };

    $.ajax({
        type: "POST",
        data: (input),
        url: apiurl + "api/booking/getbookingdetailstoprintbybookingid",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.StatusId == 1) {
                    $('.lrnumber').html(data.BookSerialNumber);
                    $('.bookingdate').html(data.BookingDate);
                    $('.fromlocation').html(data.FromCounterName);
                    $('.tolocation').html(data.ToCounterName);
                    $('.paymenttype').html(data.PaymentType);
                    $('.sendername').html(data.SenderName);
                    $('.senderphone').html(data.SenderMobileNumber);
                    $('.fromcounterphone').html(data.FromCounterPhoneNumber);
                    $('.fromgstnumber').html(data.FromCounterGST);
                    $('.receivername').html(data.ReceiverName);
                    $('.receiverphone').html(data.ReceiverMobileNumber);
                    $('.tocounterphone').html(data.ToCounterPhoneNumber);
                    $('.togstnumber').html(data.ToCounterGST);
                    $('.actualweight').html(data.ActualWeight);
                    $('.chargedweight').html(data.ChargedWeight);
                    $('.numberofpieces').html(data.TotalPieces);
                    $('.bookingtype').html(data.BookingType);
                    $('.producttype').html(data.ProductType);
                    $('.gctype').html(data.GCType);
                    $('.shippingvalue').html(parseFloat(data.ShipmentValue).toFixed(2));
                    $('.route').html(data.RouteInfo);
                    $('.shippingdecription').html(data.ShipmentDescription);
                    $('.cargogst').html(data.CargoGSTIN);
                    $('.basicamount').html(parseFloat(data.BasicAmount).toFixed(2));
                    $('.hamaliamount').html(parseFloat(data.HamaliCharges).toFixed(2));
                    $('.supcharges').html(parseFloat(data.SurCharges).toFixed(2));
                    $('.valuesrcharges').html(parseFloat(data.ValueSurCharges).toFixed(2));
                    $('.withpasscharges').html(parseFloat(data.WithPassCharges).toFixed(2));
                    $('.aoccharges').html(parseFloat(data.AOCCharges).toFixed(2));
                    $('.transhipmentcharges').html(parseFloat(data.TranshipmentCharges).toFixed(2));
                    $('.collectioncharges').html(parseFloat(data.CollectionCharges).toFixed(2));
                    $('.pickupcharges').html(parseFloat(data.PickupCharges).toFixed(2));
                    $('.locationdeliverycharges').html(parseFloat(data.LocationDeliveryCharges).toFixed(2));
                    $('.doordeliverycharges').html(parseFloat(data.DoorDeliveryCharges).toFixed(2)); 
                    $('.drivercharges').html(parseFloat(data.DriverCharges).toFixed(2));
                    $('.topaycharges').html(parseFloat(data.ToPayCharges).toFixed(2));
                    $('.subtotal').html(parseFloat(data.SubTotal).toFixed(2));
                    $('.gstamount').html(parseFloat(data.GSTAmount).toFixed(2));
                    $('.discount').html(parseFloat(data.DiscountAmount).toFixed(2));
                    $('.roundoff').html(parseFloat(data.RoundOffAmount).toFixed(2));
                    $('.grandtotal').html(parseFloat(data.GrandTotal).toFixed(2));
                    $('.barcode').attr('src', data.barcodeImage);
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