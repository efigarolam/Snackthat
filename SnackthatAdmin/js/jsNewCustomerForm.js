/// <summary>
/// Function to add new fields for new Addresses
/// </summary>
function addNewAddresses() {
    var nAddresses = 1;
    $('#addAddress').click(function () {
        nAddresses++;
        $('input:hidden[name=nAddresses]').attr("value", nAddresses);
        $('<label for="addr' + nAddresses + '">Dirección ' + nAddresses + '</label><input type="text" name="Address' + nAddresses + '" id="addr' + nAddresses + '" />').insertBefore('input:hidden[name=nAddresses]');
        $('<label for="phn' + nAddresses + '">Teléfono ' + nAddresses + '</label><input type="text" name="Phone' + nAddresses + '" id="phn' + nAddresses + '" />').appendTo('#Phones');
    });
}