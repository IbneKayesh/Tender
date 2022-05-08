$(document).ready(function () {

    $("#tndDetails").click(function () {
        $("#detailsBody").css("display", "block");
        $("#itemBody").css("display", "none");
        $("#shipmentBody").css("display", "none");
        $("#paymentBody").css("display", "none");

        $("#tndDetails").removeClass("btn-outline-primary");
        $("#tndDetails").addClass("btn-primary");
        $("#tndItem").removeClass("btn-primary");
        $("#tndItem").addClass("btn-outline-primary");
        $("#tndShipment").removeClass("btn-primary");
        $("#tndShipment").addClass("btn-outline-primary");
        $("#tndPayment").removeClass("btn-primary");
        $("#tndPayment").addClass("btn-outline-primary");
    });
    $("#tndItem").click(function () {
        $("#detailsBody").css("display", "none");
        $("#itemBody").css("display", "block");
        $("#shipmentBody").css("display", "none");
        $("#paymentBody").css("display", "none");

        $("#tndItem").removeClass("btn-outline-primary");
        $("#tndItem").addClass("btn-primary");
        $("#tndDetails").removeClass("btn-primary");
        $("#tndDetails").addClass("btn-outline-primary");
        $("#tndShipment").removeClass("btn-primary");
        $("#tndShipment").addClass("btn-outline-primary");
        $("#tndPayment").removeClass("btn-primary");
        $("#tndPayment").addClass("btn-outline-primary");

    });
    $("#tndShipment").click(function () {
        $("#detailsBody").css("display", "none");
        $("#itemBody").css("display", "none");
        $("#shipmentBody").css("display", "block");
        $("#paymentBody").css("display", "none");

        $("#tndShipment").removeClass("btn-outline-primary");
        $("#tndShipment").addClass("btn-primary");
        $("#tndDetails").removeClass("btn-primary");
        $("#tndDetails").addClass("btn-outline-primary");
        $("#tndItem").removeClass("btn-primary");
        $("#tndItem").addClass("btn-outline-primary");
        $("#tndPayment").removeClass("btn-primary");
        $("#tndPayment").addClass("btn-outline-primary");
    });
    $("#tndPayment").click(function () {
        $("#detailsBody").css("display", "none");
        $("#itemBody").css("display", "none");
        $("#shipmentBody").css("display", "none");
        $("#paymentBody").css("display", "block");

        $("#tndDetails").removeClass("btn-primary");
        $("#tndDetails").addClass("btn-outline-primary");
        $("#tndItem").removeClass("btn-primary");
        $("#tndItem").addClass("btn-outline-primary");
        $("#tndShipment").removeClass("btn-primary");
        $("#tndShipment").addClass("btn-outline-primary");
        $("#tndPayment").removeClass("btn-outline-primary");
        $("#tndPayment").addClass("btn-primary");
    });


})