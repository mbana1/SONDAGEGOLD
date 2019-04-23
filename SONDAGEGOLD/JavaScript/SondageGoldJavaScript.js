$(document).ready(function () {
    var aid = 1;

    $('.SuprimerReponse').on('click', function () {
        
        $('.ma:last').remove();
    });

    $('body').on('focus', '.mm:last', function () {
        aid++;

        $('.mm:last').append('<div class="mm ma"><label for="Rep"' + (aid + 1) + '>Reponse N°' + (aid + 1) + ':</label> <input name="Reponse" required class="Reponse" placeholder= "Entrez votre Réponse..." /></div>').show().fadeIn(slow);
    });
})