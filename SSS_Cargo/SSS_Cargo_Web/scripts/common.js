var apiurl = "http://localhost:1318/";
var weburl = "http://localhost:1318/";

function ShowSuccessAlert(message) {
    $('#divSuccessAlert').css("display", "block");
    $('#divWarningAlert').css("display", "none");
    $('#divErrorAlert').css("display", "none");
    $('#spanSuccessAlert').html(message);
}

function ShowWarningAlert(message) {
    $('#divWarningAlert').css("display", "block");
    $('#divSuccessAlert').css("display", "none");
    $('#divErrorAlert').css("display", "none");
    $('#spanWarningAlert').html(message);
}

function ShowErrorAlert(message) {
    $('#divErrorAlert').css("display", "block");
    $('#divSuccessAlert').css("display", "none");
    $('#divWarningAlert').css("display", "none");
    $('#spanErrorAlert').html(message);
}

function HideAllAlerts() {
    $('#divErrorAlert').css("display", "none");
    $('#divSuccessAlert').css("display", "none");
    $('#divWarningAlert').css("display", "none");
}

function ShowLoading() {
    $('#divLoading').show();
}

function HideLoading() {
    $('#divLoading').hide();
}