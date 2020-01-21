function cambiarPerfil() {
    var textoPerfil = document.getElementById('textoPerfil');
    if (textoPerfil.innerHTML == "Vista comprador") {
        textoPerfil.innerHTML = "Vista vendedor";
        return;
    }
    if (textoPerfil.innerHTML == "Vista vendedor") {
        textoPerfil.innerHTML = "Vista comprador";
        return;
    }
}

function cambioEstadoCheckBox() {
    var checkBox = document.getElementById("estado");
    var label = document.getElementById("labelEstado");

    if (checkBox.checked == true) {
        label.innerText = "Activa";
    } else {
        label.innerText = "Inactiva";
    }
}
