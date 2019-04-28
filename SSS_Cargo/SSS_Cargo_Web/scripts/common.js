var apiurl = "http://localhost:15333/";
var weburl = "http://localhost:50963/";
//var apiurl = "http://api.ssclogistics.co.in/";
//var weburl = "http://ssclogistics.co.in/";

function closemessage(obj) {
    $(obj).closest('.alert').hide();
}

function showsuccessalert(message) {
    $('#divErrorAlert').hide();
    $('#divWarningAlert').hide();
    
    $('#divSuccessAlert').show();
    $('#spanSuccessAlert').html(message);
}

function showwarningalert(message) {
    $('#divErrorAlert').hide();
    $('#divSuccessAlert').hide();
    
    $('#divWarningAlert').show();
    $('#spanWarningAlert').html(message);
}

function showerroralert(message) {
    $('#divWarningAlert').hide();
    $('#divSuccessAlert').hide();
    
    $('#divErrorAlert').show();
    $('#spanErrorAlert').html(message);
}

function hideallalerts() {
    $('#divErrorAlert').hide();
    $('#divSuccessAlert').hide();
    $('#divWarningAlert').hide();
}

function showloading() {
    $('#divloading').show();
}

function hideloading() {
    $('#divloading').hide();
}

function validatetextbox(obj, span) {
    $(obj).closest('.form-group').removeClass("has-success");
    $(obj).closest('.form-group').removeClass("has-error");
    if ($(obj).val().trim() == "") {
        $(obj).closest('.form-group').addClass("has-error");
        return false;
    }
    else {
        $(obj).closest('.form-group').addClass("has-success");
        return true;
    }
}

function validatedropdown(obj) {
    $(obj).closest('.form-group').removeClass("has-success");
    $(obj).closest('.form-group').removeClass("has-error");
    if ($(obj).val().trim() == "0") {
        $(obj).closest('.form-group').addClass("has-error");
        return false;
    }
    else {
        $(obj).closest('.form-group').addClass("has-success");
        return true;
    }
}

function validateemailid(obj) {
    $(obj).closest('.form-group').removeClass("has-success");
    $(obj).closest('.form-group').removeClass("has-error");
    var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;

    if ($(obj).val().trim() == "") {
        $(obj).closest('.form-group').addClass("has-error");
        return false;
    }
    else if (!re.test($.trim($(obj).val()))) {
        $(obj).closest('.form-group').addClass("has-error");
        return false;
    }
    else {
        $(obj).closest('.form-group').addClass("has-success");
        return true;
    }
}

function validatephonenumber(obj) {
    $(obj).closest('.form-group').removeClass("has-success");
    $(obj).closest('.form-group').removeClass("has-error");
    var phonewithspecialchars = /^\(?([0-9]{3})\)?[-()]?([0-9]{3})[-()]?([0-9]{4})$/;
    var phoneno = /^\d{10}$/;
    var inputtext = $(obj).val().trim();

    if (inputtext.match(phonewithspecialchars)) {
        $(obj).closest('.form-group').addClass("has-success");
        return true;
    }
    else if (inputtext.match(phoneno)) {
        $(obj).closest('.form-group').addClass("has-success");
        return true;
    }
    else {
        $(obj).closest('.form-group').addClass("has-error");
        return false;
    }
}

function validateimage(obj) {
    $(obj).closest('.form-group').removeClass("has-success");
    $(obj).closest('.form-group').removeClass("has-error");
    var fileextensions = ['jpeg', 'jpg', 'png', 'bmp'];

    if ($(obj).val().trim() == "") {
        $(obj).closest('.form-group').addClass("has-error");
        return false;
    }
    else if ($.inArray($(obj).val().trim().split('.').pop().toLowerCase(), fileextensions) == -1) {
        $(obj).closest('.form-group').addClass("has-error");
        return false;
    }
    else {
        $(obj).closest('.form-group').addClass("has-success");
        return true;
    }
}

function getmultiselectedvalues(obj) {
    var options = $(obj).find('option:selected');
    var selectedvalues = '';

    for (var i = 0; i < options.length; i++) {
        var value = $(options[i]).val();
        selectedvalues = (selectedvalues == "") ? value : selectedvalues + ',' + value;
    }

    return selectedvalues;
}