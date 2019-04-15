$(document).ready(function () {
    $(".answer:last").fadeOut("slow", function () {
        $(this).fadeIn("slow");
    });
    var aid = 2;
    $(`.Reponse:last`).on("focus", function () {
        aid++
        $(".rouge").hide("slow");
        $("<div class=`mm`> <label for=`Rep`>Reponse N°" + aid+":</label> <input name=Reponse class=Reponse type`=`text` placeholder=`Entrez votre Réponse`></div>").fadeIn('slow').appendTo('.mm:last');
   
    });
    $(".valider").on("click",function () {

     $.alert("ok");


    });
    function myFunction() {

        alert("Hello! I am an alert box!");
    }

    $(".BoutonMenu").click(function () {
        // crée la division qui sera convertie en popup
        $('aside').append('<div id="popupinformation" title="Information"></div>');
        $("#popupinformation").html("teste");

        // transforme la division en popup
        var popup = $("#popupinformation").dialog({
            autoOpen: true,
            width: 400,
            dialogClass: 'dialogstyleperso',
            buttons: [
                {
                    text: "OK",
                    "class": 'ui-state-information',
                    click: function () {
                        $(this).dialog("close");
                        $('#popupinformation').remove();
                    }
                }
            ]
        });
    });
})